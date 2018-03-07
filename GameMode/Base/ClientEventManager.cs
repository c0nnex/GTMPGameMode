using GrandTheftMultiplayer.Server.Elements;
using GTMPGameMode.Base;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GrandTheftMultiplayer.Server.API.API;

namespace Server.Base
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
            API.onClientEventTrigger += API_onClientEventTrigger;
        }

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
