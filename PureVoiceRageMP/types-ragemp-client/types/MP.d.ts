/// <reference path="../index.d.ts" />

declare interface ScriptContext {
    readonly players: MpPlayerPool;
    readonly vehicles: MpVehiclePool;
    readonly objects: MpObjectPool;
    readonly pickups: MpPickupPool;
    readonly blips: MpBlipPool;
    readonly gui: MpGui;
    readonly keys: MpKeys;
    readonly markers: MpMarkerPool;
    readonly checkpoints: MpCheckpointPool;
    readonly cameras: MpCameraPool;
    readonly browsers: MpBrowserPool;
    readonly colshapes: MpColshapePool;
    readonly events: MpEvents;
    readonly game: MpGame;
    readonly nametags: MpNametags;
    readonly raycasting: MpRaycasting;
    readonly discord: MpDiscord;
    readonly Vector3: Vector3;
}
