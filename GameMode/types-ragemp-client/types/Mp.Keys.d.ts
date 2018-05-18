/// <reference path="../index.d.ts" />

declare interface MpKeys {
    isUp( keyCode: number ): void;
    isDown( keyCode: number ): void;
    bind( keyCode: number, keyHold: boolean, handler: Function ): void;
    unbind( keyCode: number, handler: Function ): void;
}
