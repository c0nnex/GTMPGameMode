/// <reference path="../index.d.ts" />

declare interface MpColshape extends MpEntity {
    readonly shapeType: string;

    isPointWithin(position: MpVector3): boolean;
}

declare interface MpColshapePool extends MpPool<MpColshape> {
    newCircle(x: number, y: number, radius: number, dimension?: number): MpColshape;
    newSphere(x: number, y: number, z: number, radius: number, dimension?: number): MpColshape;
    newTube(x: number, y: number, z: number, radius: number, height: number, dimension?: number): MpColshape;
    newRectangle(x: number, y: number, width: number, height: number, dimension?: number): MpColshape;
    newCube(x: number, y: number, z: number, width: number, depth: number, height: number, dimension?: number): MpColshape;
}