#if GTMP
using static GrandTheftMultiplayer.Server.API.API;
using GrandTheftMultiplayer.Server.Elements;
#endif
#if RAGEMP
using GTANetworkAPI;
#endif

using GTMPGameMode.Server.Base;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTMPGameMode.Server.Managers
{
    class ClientEventManager : GameModeScript
    {
        private static ConcurrentDictionary<string, ServerEventTrigger> _ClientEvents = new ConcurrentDictionary<string, ServerEventTrigger>(StringComparer.InvariantCultureIgnoreCase);

        public static void RegisterClientEvent(string eventName, ServerEventTrigger serverFunction)
        {
            _ClientEvents[eventName] = serverFunction;
        }

        public override ScriptPriority ScriptStartPosition => ScriptPriority.FIRST;

        public override void OnScriptStart()
        {
#if GTMP
            API.onClientEventTrigger += API_onClientEventTrigger;
#endif
        }

#if RAGEMP
        [RemoteEvent] 
#endif
        private void API_onClientEventTrigger(Client player, string eventName, params object[] arguments)
        {
            if (_ClientEvents.TryGetValue(eventName,out var handler))
            {
                try
                {
                    handler.Invoke(player, eventName, arguments);
                    return;
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Problem in onClientEventTrigger {0}", eventName);
                }
            }
            else
            {
                logger.Trace("Unhandled ClientEvent: {0}", eventName);
            }
        }
    }
}
