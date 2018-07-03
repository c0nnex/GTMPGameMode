/// <reference path="../index.d.ts" />

declare interface MpGameItemset {
    removeFromItemset(p0: object, p1: object): void;
    isInItemset(p0: object, p1: object): boolean;
    createItemset(p0: boolean): object;
    cleanItemset(p0: object): void;
    getIndexedItemInItemset(p0: object, p1: object): object;
    isItemsetValid(p0: object): boolean;
    getItemsetSize(p0: object): object;
    destroyItemset(p0: object): void;
    addToItemset(p0: object, p1: object): boolean;
}
