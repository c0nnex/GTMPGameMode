using GTANetworkInternals;
using GTANetworkAPI;
#if GTMP
#endif
#if RAGEMP
using GTANetworkAPI;
using GTANetworkInternals;
#endif
using GTMPGameMode.Server.Base;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Timers;

namespace GTMPGameMode.Server
{
    public delegate void NoArgumentsEventHandler();

    class GameMode : RealScript
    {
        private static List<GameModeScript> _loadedScripts = new List<GameModeScript>();
        internal static Script Instance = null;

        public static bool IsShuttingDown { get; private set; }

        public static EventWaitHandle WorldStartedEvent = new ManualResetEvent(false);
        public static bool IsWorldStarted { get { return WorldStartedEvent.WaitOne(0); } }
        public static Random RND = new Random(Environment.TickCount);

        public static event NoArgumentsEventHandler OnPeriodicSave;
        public static event NoArgumentsEventHandler OnPeriodicFastSave;
        public static event NoArgumentsEventHandler OnWorldReady;
        public static event NoArgumentsEventHandler OnWorldStartup;
        public static event NoArgumentsEventHandler OnWorldStartupFirst;
        public static event NoArgumentsEventHandler OnWorldShutdown;
        public static event NoArgumentsEventHandler OnWorldReloadConfig;
        public static event NoArgumentsEventHandler OnWorldReloadConfigFirst;

        public static string VoiceServerIP = "";
        public static int VoiceServerPort = 4500;
        public static string VoiceServerSecret = "";
        public static string VoiceServerGUID = "";
        public static ulong VoiceDefaultChannel = 1;
        public static ulong VoiceIngameChannel = 115;
        public static int VoiceClientPort = 4239;
        public static string VoiceIngameChannelPassword = "egal";
        public static Version VoiceServerPluginVersion = new Version(0, 0, 0, 0);
        public static bool VoiceEnableLipSync = true;

        public static float RadioDistanceMax = 3000.0f;

#if GTMP
        private static GTAAPI GTAAPI;
#endif
#if RAGEMP
        private static GTAAPI GTAAPI;
#endif
        static GameMode()
        {

        }

        public GameMode()
        {
            var ci = new CultureInfo("en-us");
            CultureInfo.DefaultThreadCurrentCulture = ci;
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
#if GTMP
            sharedAPI = API;
            GTAAPI = API;
            GTAAPI.onResourceStart += API_onResourceStart;
            GTAAPI.onResourceStop += API_onResourceStop;
            GTAAPI.onPlayerBeginConnect += API_onPlayerBeginConnect;
#endif
#if RAGEMP

            GTAAPI.WireEvents(API);
#endif
        }

#if RAGEMP
        [ServerEvent(Event.ResourceStart)]
#endif
        private void API_onResourceStart()
        {
            Instance = this;
            var allTypes = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var item in allTypes)
            {
                if (item.IsSubclassOf(typeof(GameModeScript)) && !item.IsAbstract)
                {
                    logger.Info($"Loading class {item.Name}");
                    var o = Activator.CreateInstance(item) as GameModeScript;
                    if (o != null)
                        _loadedScripts.Add(o); // Important. Prevent Garbage collection
                }
                if (item.IsSubclassOf(typeof(Script)) && (item != typeof(GameMode)) && !item.IsAbstract)
                    throw new InvalidOperationException($"Derivates of Script are not allowed! ({item.Name})");
            }
            // give other ressources some time to start up
            GTAAPI.Delay(2000, true, () => WorldStartup());
        }

#if RAGEMP
        [ServerEvent(Event.ResourceStop)]
#endif
        private void API_onResourceStop()
        {
            IsShuttingDown = true;
            ShutDown(true, false);
        }

        [Command("reload")]
        public void CommandReload(Client player)
        {
            /* Do needed Stuff*/
            OnWorldReloadConfigFirst?.Invoke();
            OnWorldReloadConfig?.Invoke();
        }

#if GTMP
        private void API_onPlayerBeginConnect(Client player, CancelEventArgs cancelConnection)
#endif
#if RAGEMP
        [ServerEvent(Event.PlayerConnected)]
        private void API_onPlayerBeginConnect(Client player)
#endif
        {
            if (!IsWorldStarted)
            {
#if GTMP
                cancelConnection.Cancel = true;
                cancelConnection.Reason = "Server still starting. Please wait a minute...";
#endif
#if RAGEMP
                player.Kick("Server still starting. Please wait a minute...");
#endif
                return;
            }
        }

        private void RegisterScriptCommands(GameModeScript script)
        {
#if GTMP
            var methods = script.GetType().GetMethods();
            foreach (var method in methods.Where(ifo => ifo.CustomAttributes.Any(att => att.AttributeType == typeof(CommandAttribute))))
            {
                var cmd = method.GetCustomAttribute<CommandAttribute>();
                GTAAPI.addResourceChatCommand(method, cmd, script);
                logger.Info($"RegisterCommand {script.GetType().Name}:{cmd.Alias}/{method.Name}");
            }
#endif
#if RAGEMP // Not supported
            if (script.GetType().GetMethods().Any(ifo => ifo.CustomAttributes.Any(att => att.AttributeType == typeof(CommandAttribute))))
                throw new NotSupportedException("OutOfScript-Commands are not supported by RageMP");
#endif
        }

