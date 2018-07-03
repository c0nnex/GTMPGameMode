/// <reference path="../index.d.ts" />

declare interface MpGameBrain {
    registerObjectScriptBrain(stringName: string, p1: string | number, p2: number, p3: number, p4: number, p5: number): void;
    disableScriptBrainSet(brainSet: number): void;
    registerWorldPointScriptBrain(p0: object, p1: number, p2: object): void;
    addScriptToRandom(name: string, model: string | number, p2: number, p3: number): void;
    enableScriptBrainSet(brainSet: number): void;
    isObjectWithinBrainActivationRange(object: MpObject | object): boolean;
}
