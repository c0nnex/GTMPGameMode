/// <reference path="../index.d.ts" />

declare interface MpBlip extends MpEntity {
    setColour(color: number): void;
    setNameToPlayerName(player: MpPlayer): void;
    setShowCone(toggle: boolean): void;
    setSecondaryColour(r: number, g: number, b: number): void;
    getNextInfoId(): MpBlip;
    getFirstInfoId(): MpBlip;
    getInfoIdDisplay(): number;
    getSprite(): number;
    setCategory(index: number): void;
    setAsMissionCreator(toggle: boolean): void;
    isMissionCreator(): boolean;
    setFade(opacity: number, duration: number): void;
    setFlashesAlternate(toggle: boolean): void;
    setAlpha(alphaLevel: string, skin: boolean): void;
    setAlpha(alpha: number): void;
    getInfoIdEntityIndex(): MpEntity | object;
    setRoute(enabled: boolean): void;
    hideNumberOn(): void;
    getCoords(): MpVector3;
    setShowHeadingIndicator(toggle: boolean): void;
    setAsFriendly(toggle: boolean): void;
    getHudColour(): number;
    pulse(): void;
    addTextComponentSubstringName(): void;
    setRouteColour(colour: number): void;
    setDisplay(displayId: number): void;
    getAlpha(): number;
    getInfoIdPickupIndex(): MpPickup;
    showNumberOn(number: number): void;
    isFlashing(): boolean;
    doesExist(): boolean;
    setFlashInterval(p1: object): void;
    setCoords(posX: number, posY: number, posZ: number): void;
    setPriority(priority: number): void;
    setFlashes(toggle: boolean): void;
    setBright(toggle: boolean): void;
    endTextCommandSetName(): void;
    setAsShortRange(toggle: boolean): void;
    getInfoIdType(): number;
    setScale(scale: number): void;
    setFlashTimer(duration: number): void;
    isShortRange(): boolean;
    getColour(): number;
    setSprite(spriteId: number): void;
    setHighDetail(toggle: boolean): void;
    isOnMinimap(): boolean;
    setNameFromTextFile(gxtEntry: string): void;
    setRotation(rotation: number): void;
}

declare interface MpBlipPool extends MpPool<MpBlip> {

}