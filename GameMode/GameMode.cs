using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;
using GTMPGameMode.Base;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Timers;

namespace GTMPGameMode
{
    public delegate void NoArgumentsEventHandler();

    class GameMode : RealScript
    {
        private static List<GameModeScript> _loadedScripts = new List<GameModeScript>();

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

        static GameMode()
        {

        }

        public GameMode()
        {
            var ci = new CultureInfo("en-us");
            CultureInfo.DefaultThreadCurrentCulture = ci;
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
            sharedAPI = API;
            API.onResourceStart += API_onResourceStart;
            API.onResourceStop += API_onResourceStop;
            API.onPlayerBeginConnect += API_onPlayerBeginConnect;
        }

        

        private void API_onResourceStart()
        {

            var allTypes = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var item in allTypes)
            {
                if (item.IsSubclassOf(typeof(GameModeScript)) && !item.IsAbstract)
                {
                    logger.Info($"Loading class {item.Name}");
                    var o = Activator.CreateInstance(item) as GameModeScript;
                    if (o!= null)
                        _loadedScripts.Add(o); // Important. Prevent Garbage collection
                }
                if (item.IsSubclassOf(typeof(Script)) && (item != typeof(GameMode)) && !item.IsAbstract)
                    throw new InvalidOperationException($"Derivates of Script are not allowed! ({item.Name})");
            }
            // give other ressources some time to start up
            API.delay(2000, true, () => WorldStartup());
        }

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

        private void API_onPlayerBeginConnect(Client player, CancelEventArgs cancelConnection)
        {
            if (!IsWorldStarted)
            {
                cancelConnection.Cancel = true;
                cancelConnection.Reason = "Server still starting. Please wait a minute...";
                return;
            }
        }

        private void RegisterScriptCommands(GameModeScript script)
        {
            var methods = script.GetType().GetMethods();
            foreach (var method in methods.Where(ifo => ifo.CustomAttributes.Any(att => att.AttributeType == typeof(CommandAttribute))))
            {
                var cmd = method.GetCustomAttribute<CommandAttribute>();
                API.addResourceChatCommand(method, cmd, script);
                logger.Info($"RegisterCommand {script.GetType().Name}:{cmd.Alias}/{method.Name}");
            }
        }

        private void WorldStartup()
        {
            VoiceDefaultChannel = API.getSetting<ulong>("voice_defaultchannel");
            VoiceIngameChannel = API.getSetting<ulong>("voice_ingamechannel");
            VoiceIngameChannelPassword = API.getSetting<string>("voice_ingamechannelpassword");
            VoiceServerGUID = API.getSetting<string>("voice_serverguid"); ;
            VoiceServerIP = API.getSetting<string>("voice_server");
            VoiceServerPort = API.getSetting<int>("voice_port");
            VoiceServerSecret = API.getSetting<string>("voice_secret");
            VoiceClientPort = API.getSetting<int>("voice_clientport");
            Version.TryParse(API.getSetting<string>("voice_minpluginversion"), out VoiceServerPluginVersion);
            VoiceEnableLipSync = API.getSetting<bool>("voice_enablelipsync");
            RadioDistanceMax = API.getSetting<float>("radio_max_distance");

            OnWorldStartupFirst?.Invoke();

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
                    foreach (var player in API.shared.getAllPlayers().ToList())
                    {
                        // Maybe Save Players?
                        player.kick("Server restart");
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
                    API.shared.stopServer();
            }
            catch (Exception ex)
            {
                sharedLogger.Fatal(ex);
            }
        }

       

    }
}
