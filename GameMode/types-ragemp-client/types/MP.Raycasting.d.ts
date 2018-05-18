/// <reference path="../index.d.ts" />

declare interface MpRaycasting {
    testPointToPoint(pos1: MpVector3, pos2: MpVector3, ignoreEntity?: MpEntity, flags?: any[]);
    testCapsule(pos1: MpVector3, pos2: MpVector3, radius: any, ignoreEntity?: MpEntity, flags?: any[]);
}