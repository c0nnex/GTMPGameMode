/// <reference path="../index.d.ts" />

declare interface MpGameZone {
    getZoneAtCoords(x: number, y: number, z: number): number;
    getZonePopschedule(zoneId: number): number;
    clearPopscheduleOverrideVehicleModel(scheduleId: number): void;
    getZoneScumminess(zoneId: number): number;
    overridePopscheduleVehicleModel(scheduleId: number, vehicleHash: string): void;
    overridePopscheduleVehicleModel(scheduleId: number, vehicleHash: number): void;
    getHashOfMapAreaAtCoords(x: number, y: number, z: number): string;
    getHashOfMapAreaAtCoords(x: number, y: number, z: number): number;
    getZoneFromNameId(zoneName: string): number;
    setZoneEnabled(zoneId: number, toggle: boolean): void;
    getZoneOfName(x: number, y: number, z: number): string;
}
