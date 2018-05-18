/// <reference path="../index.d.ts" />

declare interface ScriptContext {
    versions: MpVersions;
    config: MpConfig;
    world: MpWorld;
    events: MpEvents;

    blips: MpBlipPool;
    checkpoints: MpCheckpointPool;
    colshapes: MpColshapePool;
    markers: MpMarkerPool;
    objects: MpObjectPool;
    pickups: MpPickupPool;
    players: MpPlayerPool;
    vehicles: MpVehiclePool;
    labels: MpTextLabelPool;

    joaat(value: string): number;
    joaat(value: string[]): number[];
}
