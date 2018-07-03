/// <reference path="../index.d.ts" />

declare interface MpQuaternion {
    x: number;
    y: number;
    z: number;
    w: number;

    'new'(x: number, y: number, z: number, w: number): MpQuaternion;
}
