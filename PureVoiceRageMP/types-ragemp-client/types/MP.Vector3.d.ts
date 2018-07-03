/// <reference path="../index.d.ts" />

declare interface MpVector3 {
    x: number;
    y: number;
    z: number;

    'new'(x: number, y: number, z: number): MpVector3;
}

declare interface Vector3 {
    new( x: number, y: number, z: number ): MpVector3;
}