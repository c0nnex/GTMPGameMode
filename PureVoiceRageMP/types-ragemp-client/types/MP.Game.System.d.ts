/// <reference path="../index.d.ts" />

declare interface MpGameSystem {
    sin(value: number): number;
    ceil(value: number): number;
    vdist(x1: number, y1: number, z1: number, x2: number, y2: number, z2: number): number;
    wait(ms: number): void;
    settimerb(value: number): void;
    vmag(p0: number, p1: number, p2: number): number;
    sqrt(value: number): number;
    shiftRight(value: number, bitShift: number): number;
    vmag2(p0: number, p1: number, p2: number): number;
    vdist2(x1: number, y1: number, z1: number, x2: number, y2: number, z2: number): number;
    startNewScriptWithArgs(scriptName: string, args: object, argCount: number, stackSize: number): number;
    toFloat(value: number): number;
    settimera(value: number): void;
    startNewStreamedScriptWithArgs(scriptHash: string | number, args: object, argCount: number, stackSize: number): number;
    cos(value: number): number;
    pow(base: number, exponent: number): number;
    startNewScript(scriptName: string, stackSize: number): number;
    startNewStreamedScript(scriptHash: string | number, stackSize: number): number;
    shiftLeft(value: number, bitShift: number): number;
    round(value: number): number;
    floor(value: number): number;
}
