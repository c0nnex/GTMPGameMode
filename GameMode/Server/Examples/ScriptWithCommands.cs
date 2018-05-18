using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Managers;
using GTMPGameMode.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTMPGameMode.Examples
{
    public class ScriptWithCommands : GameModeScript
    {

        [Command("kick",GreedyArg=true)]
        public void CommandKick(Client player, string name)
        {
            var target = FindPlayer(name);
            if (target == null)
            {
                player.sendChatMessage($"Player '{name}' not found/online.");
            }
            else
            {
                target.kick("Byebye!");
                player.sendChatMessage($"Player {target.GetCharacterName()}/{player.socialClubName} kicked.");
                logger.Debug($"{player.socialClubName}: KICK {target.GetCharacterName()}/{player.socialClubName}");
            }
        }
    }
}
