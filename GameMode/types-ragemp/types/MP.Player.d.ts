/// <reference path="../index.d.ts" />

declare interface MpPlayer extends MpEntity {
    readonly action: string;
    readonly isJumping: boolean;
    readonly isInCover: boolean;
    readonly isClimbing: boolean;
    readonly isEnteringVehicle: boolean;
    readonly isLeavingVehicle: boolean;
    readonly vehicle: MpVehicle;
    readonly seat: number;
    readonly weapon: any;
    readonly isAiming: number;
    readonly aimTarget: any;
    readonly ping: number;
    readonly ip: string;
    readonly streamedPlayers: MpPlayer[];
    readonly allWeapons: number[];

    name: string;
    heading: number;
    health: number;
    armour: number;
    eyeColour: number;
    hairColour: number;
    hairHighlightColour: number;

    kick(reason: string): void;
    ban(reason: string): void;
    spawn(pos: MpVector3): void;
    giveWeapon(weaponHash: number | number[], ammo: number): void;
    resetWeapon(...args: any[]): any; // TODO: Missing documentation
    outputChatBox(message: string): void;
    getClothes(componentNumber: number): {
        readonly drawable: number;
        readonly texture: number;
        readonly palette: number;
    };
    setClothes(componentNumber: number, drawable: number, texture: number, palette: number): void;
    getProp(propId: number): {
        readonly drawable: number;
        readonly texture: number;
        readonly palette: number;
    };
    setProp(propId: number, drawable: number, texture: number): void;
    putIntoVehicle(vehicle: MpVehicle, seat: number): void;
    removeFromVehicle(): void;
    invoke(hash: string, ...args: any[]): void;
    call(eventName: string, ...args: any[]): void;
    notify(message: string): void;
    getHeadBlend(): {
        readonly shape: number[];
        readonly skin: number[];
        readonly shapeMix: number;
        readonly skinMix: number;
        readonly thirdMix: number;
    };
    setHeadBlend(shapeFirstId: number, shapeSecondId: number, shapeThirdId: number, skinFirstId: number, skinSecondId: number, skinThirdId: number, shapeMix: number, skinMix: number, thirdMix: number): void;
    updateHeadBlend(shapeMix: number, skinMix: number, thirdMix: number): void;
    setFaceFeature(id: number, scale: number): void;
    getFaceFeature(id: number): number;
    setHairColour(firstColor: number, secondColor: number): void;
    playAnimation(dictionary: string, name: string, speed: number, flag: number): void;
    playScenario(scenario: string): void;
    stopAnimation(): void;
    isStreamedFor(object: MpPlayer): boolean;
    removeObject(object: any): void; // TODO: Missing documentation
}

declare interface MpPlayerPool extends MpPool<MpPlayer> {
    broadcast(text: string): void;
    broadcastInRange(position: MpVector3, range: number, text: string): void;
    broadcastInRange(position: MpVector3, range: number, dimension: number, text: string): void;
    broadcastInDimension(dimension: number, text: string): void;
    call(eventName: string, ...args: any[]): void;
    callInRange(position: MpVector3, range: number, eventName: string, ...args: any[]): void;
    callInRange(position: MpVector3, range: number, dimension: number, eventName: string, ...args: any[]): void;
    callInDimension(dimension: number, eventName: string, ...args: any[]): void;
}
