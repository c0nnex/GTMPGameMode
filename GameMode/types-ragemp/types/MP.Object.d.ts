/// <reference path="../index.d.ts" />

declare interface MpObject extends MpEntity {
    rotation: MpVector3;
}

declare interface MpObjectPool extends MpPool<MpObject> {
    'new'(model: number, position: MpVector3, options?: MpObjectOptions): MpObject;
}

declare interface MpObjectOptions {
    alpha: number;
    dimension: number;
    rotation: MpVector3;
}