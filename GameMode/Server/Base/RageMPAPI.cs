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
        public static GTAAPI Instance;

        public static API shared;

        public static EntityDataResetedEvent onEntityDataReset;
        public static ResourceEvent onServerResourceStart;
        public static ResourceEvent onServerResourceStop;
        public static ServerResouceEvent onServerResourceEvent;
        public static EntityHealthEvent onVehicleHealthChange;
        public static PlayerIntEvent onPlayerHealthChange;
        public static PlayerIntEvent onPlayerArmorChange;
        public static PlayerWeaponEvent onPlayerWeaponSwitch;
        public static PlayerAmmoEvent onPlayerWeaponAmmoChange;
        public static EntityBooleanEvent onVehicleSirenToggle;
        public static EntityIntEvent onVehicleDoorBreak;
        public static EntityIntEvent onVehicleTyreBurst;
        public static TrailerEvent onVehicleTrailerChange;
        public static PlayerIntEvent onPlayerModelChange;
        public static PlayerEvent onPlayerDetonateStickies;
        public static MoveEvent onEntityMovingPositionFinished;
        public static MoveEvent onEntityMovingRotationFinished;
        public static PlayerDamagedEvent onPlayerDamaged;
        public static PlayerWaypointChangeEvent onPlayerWaypointChange;
        public static EntityDataChangedEvent onEntityDataChange;
        public static GlobalColShapeEvent onEntityExitColShape;
        public static EntityIntEvent onVehicleWindowSmash;
        public static EntityEvent onVehicleDeath;
        public static GlobalColShapeEvent onEntityEnterColShape;
        public static EmptyEvent onResourceStart;
        public static EmptyEvent onResourceStop;
        public static EmptyEvent onUpdate;
        public static ChatEvent onChatMessage;
        public static PlayerConnectingEvent onPlayerBeginConnect;
        public static PlayerEvent onPlayerConnected;
        public static PlayerEvent onPlayerFinishedDownload;
        public static PlayerDisconnectedEvent onPlayerDisconnected;
        public static CommandEvent onChatCommand;
        public static PlayerEvent onPlayerRespawn;
        public static ServerEventTrigger onClientEventTrigger;
        public static ServerEventTrigger onCrossResourceClientEventTrigger;
        public static PickupEvent onPlayerPickup;
        public static EntityEvent onPickupRespawn;
        public static MapChangeEvent onMapChange;
        public static VehicleChangeEvent onPlayerEnterVehicle;
        public static VehicleChangeEvent onPlayerExitVehicle;
        public static PlayerKilledEvent onPlayerDeath;
        public static VehicleSeatChangeEvent onPlayerChangeVehicleSeat;

        public delegate void ResourceEvent(string resourceName);
        public delegate void DataReceivedEvent(string data);
        public delegate void PlayerIntEvent(Client player, int oldValue);
        public delegate void PlayerWeaponEvent(Client player, WeaponHash oldValue);
        public delegate void PlayerAmmoEvent(Client player, WeaponHash weapon, int oldValue);
        public delegate void TrailerEvent(NetHandle tower, NetHandle trailer);
        public delegate void EntityBooleanEvent(NetHandle entity, bool oldValue);
        public delegate void EntityIntEvent(NetHandle entity, int index);
        public delegate void MoveEvent(NetHandle entity);
        public delegate void EntityDataResetedEvent(NetHandle entity, string key);
        public delegate void PlayerDamagedEvent(Client victim, Client enemy, int weaponDamageCause, int boneHitId);
        public delegate void EntityHealthEvent(NetHandle entity, float oldValue);
        public delegate void EntityDataChangedEvent(NetHandle entity, string key, object oldValue);
        public delegate void PlayerEvent(Client player);
        public delegate void VehicleSeatChangeEvent(Client player, NetHandle vehicle, int oldSeat, int newSeat);
        public delegate void VehicleChangeEvent(Client player, NetHandle vehicle, int seat);
        public delegate void MapChangeEvent(string mapName, XmlGroup map);
        public delegate void EntityEvent(NetHandle entity);
        public delegate void PickupEvent(Client pickupee, NetHandle pickupHandle);
        public delegate void ServerEventTrigger(Client sender, string eventName, params object[] arguments);
        public delegate void PlayerKilledEvent(Client player, NetHandle entityKiller, int weapon);
        public delegate void PlayerDisconnectedEvent(Client player, string reason);
        public delegate void PlayerConnectingEvent(Client player, CancelEventArgs cancelConnection);
        public delegate void ChatEvent(Client sender, string message, CancelEventArgs cancel);
        public delegate void CommandEvent(Client sender, string command, CancelEventArgs cancel);
        public delegate void EmptyEvent();
        public delegate void PlayerWaypointChangeEvent(Client player, bool placed, Vector3 position);
        public delegate void GlobalColShapeEvent(ColShape colshape, NetHandle entity);
        public delegate void ServerResouceEvent(string resourceName, string eventName, params object[] arguments);


        internal static void WireEvents(API api)
        {
#if RAGEMP
            Instance = new GTAAPI();
            shared = GTAAPI.Shared;
#endif


#if GTMP
            shared = API.shared;
            api.onEntityDataReset += myonEntityDataReset;
            api.onServerResourceStart += myonServerResourceStart;
            api.onServerResourceStop += myonServerResourceStop;
            api.onServerResourceEvent += myonServerResourceEvent;
            api.onVehicleHealthChange += myonVehicleHealthChange;
            api.onPlayerHealthChange += myonPlayerHealthChange;
            api.onPlayerArmorChange += myonPlayerArmorChange;
            api.onPlayerWeaponSwitch += myonPlayerWeaponSwitch;
            api.onPlayerWeaponAmmoChange += myonPlayerWeaponAmmoChange;
            api.onVehicleSirenToggle += myonVehicleSirenToggle;
            api.onVehicleDoorBreak += myonVehicleDoorBreak;
            api.onVehicleTyreBurst += myonVehicleTyreBurst;
            api.onVehicleTrailerChange += myonVehicleTrailerChange;
            api.onPlayerModelChange += myonPlayerModelChange;
            api.onPlayerDetonateStickies += myonPlayerDetonateStickies;
            api.onEntityMovingPositionFinished += myonEntityMovingPositionFinished;
            api.onEntityMovingRotationFinished += myonEntityMovingRotationFinished;
            api.onPlayerDamaged += myonPlayerDamaged;
            api.onPlayerWaypointChange += myonPlayerWaypointChange;
            api.onEntityDataChange += myonEntityDataChange;
            api.onEntityExitColShape += myonEntityExitColShape;
            api.onVehicleWindowSmash += myonVehicleWindowSmash;
            api.onVehicleDeath += myonVehicleDeath;
            api.onEntityEnterColShape += myonEntityEnterColShape;
            api.onResourceStart += myonResourceStart;
            api.onResourceStop += myonResourceStop;
            api.onUpdate += myonUpdate;
            api.onChatMessage += myonChatMessage;
            api.onPlayerBeginConnect += myonPlayerBeginConnect;
            api.onPlayerConnected += myonPlayerConnected;
            api.onPlayerFinishedDownload += myonPlayerFinishedDownload;
            api.onPlayerDisconnected += myonPlayerDisconnected;
            api.onChatCommand += myonChatCommand;
            api.onPlayerRespawn += myonPlayerRespawn;
            api.onClientEventTrigger += myonClientEventTrigger;
            api.onCrossResourceClientEventTrigger += myonCrossResourceClientEventTrigger;
            api.onPlayerPickup += myonPlayerPickup;
            api.onPickupRespawn += myonPickupRespawn;
            api.onMapChange += myonMapChange;
            api.onPlayerEnterVehicle += myonPlayerEnterVehicle;
            api.onPlayerExitVehicle += myonPlayerExitVehicle;
            api.onPlayerDeath += myonPlayerDeath;
            api.onPlayerChangeVehicleSeat += myonPlayerChangeVehicleSeat;
#endif
        }

    private static void myonPlayerChangeVehicleSeat(Client player, NetHandle vehicle, int oldSeat, int newSeat)
    {
        onPlayerChangeVehicleSeat?.Invoke(player, vehicle, oldSeat, newSeat);
    }

    private static void myonPlayerDeath(Client player, NetHandle entityKiller, int weapon)
    {
        onPlayerDeath?.Invoke(player, entityKiller, weapon);
    }

    private static void myonPlayerExitVehicle(Client player, NetHandle vehicle, int seat)
    {
        onPlayerExitVehicle?.Invoke(player, vehicle, seat);
    }

    private static void myonPlayerEnterVehicle(Client player, NetHandle vehicle, int seat)
    {
        onPlayerEnterVehicle?.Invoke(player, vehicle, seat);
    }

    private static void myonMapChange(string mapName, XmlGroup map)
    {
        onMapChange?.Invoke(mapName, map);
    }

    private static void myonPickupRespawn(NetHandle entity)
    {
        onPickupRespawn?.Invoke(entity);
    }

    private static void myonPlayerPickup(Client pickupee, NetHandle pickupHandle)
    {
        onPlayerPickup?.Invoke(pickupee, pickupHandle);
    }

    private static void myonCrossResourceClientEventTrigger(Client sender, string eventName, object[] arguments)
    {
        onCrossResourceClientEventTrigger?.Invoke(sender, eventName, arguments);
    }

    private static void myonClientEventTrigger(Client sender, string eventName, object[] arguments)
    {
        onClientEventTrigger?.Invoke(sender, eventName, arguments);
    }

    private static void myonPlayerRespawn(Client player)
    {
        onPlayerRespawn?.Invoke(player);
    }

    private static void myonChatCommand(Client sender, string command, CancelEventArgs cancel)
    {
        onChatCommand?.Invoke(sender, command, cancel);
    }

    private static void myonPlayerDisconnected(Client player, string reason)
    {
        onPlayerDisconnected?.Invoke(player, reason);
    }

    private static void myonPlayerFinishedDownload(Client player)
    {
        onPlayerFinishedDownload?.Invoke(player);
    }

    private static void myonPlayerConnected(Client player)
    {
        onPlayerConnected?.Invoke(player);
    }

    private static void myonPlayerBeginConnect(Client player, CancelEventArgs cancelConnection)
    {
        onPlayerBeginConnect?.Invoke(player, cancelConnection);
    }

    private static void myonChatMessage(Client sender, string message, CancelEventArgs cancel)
    {
        onChatMessage?.Invoke(sender, message, cancel);
    }

    private static void myonUpdate()
    {
        onUpdate?.Invoke();
    }

    private static void myonResourceStop()
    {
        onResourceStop?.Invoke();
    }

    private static void myonResourceStart()
    {
        onResourceStart?.Invoke();
    }

    private static void myonEntityEnterColShape(ColShape colshape, NetHandle entity)
    {
        onEntityEnterColShape?.Invoke(colshape, entity);
    }

    private static void myonVehicleDeath(NetHandle entity)
    {
        onVehicleDeath?.Invoke(entity);
    }

    private static void myonVehicleWindowSmash(NetHandle entity, int index)
    {
        onVehicleWindowSmash?.Invoke(entity, index);
    }

    private static void myonEntityExitColShape(ColShape colshape, NetHandle entity)
    {
        onEntityExitColShape?.Invoke(colshape, entity);
    }

    private static void myonEntityDataChange(NetHandle entity, string key, object oldValue)
    {
        onEntityDataChange?.Invoke(entity, key, oldValue);
    }

    private static void myonPlayerWaypointChange(Client player, bool placed, Vector3 position)
    {
        onPlayerWaypointChange?.Invoke(player, placed, position);
    }

    private static void myonPlayerDamaged(Client victim, Client enemy, int weaponDamageCause, int boneHitId)
    {
        onPlayerDamaged?.Invoke(victim, enemy, weaponDamageCause, boneHitId);
    }

    private static void myonEntityMovingRotationFinished(NetHandle entity)
    {
        onEntityMovingRotationFinished?.Invoke(entity);
    }

    private static void myonEntityMovingPositionFinished(NetHandle entity)
    {
        onEntityMovingPositionFinished?.Invoke(entity);
    }

    private static void myonPlayerDetonateStickies(Client player)
    {
        throw new NotImplementedException();
    }

    private static void myonPlayerModelChange(Client player, int oldValue)
    {
        onPlayerModelChange?.Invoke(player, oldValue);
    }

    private static void myonVehicleTrailerChange(NetHandle tower, NetHandle trailer)
    {
        onVehicleTrailerChange?.Invoke(tower, trailer);
    }

    private static void myonVehicleTyreBurst(NetHandle entity, int index)
    {
        onVehicleTyreBurst?.Invoke(entity, index);
    }

    private static void myonVehicleDoorBreak(NetHandle entity, int index)
    {
        onVehicleDoorBreak?.Invoke(entity, index);
    }

    private static void myonVehicleSirenToggle(NetHandle entity, bool oldValue)
    {
        onVehicleSirenToggle?.Invoke(entity, oldValue);
    }

    private static void myonPlayerWeaponAmmoChange(Client player, WeaponHash weapon, int oldValue)
    {
        onPlayerWeaponAmmoChange?.Invoke(player, weapon, oldValue);
    }

    private static void myonPlayerWeaponSwitch(Client player, WeaponHash oldValue)
    {
        onPlayerWeaponSwitch?.Invoke(player, oldValue);
    }

    private static void myonPlayerArmorChange(Client player, int oldValue)
    {
        onPlayerArmorChange?.Invoke(player, oldValue);
    }

    private static void myonPlayerHealthChange(Client player, int oldValue)
    {
        onPlayerHealthChange?.Invoke(player, oldValue);
    }

    private static void myonVehicleHealthChange(NetHandle entity, float oldValue)
    {
        onVehicleHealthChange?.Invoke(entity, oldValue);
    }

    private static void myonServerResourceEvent(string resourceName, string eventName, object[] arguments)
    {
        onServerResourceEvent?.Invoke(resourceName, eventName, arguments);
    }

    private static void myonServerResourceStop(string resourceName)
    {
        onServerResourceStop?.Invoke(resourceName);
    }

    private static void myonServerResourceStart(string resourceName)
    {
        onServerResourceStart?.Invoke(resourceName);
    }

    private static void myonEntityDataReset(NetHandle entity, string key)
    {
        onEntityDataReset?.Invoke(entity, key);
    }

}

#if RAGEMP
    public class CancelEventArgs
    {
        public CancelEventArgs() { }
        public CancelEventArgs(bool cancel) { Cancel = cancel; }

        public bool Cancel { get; set; }
        public string Reason { get; set; }
    }


    public static class RageMPExtensions
    {
        public static T GetSetting<T>(this API api, string key) => api.GetSetting<T>(GameMode.Instance, key);

        public static void SetVehicleDirtLevel(this API api, Vehicle vehicle, float dirtLevel)
        {
            // Unsupported
        }

        public static float GetVehicleDirtLevel(this API api, Vehicle vehicle)
        {
            // Unsupported
            return 0f;
        }

        public static System.Timers.Timer Delay(this API api, int ms, bool onlyOnce, Action action)
        {
            if (onlyOnce)
            {
                Task.Delay(ms).ContinueWith((t) => action());
                return null;
            }
            else
            {
                var t = new System.Timers.Timer(ms);
                t.Elapsed += (s, e) => action();
                t.Start();
                return t;
            }
        }

        public static void StopTimer(this API api, System.Timers.Timer timer)
        {
            timer.Stop();
        }
    }
#endif
}
