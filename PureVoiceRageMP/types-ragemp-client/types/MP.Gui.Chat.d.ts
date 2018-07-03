/// <reference path="../index.d.ts" />

declare interface MpGuiChat {
    safe: boolean;
    colors: boolean;

    push(...value: any[]): any; // TODO: Missing documentation;
    activate(value: boolean): any;
    show(...value: any[]): any; // TODO: Missing documentation;
}