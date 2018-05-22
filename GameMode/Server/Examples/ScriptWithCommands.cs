using GTANetworkInternals;
using GTANetworkAPI;
using GTMPGameMode.Server.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTMPGameMode.Server.Examples
{
    public class ScriptWithCommands : GameModeScript
    {

        [Command("kick",GreedyArg=true)]
        public void CommandKick(Client player, string name)
        {
            var target = FindPlayer(name);
            if (target == null)
            {
                player.SendChatMessage($"Player '{name}' not found/online.");
            }
            else
            {
                target.Kick("Byebye!");
                player.SendChatMessage($"Player {target.GetCharacterName()}/{player.SocialClubName} kicked.");
                logger.Debug($"{player.SocialClubName}: KICK {target.GetCharacterName()}/{player.SocialClubName}");
            }
        }
    }
}
