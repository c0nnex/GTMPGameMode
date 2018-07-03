/// <reference path="../index.d.ts" />

declare interface MpGameDlc2 {
    nullify(variable: object, unused: object): boolean;
    isDlcPresent(dlcHash: string | number): boolean;
}
