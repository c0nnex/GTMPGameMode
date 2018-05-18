/// <reference path="../index.d.ts" />

declare interface MpTextLabel extends MpEntity {
    color: [number, number, number, number];
    drawDistance: number;
    los: boolean;
    text: string;
}

declare interface MpTextLabelPool extends MpPool<MpTextLabel> {
    "new"(text: string, position: MpVector3, options?: MpTextLabelOptions): MpTextLabel;
}

declare interface MpTextLabelOptions {
    color: [number, number, number, number];
    dimension: number;
    drawDistance: number;
    font: number;
    los: boolean;
}
