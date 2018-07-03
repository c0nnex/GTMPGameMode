/// <reference path="../index.d.ts" />

declare interface MpGameDatafile {
    objectValueGetType(objectData: object, key: string): number;
    arrayValueGetSize(arrayData: object): number;
    objectValueGetFloat(objectData: object, key: string): number;
    objectValueGetBoolean(objectData: object, key: string): boolean;
    arrayValueAddString(arrayData: object, value: string): void;
    objectValueAddBoolean(arrayData: object, key: string, value: boolean): void;
    arrayValueGetType(arrayData: object, arrayIndex: number): number;
    objectValueGetString(objectData: object, key: string): string;
    arrayValueGetInteger(arrayData: object, arrayIndex: number): number;
    arrayValueAddVector3(arrayData: object, valueX: number, valueY: number, valueZ: number): void;
    objectValueGetVector3(objectData: object, key: string): MpVector3;
    objectValueAddVector3(objectData: object, key: string, valueX: number, valueY: number, valueZ: number): void;
    arrayValueGetBoolean(arrayData: object, arrayIndex: number): boolean;
    arrayValueAddFloat(arrayData: object, value: number): void;
    objectValueAddArray(objectData: object, key: string): void;
    arrayValueAddObject(arrayData: object): void;
    objectValueGetInteger(objectData: object, key: string): number;
    objectValueGetArray(objectData: object, key: string): object;
    arrayValueGetObject(arrayData: object, arrayIndex: number): object;
    arrayValueGetVector3(arrayData: object, arraayIndex: number): MpVector3;
    objectValueAddString(objectData: object, key: string, value: string): void;
    objectValueAddObject(objectData: object, key: string): void;
    objectValueGetObject(objectData: object, key: string): void;
    arrayValueGetFloat(arrayData: object, arrayIndex: number): number;
    objectValueAddFloat(objectData: object, key: string, value: number): void;
    loadUgcFile(filename: string): boolean;
    arrayValueAddInteger(arrayData: object, value: number): void;
    arrayValueGetString(arrayData: object, arrayIndex: number): string;
    objectValueAddInteger(objectData: object, key: string, value: number): void;
    arrayValueAddBoolean(arrayData: object, value: boolean): void;
}
