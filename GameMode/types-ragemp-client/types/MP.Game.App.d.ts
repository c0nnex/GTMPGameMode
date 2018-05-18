/// <reference path="../index.d.ts" />

declare interface MpGameApp {
    appDeleteAppData(appName: string): boolean;
    appGetFloat(property: string): number;
    appGetString(property: string): string;
    appGetInt(property: string): number;
    appHasSyncedData(property: string): boolean;
    appSetApp(appName: string): void;
    appSetBlock(blockName: string): void;
    appSetFloat(property: string, value: number): void;
    appSetInt(property: string, value: number): void;
    appSetString(property: string, value: string): void;
}
