/// <reference path="../index.d.ts" />

declare interface MpGameWorldprobe {
    startShapeTestCapsule(x1: number, y1: number, z1: number, x2: number, y2: number, z2: number, radius: number, flags: number, ignoreEntity: MpEntity, p9: number): number;
    castRayPointToPoint(x1: number, y1: number, z1: number, x2: number, y2: number, z2: number, flags: number, ignoreEntity: MpEntity, p8: number): number;
    getShapeTestResult(rayHandle: number, hit: boolean, endCoords: MpVector3, surfaceNormal: MpVector3, entityHit: MpEntity): {
        readonly hit: boolean;
        readonly endCoords: MpVector3;
        readonly surfaceNormal: MpVector3;
        readonly entityHit: MpEntity;
    };
    getShapeTestResultEx(rayHandle: number, hit: boolean, endCoords: MpVector3, surfaceNormal: MpVector3, _materialHash: number, entityHit: MpEntity): {
        readonly hit: boolean;
        readonly endCoords: MpVector3;
        readonly surfaceNormal: MpVector3;
        readonly _materialHash: number;
        readonly entityHit: MpEntity;
    };
    startShapeTestLosProbe(x1: number, y1: number, z1: number, x2: number, y2: number, z2: number, flags: number, ignoreEntity: MpEntity, p8: number): number;
    startShapeTestBox(x1: number, y1: number, z1: number, x2: number, y2: number, z2: number, rotX: number, rotY: number, rotZ: number, p9: object, p10: object, ignoreEntity: MpEntity, p12: object): number;
}
