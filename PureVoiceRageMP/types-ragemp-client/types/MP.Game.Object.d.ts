/// <reference path="../index.d.ts" />

declare interface MpGameObject {
    createMoneyPickups(x: number, y: number, z: number, value: number, amount: number, model: string | number): void;
    createPortablePickup2(pickupHash: string | number, x: number, y: number, z: number, placeOnGround: boolean, modelHash: string | number): MpPickup;
    getObjectOffsetFromCoords(xPos: number, yPos: number, zPos: number, heading: number, xOffset: number, yOffset: number, zOffset: number): MpVector3;
    getPickupCoords(p0: object): number;
    removeAllPickupsOfType(p0: object): void;
    isPointInAngledArea(p0: number, p1: number, p2: number, p3: number, p4: number, p5: number, p6: number, p7: number, p8: number, p9: number, p10: boolean, p11: boolean): boolean;
    createPortablePickup(pickupHash: string | number, x: number, y: number, z: number, placeOnGround: boolean, modelHash: string | number): MpPickup;
    removePickup(pickup: MpPickup): void;
    highlightPlacementCoords(x: number, y: number, z: number, colorIndex: number): void;
    isAnyObjectNearPoint(x: number, y: number, z: number, range: number, p4: boolean): boolean;
    removeDoorFromSystem(doorHash: string | number): void;
    createObject(modelHash: string | number, x: number, y: number, z: number, networkHandle: boolean, createHandle: boolean, dynamic: boolean): MpObject;
    deleteObject(object: MpObject | object): MpObject | object;
    setTeamPickupObject(p0: object, p1: object, p2: object): void;
    createAmbientPickup(pickupHash: string | number, posX: number, posY: number, posZ: number, p4: number, value: number, modelHash: string | number, p7: boolean, p8: boolean): MpPickup;
    setDoorAccelerationLimit(doorHash: string | number, limit: number, p2: boolean, p3: boolean): void;
    getSafePickupCoords(x: number, y: number, z: number, p3: object, p4: object): MpVector3;
    addDoorToSystem(doorHash: string | number, modelHash: string | number, x: number, y: number, z: number, p5: number, p6: number, p7: number): void;
    hasClosestObjectOfTypeBeenBroken(p0: number, p1: number, p2: number, p3: number, modelHash: string | number, p5: object): boolean;
    setPickupRegenerationTime(p0: object, p1: object): void;
    hasPickupBeenCollected(p0: object): boolean;
    createPickupRotate(pickupHash: string | number, posX: number, posY: number, posZ: number, rotX: number, rotY: number, rotZ: number, flag: number, amount: number, p9: object, p10: boolean, modelHash: string | number): MpPickup;
    isObjectNearPoint(objectHash: string | number, x: number, y: number, z: number, range: number): boolean;
    isGarageEmpty(garage: object, p1: boolean, p2: number): boolean;
    createObjectNoOffset(modelHash: string | number, x: number, y: number, z: number, networkHandle: boolean, createHandle: boolean, dynamic: boolean): MpObject;
    doorControl(doorHash: string | number, x: number, y: number, z: number, locked: boolean, p5: number, p6: number, p7: number): void;
    doesPickupExist(p0: object): boolean;
    trackObjectVisibility(p0: object): void;
    setDoorAjarAngle(doorHash: string | number, ajar: number, p2: boolean, p3: boolean): void;
    getObjectFragmentDamageHealth(p0: object, p1: boolean): number;
    doesObjectOfTypeExistAtCoords(x: number, y: number, z: number, radius: number, hash: string | number, p5: boolean): boolean;
    doesDoorExist(doorHash: string | number): boolean;
    isDoorClosed(door: string | number): boolean;
    doesPickupObjectExist(p0: object): boolean;
    getClosestObjectOfType(x: number, y: number, z: number, radius: number, modelHash: string | number, isMissing: boolean, p6: boolean, p7: boolean): MpObject;
    getStateOfClosestDoorOfType(type: string | number, x: number, y: number, z: number, locked: boolean, heading: number): {
        readonly locked: boolean;
        readonly heading: boolean;
    };
    setForceObjectThisFrame(p0: object, p1: object, p2: object, p3: object): void;
    setStateOfClosestDoorOfType(type: string | number, x: number, y: number, z: number, locked: boolean, heading: number, p6: boolean): void;
    isPickupWithinRadius(pickupHash: string | number, x: number, y: number, z: number, radius: number): boolean;
    createPickup(pickupHash: string | number, posX: number, posY: number, posZ: number, p4: number, value: number, p6: boolean, modelHash: string | number): MpPickup;
}
