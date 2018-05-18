/// <reference path="../index.d.ts" />

declare interface MpGameWeapon {
    getWeaponComponentTypeModel(componentHash: string | number): string | number;
    giveWeaponComponentToWeaponObject(weaponObject: MpObject, addonHash: string | number): void;
    hasWeaponAssetLoaded(weaponHash: string | number): boolean;
    getWeaponDamageType(weaponHash: string | number): number;
    getWeapontypeSlot(weaponHash: string | number): string | number;
    requestWeaponHighDetailModel(weaponObject: MpObject | object): void;
    requestWeaponAsset(weaponHash: string | number, p1: number, p2: number): void;
    getWeaponClipSize(weaponHash: string | number): number;
    doesWeaponTakeWeaponComponent(weaponHash: string | number, componentHash: string | number): boolean;
    getWeaponTintCount(weaponHash: string | number): number;
    hasVehicleGotProjectileAttached(driver: MpPed, vehicle: MpVehicle, weapon: number | string, p3: any): boolean;
    hasWeaponGotWeaponComponent(weapon: MpObject, addonHash: string | number): boolean;
    isWeaponValid(weaponHash: string | number): boolean;
    createWeaponObject(weaponHash: string | number, ammoCount: number, x: number, y: number, z: number, showWorldModel: boolean, heading: number, p7: any): MpObject;
    setPedAmmoToDrop(p0: any, p1: any): void;
    removeWeaponAsset(weaponHash: string | number): void;
    giveWeaponObjectToPed(weaponObject: MpObject, ped: MpPed): void;
    getWeaponComponentHudStats(p0: any, p1: any): boolean;
    canUseWeaponOnParachute(weaponHash: string | number): boolean;
    getWeapontypeGroup(weaponHash: string | number): string | number;
    enableLaserSightRendering(toggle: boolean): void;
    getWeaponObjectTintIndex(weapon: MpEntity): number;
    setFlashLightFadeDistance(distance: number): void;
    getWeaponHudStats(weaponHash: string | number, data: {
        hudDamage: number;
        hudSpeed: number;
        hudCapacity: number;
        hudAccuracy: number;
        hudRange: number;
    }): boolean;
    getWeapontypeModel(weaponHash: string | number): string | number;
    removeWeaponComponentFromWeaponObject(p0: any, p1: any): void;
    setWeaponObjectTintIndex(weapon: MpEntity, tint: number): void;
    removeAllProjectilesOfType(weaponhash: string | number, p1: boolean): void;
}
