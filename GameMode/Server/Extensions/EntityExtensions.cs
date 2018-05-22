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
    public static class EntityExtensions
    {
        public static T GetData<T>(this Entity entity, string dataName, T defaultValue = default(T))
        {
            if (entity == null || !GTAAPI.Shared.DoesEntityExist(entity))
                return defaultValue;
            if (!entity.HasData(dataName))
                return defaultValue;
            var tmp = entity.GetData(dataName);
            if (tmp == null)
                return defaultValue;
            if (typeof(T).HasInterface(typeof(IConvertible)))
                return (T)Convert.ChangeType(tmp, typeof(T));
            else
                return (T)tmp;
        }

        public static T GetSyncedData<T>(this Entity entity, string dataName, T defaultValue = default(T))
        {
            if (entity == null || !GTAAPI.Shared.DoesEntityExist(entity))
                return defaultValue;
            if (!entity.HasSharedData(dataName))
                return defaultValue;
            var tmp = entity.GetSharedData(dataName);
            if (tmp == null)
                return defaultValue;
            if (typeof(T).HasInterface(typeof(IConvertible)))
                return (T)Convert.ChangeType(tmp, typeof(T));
            else
                return (T)tmp;
        }
    }
}
