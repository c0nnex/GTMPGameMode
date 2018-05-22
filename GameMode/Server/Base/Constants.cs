using GTANetworkInternals;
using GTANetworkAPI;
#if  GTMP
#endif
#if RAGEMP
using GTANetworkAPI; 
#endif

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTMPGameMode.Server.Base
{
    public static class Constants
    {
        public readonly static Vector3 EmptyVector = new Vector3(0, 0, 0);
    }
}
