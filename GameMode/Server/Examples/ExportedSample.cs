using GTANetworkInternals;
using GTANetworkAPI;
#if GTMP
#endif
#if RAGEMP
using GTANetworkAPI;
#endif

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTMPGameMode.Server.Base;

namespace GTMPGameMode.Server.Examples
{ 
    [ExportAs("coolname")]
    public class ExportedSample : GameModeScript
    {
        [ExportEvent]
        public event EventHandler<GameModeScript,Client> OnReturnTrue;

        [ExportFunction]
        public bool ReturnTrue(Client player)
        {
            OnReturnTrue?.Invoke(this,player);
            return player != null;
        }

        /* To Access it from other GTMP scripts:
         * 
         * GTAAPI.exported.coolname.ReturnTrue(client)
         * 
         * GTAAPI.exported.coolname.OnReturnTrue += myEventhandler;
         * public void myEventHandler(GameModeScript script, Client player)
         * {
         * ]
         * 
         */

    }
}
