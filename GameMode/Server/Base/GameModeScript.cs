//using GrandTheftMultiplayer.Server.API;
#if GTMP
using GrandTheftMultiplayer.Server.Elements;
#endif
#if RAGEMP
using GTANetworkAPI;
#endif
using NLog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GTMPGameMode.Server.Base
{
    /// <summary>
    /// This is a GameMode-Internal script which will be called by the RealScript API.
    /// </summary>
    public abstract class GameModeScript
    {
        protected static Logger sharedLogger = LogManager.GetLogger("GameModeScript");
        protected Logger logger;
        protected GTAAPI API => GameMode.sharedAPI;

        static GameModeScript()
        {
        }

        public GameModeScript()
        {
            logger = LogManager.GetLogger(this.GetType().FullName);
        }

        /// <summary>
        /// Override. Replaces API.OnResourceStart
        /// </summary>
        public virtual void OnScriptStart() { }

        /// <summary>
        /// Override. Replaces API.OnResourceStop
        /// </summary>
        public virtual void OnScriptStop() { }

        /// <summary>
        /// Override to influence starting priority. The lower the Position the earlier it will be called. 
        /// Use it to sequence scripts that depend on other scripts with a lower position.
        /// </summary>
        public virtual ScriptPriority ScriptStartPosition => ScriptPriority.AFTERINIT;

        public static T GetArg<T>(object[] args, int index, T defaultValue = default)
        {
            return args.GetArg<T>(index, defaultValue);
        }

        public static T GetArg<T>(IEnumerable<object> args, int index, T defaultValue = default)
        {
            return args.GetArg<T>(index, defaultValue);
        }

        protected Client FindPlayer(string name)
        {
            var allClients = API.getAllPlayers().ToList(); // ToList is important else connecting/disconnecting players will casue an exception

            return allClients.FirstOrDefault(cl => cl.IsReady() && (
                String.Compare(GTAAPI.Shared.GetPlayerSocialClubName(cl), name, true) == 0 || 
                String.Compare(GTAAPI.Shared.GetPlayerName(cl), name, true) == 0 || 
                String.Compare(cl.GetName(), name, true) == 0 || 
                String.Compare(cl.GetCharacterName(), name, true) == 0));
        }


        internal dynamic InvokeMethod(MethodInfo methodInfo, object[] args)
        {
            if (args.Length == 0)
            {
                return methodInfo.Invoke(this, null);
            }

            var nArgs = args.ToList();
            if (args.Length != methodInfo.GetParameters().Length)
            {
                for (int i = args.Length; i < methodInfo.GetParameters().Length; i++)
                {
                    nArgs.Add(Type.Missing);
                }
            }

            return methodInfo.Invoke(this, nArgs.ToArray());
        }
    }
}
