/// <reference path="../index.d.ts" />

declare interface MpGameWater {
    testVerticalProbeAgainstAllWater(x: number, y: number, z: number, p3: object, p4: object): boolean;
    testProbeAgainstAllWater(p0: object, p1: object, p2: object, p3: object, p4: object, p5: object, p6: object, p7s: object): boolean;
    getWaterHeightNoWaves(x: number, y: number, z: number, height: number): void;
    setWavesIntensity(intensity: number): void;
    modifyWater(x: number, y: number, radius: number, height: number): void;
    getWaterHeight(x: number, y: number, z: number, height: number): number;
    testProbeAgainstWater(startX: number, startY: number, startZ: number, endX: number, endY: number, endZ: number, p6: boolean): boolean;
}
