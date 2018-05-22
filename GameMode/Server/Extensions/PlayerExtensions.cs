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

namespace GTMPGameMode.Server.Base
{
    public static class PlayerExtensions
    {
        #region Data Helpers
        public static T GetData<T>(this Client player, string dataName, T defaultValue = default)
        {
            if (player == null || !GTAAPI.Shared.DoesEntityExist(player))
                return defaultValue;
            if (!player.HasData(dataName))
                return defaultValue;
            var tmp = player.GetData(dataName);
            if (tmp == null)
                return defaultValue;
            if (typeof(T).HasInterface(typeof(IConvertible)))
                return (T)Convert.ChangeType(tmp, typeof(T));
            else
                return (T)tmp;
        }

        public static T GetSyncedData<T>(this Client player, string dataName, T defaultValue = default)
        {
            if (player == null || !GTAAPI.Shared.DoesEntityExist(player))
                return defaultValue;
            if (!player.HasSharedData(dataName))
                return defaultValue;
            var tmp = player.GetSharedData(dataName);
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
            return player.GetData("IS_DEAD", false) || player.IsDead();
        }

        public static int GetCharacterId(this Client player)
        {
            return player.GetData("PLAYER_ID", ((NetHandle)player).Value);
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
            return player.GetData("PLAYER_CHARACTER_NAME", player.GetName());
        }

        public static void UpdateHUD(this Client player)
        {
            //TODO: Do whatever is needed to updte player HUD
            player.TriggerEvent("UPDATE_HUD");
        }

        public static void PlaySound(this Client player, string soundName, float distanceToHear = 0, bool loop = false, bool needStopEvent = false)
        {
            // TODO: Add whatever is necessary to play a sound at the player
        }

        public static void Message(this Client player, string msg)
        {
#if GTMP
            player.SendNotification("System", msg, false);
#endif
#if RAGEMP
            player.SendNotification(msg, false);
#endif
        }

        /// <summary>
        /// Set/Reset virtual voice-position of player
        /// </summary>
        /// <param name="player"></param>
        /// <param name="virtualPosition">null = reset</param>
        public static void SetVoicePosition(this Client player, Vector3 virtualPosition = null)
        {
            if (virtualPosition == null)
                player.ResetData("VOICE_POSITION");
            else
                player.SetData("VOICE_POSITION", virtualPosition);
        }

        /// <summary>
        /// Get the Voice Position of this player. Takes into account if he is in an interior that is physically somewhere else.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static Vector3 GetVoicePosition(this Client player)
        {
            if (player.HasData("VOICE_POSITION"))
                return player.GetData("VOICE_POSITION", new Vector3());
            return GTAAPI.Shared.GetEntityPosition(player);
        }

#region Radio stuff

        /// <summary>
        /// Set/Reset virtual radio voice-position of player
        /// </summary>
        /// <param name="player"></param>
        /// <param name="virtualPosition">null = reset</param>
        public static void SetRadioVoicePosition(this Client player, Vector3 virtualPosition = null)
        {
            if (virtualPosition == null)
                player.ResetData("RADIO_VOICE_POSITION");
            else
                player.SetData("RADIO_VOICE_POSITION", virtualPosition);
        }

        /// <summary>
        /// Get the radio Voice Position of this player. Takes into account if he is in an interior that is physically somewhere else.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static Vector3 GetRadioVoicePosition(this Client player)
        {
            if (player.HasData("RADIO_VOICE_POSITION"))
                return player.GetData("RADIO_VOICE_POSITION", new Vector3());
            return GTAAPI.Shared.GetEntityPosition(player);
        }

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
                    player.SetData("RADIO_MODE", "off");
                    break;
                case RadioModes.LISTENING:
                    player.SetData("RADIO_MODE", "on");
                    break;
                case RadioModes.SPEAKING:
                    player.SetData("RADIO_MODE", "send");
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
            player.SetData("RADIO_CHANNEL", channel);
        }

        public static bool CanUseRadio(this Client player)
        {
            return true;
        }
#endregion
    }
}
