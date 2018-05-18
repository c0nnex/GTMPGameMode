#if GTMP 
using GrandTheftMultiplayer.Server.Elements;
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
            if (entity == null || !API.Shared.doesEntityExist(entity))
                return defaultValue;
            if (!entity.hasData(dataName))
                return defaultValue;
            var tmp = entity.getData(dataName);
            if (tmp == null)
                return defaultValue;
            if (typeof(T).HasInterface(typeof(IConvertible)))
                return (T)Convert.ChangeType(tmp, typeof(T));
            else
                return (T)tmp;
        }

        public static T GetSyncedData<T>(this Entity entity, string dataName, T defaultValue = default(T))
        {
            if (entity == null || !API.Shared.doesEntityExist(entity))
                return defaultValue;
            if (!entity.hasSyncedData(dataName))
                return defaultValue;
            var tmp = entity.getSyncedData(dataName);
            if (tmp == null)
                return defaultValue;
            if (typeof(T).HasInterface(typeof(IConvertible)))
                return (T)Convert.ChangeType(tmp, typeof(T));
            else
                return (T)tmp;
        }
    }
}
