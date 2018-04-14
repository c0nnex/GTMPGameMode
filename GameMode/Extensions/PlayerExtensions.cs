using GrandTheftMultiplayer.Server.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTMPGameMode.Base
{
    public static class PlayerExtensions
    {
        #region Data Helpers
        public static T GetData<T>(this Client player, string dataName, T defaultValue = default)
        {
            if (player == null || !player.exists)
                return defaultValue;
            if (!player.hasData(dataName))
                return defaultValue;
            var tmp = player.getData(dataName);
            if (tmp == null)
                return defaultValue;
            if (typeof(T).HasInterface(typeof(IConvertible)))
                return (T)Convert.ChangeType(tmp, typeof(T));
            else
                return (T)tmp;
        }

        public static T GetSyncedData<T>(this Client player, string dataName, T defaultValue = default)
        {
            if (player == null || !player.exists)
                return defaultValue;
            if (!player.hasSyncedData(dataName))
                return defaultValue;
            var tmp = player.getSyncedData(dataName);
            if (tmp == null)
                return defaultValue;
            if (typeof(T).HasInterface(typeof(IConvertible)))
                return (T)Convert.ChangeType(tmp, typeof(T));
            else
                return (T)tmp;
        }
        #endregion

        public static bool IsAdmin(this Client player)
        {
            return player.GetData("IS_ADMIN", false);
        }

        public static bool IsReady(this Client player)
        {
            return player.GetData("IS_READY", false);
        }

        public static bool IsDead(this Client player)
        {
            return player.GetData("IS_DEAD", false) || player.dead;
        }

        public static int GetCharacterId(this Client player)
        {
            return player.GetData("PLAYER_ID", player.handle.Value);
        }

        public static string GetTeamspeakID(this Client player)
        {
            return player.GetData("PLAYER_TEAMSPEAK_IDENT", "NothingAtAll");
        }

        public static ushort GetTeamspeakClientID(this Client player)
        {
            return player.GetData("VOICE_TS_ID", (ushort)0);
        }

        public static long GetVoiceConnectionID(this Client player)
        {
            return player.GetData("VOICE_ID", (long)0);
        }

        public static string GetName(this Client player)
        {
            return player.GetData("PLAYER_TEAMSPEAK_NAME", "");
        }

        public static string GetCharacterName(this Client player)
        {
            return player.GetData("PLAYER_CHARACTER_NAME", player.name);
        }

        public static void UpdateHUD(this Client player)
        {
            //TODO: Do whatever is needed to updte player HUD
            player.triggerEvent("UPDATE_HUD");
        }

        public static void PlaySound(this Client player, string soundName, float distanceToHear = 0, bool loop = false, bool needStopEvent = false)
        {
            // TODO: Add whatever is necessary to play a sound at the player
        }

        public static void Message(this Client player, string msg)
        {
            player.sendNotification("System", msg, false);
        }
        
        #region Radio stuff
        public static RadioModes GetRadioMode(this Client player)
        {
            switch (player.GetData("RADIO_MODE", "off"))
            {
                case "off":
                    return RadioModes.OFF;
                case "on":
                    return RadioModes.LISTENING;
                case "send":
                    return RadioModes.SPEAKING;
            }
            return RadioModes.OFF;
        }

        public static void SetRadioMode(this Client player, RadioModes newMode)
        {
            switch (newMode)
            {
                case RadioModes.OFF:
                    player.setData("RADIO_MDOE", "off");
                    break;
                case RadioModes.LISTENING:
                    player.setData("RADIO_MDOE", "on");
                    break;
                case RadioModes.SPEAKING:
                    player.setData("RADIO_MDOE", "send");
                    break;
                default:
                    break;
            }
        }

        public static int GetRadioChannel(this Client player)
        {
            return player.GetData("RADIO_CHANNEL", 0);
        }
        public static void SetRadioChannel(this Client player, int channel)
        {
            player.setData("RADIO_CHANNEL", channel);
        }

        public static bool CanUseRadio(this Client player)
        {
            return true;
        }
        #endregion
    }
}