        private void RegisterExportedFunctions(GameModeScript script)
        {
            var scriptType = script.GetType();
            if (!scriptType.CustomAttributes.Any(att => att.AttributeType == typeof(ExportAsAttribute)))
                return;
#if GTMP
            var exportedPool = GTAAPI.exported as IDictionary<string, object>;
#endif
#if RAGEMP
            var exportedPool = GTAAPI.Exported as IDictionary<string, object>;
#endif
            var resName = scriptType.GetCustomAttribute<ExportAsAttribute>().ExportedRessourceName;

            dynamic resPool = null;
            var needToAdd = false;
            if (!exportedPool.TryGetValue(resName, out resPool)) // already exporterd? then expand else create new expandobject
            {
                needToAdd = true;
                resPool = new ExpandoObject();
            }

            var exportDict = resPool as IDictionary<string, object>;

            foreach (var method in scriptType.GetMethods().Where(ifo => ifo.CustomAttributes.Any(att => att.AttributeType == typeof(ExportFunctionAttribute))))
            {
                ExportedFunctionDelegate punchthrough = delegate (object[] parameters)
                {
                    return script.InvokeMethod(method, parameters);
                };
                exportDict.Add(method.Name, punchthrough);
            }

            foreach (var eventInfo in scriptType.GetEvents().Where(ifo => ifo.CustomAttributes.Any(att => att.AttributeType == typeof(ExportEventAttribute))))
            {
                exportDict.Add(eventInfo.Name, null);

                ExportedEvent punchthrough = delegate (dynamic[] parameters)
                {
                    var e = exportDict[eventInfo.Name] as ExportedEvent;
                    e?.Invoke(parameters);
                };

                eventInfo.AddEventHandler(script, punchthrough);
            }

            if (needToAdd)
                exportedPool.Add(resName, resPool);
        }

        private void WorldStartup()
        {

            VoiceDefaultChannel = GTAAPI.GetSetting<ulong>("voice_defaultchannel");
            VoiceIngameChannel = GTAAPI.GetSetting<ulong>("voice_ingamechannel");
            VoiceIngameChannelPassword = GTAAPI.GetSetting<string>("voice_ingamechannelpassword");
            VoiceServerGUID = GTAAPI.GetSetting<string>("voice_serverguid");
            ;
            VoiceServerIP = GTAAPI.GetSetting<string>("voice_server");
            VoiceServerPort = GTAAPI.GetSetting<int>("voice_port");
            VoiceServerSecret = GTAAPI.GetSetting<string>("voice_secret");
            VoiceClientPort = GTAAPI.GetSetting<int>("voice_clientport");
            Version.TryParse(GTAAPI.GetSetting<string>("voice_minpluginversion"), out VoiceServerPluginVersion);
            VoiceEnableLipSync = GTAAPI.GetSetting<bool>("voice_enablelipsync");
            RadioDistanceMax = GTAAPI.GetSetting<float>("radio_max_distance");

            OnWorldStartupFirst?.Invoke();

            // Register Exports in GameModeScript's
            _loadedScripts.OrderBy(lt => lt.ScriptStartPosition).ToList().ForEach(lt => RegisterExportedFunctions(lt));

            // Start all sub GameModeScript's
            _loadedScripts.OrderBy(lt => lt.ScriptStartPosition).ToList().ForEach(lt => lt.OnScriptStart());

            // Regsiter Commands in GameModeScript's
            _loadedScripts.OrderBy(lt => lt.ScriptStartPosition).ToList().ForEach(lt => RegisterScriptCommands(lt));

            OnWorldStartup?.Invoke();

            // We are reday to go
            OnWorldReady?.Invoke();

            WorldStartedEvent.Set();
            logger.Info("Server ready to accept connections ....");
        }

        private static bool PreShutDownDone = false;
        private static void ShutDown(bool kickPlayers, bool stopServer)
        {
            try
            {
                if (PreShutDownDone)
                    return;
                PreShutDownDone = true;
                sharedLogger.Debug("ShutDown ENTER");
                IsShuttingDown = true;
                WorldStartedEvent.Reset();
                if (kickPlayers)
                {
                    foreach (var player in GTAAPI.shared.GetAllPlayers().ToList())
                    {
                        // Maybe Save Players?
                        GTAAPI.Shared.KickPlayer(player, "Server restart");
                    }
                    Thread.Sleep(2000);
                }
                sharedLogger.Debug("ShutDown 2");
                OnPeriodicFastSave?.Invoke();
                sharedLogger.Debug("ShutDown 3");
                OnPeriodicSave?.Invoke();
                sharedLogger.Debug("ShutDown 4");
                OnWorldShutdown?.Invoke();
                sharedLogger.Debug("ShutDown 5");
                // Stop all sub Scripts
                _loadedScripts.OrderBy(lt => lt.ScriptStartPosition).ToList().ForEach(lt => lt.OnScriptStop());
                sharedLogger.Debug("ShutDown EXIT");
                if (stopServer)
                {
#if GTMP
                    GTAAPI.shared.stopServer();
#else
                    throw new NotSupportedException();
#endif
                }
            }
            catch (Exception ex)
            {
                sharedLogger.Fatal(ex);
            }
        }



    }
}
