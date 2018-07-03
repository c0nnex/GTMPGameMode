/// <reference path="../index.d.ts" />

declare interface MpGameInterior {
    getInteriorAtCoordsWithType(x: number, y: number, z: number, interiorType: string): number;
    unpinInterior(interiorId: number): void;
    isValidInterior(interiorId: number): boolean;
    isInteriorPropEnabled(interiorId: number, propName: string): boolean;
    addPickupToInteriorRoomByName(pickup: MpPickup, roomName: string): void;
    refreshInterior(interiorId: number): void;
    disableInteriorProp(interiorId: number, propName: string): void;
    enableInteriorProp(interiorId: number, propName: string): void;
    disableInterior(interiorId: number, toggle: boolean): void;
    isInteriorReady(interiorId: number): boolean;
    isInteriorCapped(interiorId: number): boolean;
    getOffsetFromInteriorInWorldCoords(interiorId: number, x: number, y: number, z: number): MpVector3;
    hideMapObjectThisFrame(mapObjectHash: string): void;
    hideMapObjectThisFrame(mapObjectHash: number): void;
    getInteriorAtCoords(x: number, y: number, z: number): number;
    isInteriorDisabled(interiorId: number): boolean;
    capInterior(interiorId: number, toggle: boolean): void;
    getInteriorGroupId(interiorId: number): number;
    getInteriorFromCollision(x: number, y: number, z: number): number;
    areCoordsCollidingWithExterior(x: number, y: number, z: number): boolean;
    unkGetInteriorAtCoords(x: number, y: number, z: number, unk: number): number;
}
