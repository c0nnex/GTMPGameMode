/// <reference path="../index.d.ts" />

declare interface MpGameFire {
    stopFireInRange(x: number, y: number, z: number, radius: number): void;
    getPedInsideExplosionArea(explosionType: number, x1: number, y1: number, z1: number, x2: number, y2: number, z2: number, radius: number): MpEntity;
    isExplosionInArea(explosionType: number, x1: number, y1: number, z1: number, x2: number, y2: number, z2: number): boolean;
    getClosestFirePos(outPosition: MpVector3, x: number, y: number, z: number): MpVector3;
    addSpecfxExplosion(x: number, y: number, z: number, explosionType: number, explosionFx: string | number, damageScale: number, isAudible: boolean, isInvisible: boolean, cameraShake: number): void;
    getNumberOfFiresInRange(x: number, y: number, z: number, radius: number): number;
    startScriptFire(x: number, y: number, z: number, maxChildren: number, isGasFire: boolean): number;
    removeScriptFire(fireHandle: number): void;
    isExplosionInAngledArea(explosionType: number, x1: number, y1: number, z1: number, x2: number, y2: number, z2: number, angle: number): boolean;
    isExplosionInSphere(explosionType: number, x: number, y: number, z: number, radius: number): boolean;
}
