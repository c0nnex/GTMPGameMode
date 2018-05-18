/// <reference path="../index.d.ts" />

declare interface MpGameEntity {
    removeModelSwap(x: number, y: number, z: number, radius: number, originModel: string | number, newModel: string | number, p6: boolean): void;
    findAnimEventPhase(animDictionary: string, animName: string, p2: string, p3: object, p4: object): boolean;
    stopSynchronizedMapEntityAnim(p0: number, p1: number, p2: number, p3: number, p4: object, p5: number): boolean;
    createForcedObject(x: number, y: number, z: number, p3: object, modelHash: string | number, p5: boolean): void;
    createModelHideExcludingScriptObjects(x: number, y: number, z: number, radius: number, model: string | number, p5: boolean): void;
    setObjectAsNoLongerNeeded(object: MpObject): MpObject;
    removeForcedObject(p0: object, p1: object, p2: object, p3: object, p4: object): void;
    isAnEntity(handle: number): boolean;
    createModelHide(x: number, y: number, z: number, radius: number, model: string | number, p5: boolean): void;
    createModelSwap(x: number, y: number, z: number, radius: number, originalModel: string | number, newModel: string | number, p6: boolean): void;
    playSynchronizedMapEntityAnim(p0: number, p1: number, p2: number, p3: number, p4: object, p5: object, p6: object, p7: object, p8: number, p9: number, p10: object, p11: number): boolean;
    removeModelHide(p0: object, p1: object, p2: object, p3: object, p4: object, p5: object): void;
    wouldEntityBeOccluded(hash: string | number, x: number, y: number, z: number, p4: boolean): boolean;
    getEntityAnimDuration(animDict: string, animName: string): number;
}
