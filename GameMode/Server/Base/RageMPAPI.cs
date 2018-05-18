using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTMPGameMode.Server
{
    public delegate void ServerEventTrigger(Client sender, string eventName, params object[] arguments);

    public class GTAAPI : API
    {
        public static API shared => API.Shared;
        // public static Vector3 GetEntityPosition(NetHandle handle) => API.Shared.GetEntityPosition(handle);
    }

    public static class RageMPExtensions
    {
        public static T getSetting<T>(this API api ,string key)
        {
            return api.GetSetting<T>(GameMode.Instance, key);
        }

        public static void delay(this API api,int ms, bool onlyOnce, Action action)
        {
            if (onlyOnce)
                Task.Delay(ms).ContinueWith((t) => action());
            else
            {
                throw new NotImplementedException("Not implemented yet");
            }
        }
        public static List<Client> getAllPlayers(this API api) => api.GetAllPlayers();

        public static bool doesEntityExist(this API api,NetHandle handle) => API.Shared.DoesEntityExist(handle);

        public static bool hasData(this Entity entity, string key) => entity.HasData(key);
        public static dynamic getData(this Entity entity, string key) => entity.GetData(key);
        public static bool hasSyncedData(this Entity entity, string key) => entity.HasSharedData(key);
        public static dynamic getSyncedData(this Entity entity, string key) => entity.GetSharedData(key);
        public static void resetData(this Entity entity, string key) => entity.ResetData(key);
        public static void setData(this Entity entity, string key, object value) => entity.SetData(key, value);
        public static void setSyndedData(this Entity entity, string key, object value) => entity.SetSharedData(key, value);
       
        public static void kick(Client player, string message) => player.Kick(message);
        public static bool isDead(this Client player) => player.Dead;
        public static string getName(this Client player) => player.Name;
        public static void triggerEvent(this Client player, string eventName, params object[] args) => player.TriggerEvent(eventName, args);
        
    }
}
