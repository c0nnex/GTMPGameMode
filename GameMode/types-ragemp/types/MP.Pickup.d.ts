/// <reference path="../index.d.ts" />

declare interface MpPickup extends MpEntity {
    pickupHash: number;
}

declare interface MpPickupPool extends MpPool<MpPickup> {
    'new'(...args: any[]): MpPickup; // TODO: Missing documentation;
}
