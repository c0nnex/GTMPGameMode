#if GTMP
using GrandTheftMultiplayer.Server.API;
#endif
#if RAGEMP
using GTANetworkAPI;
#endif
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTMPGameMode.Server.Base
{
    /// <summary>
    /// This is a real script which binds to GT-MP Server. Restricted Use
    /// There should be only ONE RealScript in the Gamemode
    /// </summary>
    public abstract class RealScript : Script
    {
        protected Logger logger = null;
        protected static Logger sharedLogger = LogManager.GetLogger("RealScript");

#if GTMP
        public static GTAAPI sharedAPI;
#endif
#if RAGEMP
        public static GTAAPI sharedAPI;
#endif

        public RealScript()
        {
            logger = LogManager.GetLogger(this.GetType().FullName);
        }
    }


}
