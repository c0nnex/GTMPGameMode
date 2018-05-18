/// <reference path="../index.d.ts" />

declare interface MpGuiCursor {
    visible: boolean;

    update(...args: any[]): any;
}