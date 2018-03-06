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
        public static T GetData<T>(this Client player, string dataName, T defaultValue = default(T))
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

        public static T GetSyncedData<T>(this Client player, string dataName, T defaultValue = default(T))
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
            return player.handle.Value;
        }

        public static string GetTeamspeakID(this Client player)
        {
            return player.GetData("PLAYER_TEAMSPEAK_IDENT", "NothingAtAll");
        }
        public static ushort GetTeamspeakClientID(this Client player)
        {
            return player.GetData("VOICE_TS_ID", (ushort)0);
        }

        public static string GetName(this Client player)
        {
            return player.GetData("PLAYER_TEAMSPEAK_NAME", "");
        }

        public static string GetCharacterName(this Client player)
        {
            return player.GetData("PLAYER_CHARCTER_NAME", player.name);
        }
    }
}
