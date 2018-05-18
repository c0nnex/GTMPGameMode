/// <reference path="../index.d.ts" />

declare interface MpEntity {
    readonly id: number;
    readonly type: string

    alpha: number;
    dimension: number;
    model: number;
    position: MpVector3;

    destroy(): void;
}