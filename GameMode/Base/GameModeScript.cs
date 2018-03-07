using GrandTheftMultiplayer.Server.API;
using NLog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTMPGameMode.Base
{
    /// <summary>
    /// This is a GameMode-Internal script which will be called by the RealScript API.
    /// </summary>
    public abstract class GameModeScript 
    {
        protected static Logger sharedLogger = LogManager.GetLogger("GameModeScript");
        protected Logger logger;
        protected API API => GameMode.sharedAPI;

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

        public static T GetArg<T>(object[] args, int index , T defaultValue = default(T))
        {
            if ( (args == null) || (index >= args.Length))
                return defaultValue;
            try
            {
                T tmp = (T)Convert.ChangeType(args[index], typeof(T), CultureInfo.InvariantCulture);
                return tmp;
            }
            catch { return defaultValue; }
            
        }

        public static T GetArg<T>(IEnumerable<object> args, int index, T defaultValue = default(T))
        {
            var tmpList = args.ToList();
            if ((args == null) || (index >= tmpList.Count))
                return defaultValue;
            try
            {
                T tmp = (T)Convert.ChangeType(tmpList[index], typeof(T), CultureInfo.InvariantCulture);
                return tmp;
            }
            catch { return defaultValue; }
        }
    }
}
