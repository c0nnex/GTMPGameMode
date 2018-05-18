/// <reference path="../index.d.ts" />

declare interface MpPool<TEntity> {
    readonly length: number;
    readonly size: number;

    apply(func: (...args: any[]) => void, ...args: any[]): void;
    at(index: number): TEntity;
    exists(entity: TEntity): boolean;
    forEach(func: (entity: TEntity, index?: number) => void): void;
    forEachInRange(position: MpVector3, range: number, func: (entity: TEntity, index?: number) => void): void;
    forEachInRange(position: MpVector3, range: number, dimension: number, func: (entity: TEntity, index?: number) => void): void;
    forEachInDimension(dimension: number, func: (entity: TEntity, index?: number) => void): void;
    toArray(): TEntity[];
}
