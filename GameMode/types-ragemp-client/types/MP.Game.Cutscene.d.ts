/// <reference path="../index.d.ts" />

declare interface MpGameCutscene {
    setCutscenePedPropVariation(cutsceneEntName: string, p1: number, p2: number, p3: number, modelHash: string | number): void;
    getEntityIndexOfCutsceneEntity(cutsceneEntName: string, modelHash: string | number): MpEntity;
    startCutscene(p0: number): void;
    startCutsceneAtCoords(x: number, y: number, z: number, p3: number): void;
    hasThisCutsceneLoaded(cutsceneName: string): boolean;
    doesCutsceneEntityExist(cutsceneEntName: string, modelHash: string | number): boolean;
    canSetExitStateForRegisteredEntity(cutsceneEntName: string, modelHash: string | number): boolean;
    canSetEnterForRegisteredEntity(cutsceneEntName: string, modelHash: string | number): boolean;
    requestCutscene(cutsceneName: string, p1: number): void;
    setCutsceneFadeValues(p0: boolean, p1: boolean, p2: boolean, p3: boolean): void;
    setCutsceneTriggerArea(p0: number, p1: number, p2: number, p3: number, p4: number, p5: number): void;
    canSetExitStateForCamera(p0: boolean): boolean;
    setCutsceneOrigin(p0: object, p1: object, p2: object, p3: object, p4: object): void;
    setCutscenePetComponentVariation(cutsceneEntName: string, p1: number, p2: number, p3: number, modelHash: string | number): void;
    getEntityIndexOfRegisteredEntity(cutsceneEntName: string, modelHash: string | number): MpEntity;
    requestCutscene2(cutsceneName: string, p1: number, p2: number): void;
    stopCutscene(p0: boolean): void;
    registerEntityForCutscene(cutscenePed: MpPed | object, cutsceneEntName: string, p2: number, modelHash: string | number, p4: number): void;
}
