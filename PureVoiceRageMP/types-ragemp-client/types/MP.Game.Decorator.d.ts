/// <reference path="../index.d.ts" />

declare interface MpGameDecorator {
    decorIsRegisteredAsType(propertyName: string, type: number): boolean;
    decorRegister(propertyName: string, type: number): void;
}
