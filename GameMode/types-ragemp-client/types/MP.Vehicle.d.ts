/// <reference path="../index.d.ts" />

declare interface MpVehicle extends MpEntity {
    gear: number;
    steeringAngle: number;
    rpm: number;

    removeHighDetailModel(): void;
    setCreatesMoneyPickupsWhenExploded(toggle: boolean): void;
    steerUnlockBias(toggle: boolean): void;
    getTrainCarriage(cariage: number): MpEntity | object;
    setRudderBroken(p0: boolean): void;
    detachFromCargobob(cargobob: MpVehicle | object): void;
    getWindowTint(): number;
    setFixed(): void;
    areAllWindowsIntact(): boolean;
    doesExtraExist(extraId: number): boolean;
    isInBurnout(): boolean;
    isAttachedToTowTruck(vehicle: MpVehicle | object): boolean;
    setSearchlight(toggle: boolean, canBeUsedByAI: boolean): void;
    setTrainCruiseSpeed(speed: number): void;
    isCargobobHookActive(): boolean;
    setFrictionOverride(friction: number): void;
    getVehicleTrailer(vehicle: MpVehicle): MpVehicle;
    getTrailer(vehicle: MpVehicle): MpVehicle;
    isHighDetail(): boolean;
    setModKit(modKit: number): void;
    setExtraColours(pearlescentColor: number, wheelColor: number): void;
    getPedUsingDoor(doorIndex: number): MpPed | object;
    resetWheels(toggle: boolean): void;
    setReduceGrip(toggle: boolean): void;
    isSeatFree(seatIndex: number): boolean;
    disablePlaneAileron(p0: boolean, p1: boolean): void;
    setEngineOn(value: boolean, instantly: boolean, otherwise: boolean): void;
    getNumberOfPassengers(): number;
    getDoorLockStatus(): number;
    doesHaveWeapon(): boolean;
    setHalt(distance: number, killEngine: boolean, unknown: boolean): void;
    getBoatAnchor(): boolean;
    getLayoutHash(): string | number;
    getClass(): number;
    isStoppedAtTrafficLights(): boolean;
    attachToTowTruck(vehicle: MpVehicle | object, rear: boolean, hookOffsetX: number, hookOffsetY: number, hookOffsetZ: number): void;
    setWheelsCanBreak(enabled: boolean): void;
    toggleMod(modType: number, toggle: boolean): void;
    setNeonLightEnabled(index: number, toggle: boolean): void;
    setHasBeenOwnedByPlayer(owned: boolean): void;
    getLivery(): number;
    isAnySeatEmpty(): boolean;
    setTimedExplosion(ped: MpPed | object, toggle: boolean): void;
    setDoorBreakable(doorIndex: number, isBreakable: boolean): void;
    setCanBeUsedByFleeingPeds(toggle: boolean): void;
    canShuffleSeat(p0: object): boolean;
    setRenderTrainAsDerailed(toggle: boolean): void;
    setIsConsideredByPlayer(toggle: boolean): void;
    setColourCombination(numCombos: number): void;
    getNumModKits(): number;
    setLights(state: number | boolean): void;
    closeBombBayDoors(): void;
    setCustomSecondaryColour(r: number, g: number, b: number): void;
    setCanBeTargetted(state: boolean): void;
    setDisablePetrolTankDamage(toggle: boolean): void;
    setPaintFade(fade: number): void;
    getNumberOfColours(): number;
    getExtraColours(pearlescentColor: number, wheelColor: number): {
        readonly pearlescentColor: number;
        readonly wheelColor: number;
    };
    attachToTrailer(trailer: MpVehicle, radius: number): void;
    setStrong(toggle: boolean): void;
    wasCounterActivated(p0: object): boolean;
    attachToCargobob(cargobob: MpVehicle | object, p1: number, x: number, y: number, z: number): void;
    isModel(model: string | number): boolean;
    setSteerBias(value: number): void;
    isAlarmActivated(): boolean;
    setModColor1(paintType: number, color: number, p2: number): void;
    releasePreloadMods(): void;
    setEngineHealth(health: number): void;
    setDisablePetrolTankFires(toggle: boolean): void;
    isBumberBrokenOff(front: boolean): boolean;
    isWindowIntact(windowIndex: number): boolean;
    setWheelType(wheelType: number): void;
    getModColor2TextLabel(): string;
    setOnGroundProperly(): boolean;
    isStolen(): boolean;
    isDriveable(p0: boolean): boolean;
    setCanBeVisiblyDamaged(state: boolean): void;
    isSirenOn(): boolean;
    getDeformationAtPos(offsetX: number, offsetY: number, offsetZ: number): MpVector3;
    setColours(colorPrimary: number, colorSecondary: number): void;
    setDoorsLockedForPlayer(player: MpPlayer, toggle: boolean): void;
    getModSlotName(modType: number): string;
    setCanRespray(state: boolean): void;
    isAConvertible(p0: boolean): boolean;
    getSuspensionHeight(): number;
    clearCustomPrimaryColour(): void;
    isStopped(): boolean;
    setPedEnabledBikeRingtone(p0: object): boolean;
    setWindowTint(tint: number): void;
    doesHaveStuckVehicleCheck(): boolean;
    setMissionTrainCoords(x: number, y: number, z: number): void;
    setTaxiLight(state: boolean): void;
    setCanBreak(toggle: boolean): void;
    setProvidesCover(toggle: boolean): void;
    setAllowNoPassengersLockon(toggle: boolean): void;
    getAcceleration(): number;
    getIsLeftHeadlightDamaged(): boolean;
    clearCustomSecondaryColour(): void;
    rollUpWindow(windowIndex: number): void;
    setLivery(livery: number): void;
    getModKit(): number;
    trackVisibility(): void;
    getTyresCanBurst(): boolean;
    isStuckTimerUp(p0: number, p1: number): boolean;
    setIsStolen(isStolen: boolean): void;
    setHandbrake(toggle: boolean): void;
    getColourCombination(): number;
    setMod(modType: number, modIndex: number, customTires: boolean): void;
    detachWindscreen(): void;
    setHelicopterRollPitchYawMult(multiplier: number): void;
    isCargobobMagnetActive(): boolean;
    setTyreFixed(tyreIndex: number): void;
    setPetrolTankHealth(fix: number): void;
    setCustomPrimaryColour(r: number, g: number, b: number): void;
    setExplodesOnHighExplosionDamage(toggle: boolean): void;
    isTaxiLightOn(): boolean;
    setBoatAnchor(toggle: boolean): void;
    getNeonLightsColour(r: number, g: number, b: number): {
        readonly r: number;
        readonly g: number;
        readonly b: number;
    };
    fixWindow(index: number): void;
    getMod(modType: number): number;
    setDoorsShut(closeInstantly: boolean): void;
    explodeInCutscene(p0: boolean): void;
    setDirtLevel(dirtLevel: number): void;
    rollDownWindow(windowIndex: number): void;
    enableCargobobHook(state: number): void;
    setDoorOpen(doorIndex: number, loose: boolean, openInstantly: boolean): void;
    getNumberPlateText(): string;
    getPetrolTankHealth(): number;
    setExtra(extraId: number, toggle: boolean): void;
    getModColor2(paintType: number, color: number): {
        readonly paintType: number;
        readonly color: number;
        readonly p2: number;
    };
    setModColor2(paintType: number, color: number): void;
    getCustomSecondaryColour(r: number, g: number, b: number): {
        readonly r: number;
        readonly g: number;
        readonly b: number;
    };
    getLastPedInSeat(seatIndex: number): MpPed | object;
    isToggleModOn(modType: number): boolean;
    rollDownWindows(): void;
    getAttachedToCargobob(): MpVehicle | object;
    getLiveryCount(): number;
    openBombBayDoors(): void;
    getModTextLabel(modType: number, modValue: number): string;
    setGravity(toggle: boolean): void;
    setUndriveable(toggle: boolean): void;
    doesHaveRoof(): boolean;
    setFullbeam(toggle: boolean): void;
    setAutomaticallyAttaches(p0: object, p1: object): void;
    isNeaonLightEnabled(index: number): boolean;
    setNeonLightsColour(r: number, g: number, b: number): void;
    getDirtLevel(): number;
    getOwner(entity: MpEntity | object): boolean;
    raiseConvertibleRoof(instantlyRaise: boolean): void;
    detachFromTrailer(): void;
    setNumberPlateTextIndex(plateIndex: number): void;
    getModModifierValue(modType: number, modIndex: number): void;
    getIsSecondaryColourCustom(): boolean;
    setBreakLights(toggle: boolean): void;
    removeMod(modType: number): void;
    setHasStrongAxles(toggle: boolean): void;
    setEnginePowerMultiplier(value: number): void;
    setLodMultiplier(multiplier: number): void;
    setDoorShut(doorIndex: number, closeInstantly: boolean): void;
    setDeformationFixed(): void;
    setNumberPlateText(plateText: string): void;
    retractCargobobHook(): void;
    setEngineCanDegrade(toggle: boolean): void;
    cargobobMagnetGrab(toggle: boolean): void;
    getLandingGearState(): number;
    startHorn(duration: number, model: string | number, forever: boolean): void;
    getPlateType(): number;
    setBikeLeanAngle(x: number, y: number): void;
    setSilent(toggle: boolean): void;
    smashWindow(index: number): void;
    isBig(): boolean;
    getMaxTraction(): number;
    setHeliBladesFullSpeed(): void;
    getColours(colorPrimary: number, colorSecondary: number): {
        readonly colorPrimary: number;
        readonly colorSecondary: number;
    };
    setDamage(xOffset: number, yOffset: number, zOffset: number, damage: number, radius: number, p5: boolean): void;
    setDoorsLockedForAllPlayers(toggle: boolean): void;
    setWheelsCanBreakOffWhenBlowUp(toggle: boolean): void;
    setCeilingHeight(p0: number): void;
    setPlaybackToUseAi(flag: number): void;
    setDoorLatched(doorIndex: number, p1: boolean, p2: boolean, p3: boolean): void;
    requestHighDetailModel(): void;
    removeWindow(windowIndex: number): void;
    getMaxNumberOfPassengers(): number;
    getIsRightHeadlightDamaged(): boolean;
    getPaintFade(): number;
    isVisible(): boolean;
    setTrainSpeed(speed: number): void;
    setForwardSpeed(speed: number): void;
    getHeliEngineHealth(): number;
    getMaxBreaking(): number;
    detachFromAnyCargobob(): boolean;
    getIsEngineRunning(): boolean;
    getHeliTailRotorHealth(): number;
    isOnAllWheels(): boolean;
    setLightMultiplier(multiplier: number): void;
    getModVariation(modType: number): boolean;
    getWheelType(): number;
    getModColor1TextLabel(p0: boolean): string;
    isStuckOnRoof(): boolean;
    getLiveryName(liveryIndex: number): string;
    setEngineTorqueMultiplier(value: number): void;
    setTyreSmokeColor(r: number, g: number, b: number): void;
    setExclusiveDriver(ped: MpPed | object, p1: number): void;
    isSirenSoundOn(): boolean;
    setIndicatorLights(turnSignal: number, toggle: boolean): void;
    getTyreSmokeColor(r: number, g: number, b: number): {
        readonly r: number;
        readonly g: number;
        readonly b: number;
    };
    getCustomPrimaryColour(r: number, g: number, b: number): {
        readonly r: number;
        readonly g: number;
        readonly b: number;
    };
    setDoorsLocked(doorLockStatus: number): void;
    addUpsidedownCheck(): void;
    setBodyHealth(value: number): void;
    setDoorsLockedForTeam(team: number, toggle: boolean): void;
    setPlaneMinHeightAboveGround(height: number): void;
    isDoorDamaged(doorId: number | number): boolean;
    getBodyHealth2(): number;
    setJetEngineOn(toggle: boolean): void;
    startAlarm(): void;
    getLightsState(lightsOn: boolean, highbeamsOn: boolean): {
        readonly lightsOn: boolean;
        readonly highbeamsOn: boolean;
    };
    isTyreBurst(wheelId: number, completely: boolean): boolean;
    explode(isAudible: boolean, isInvisble: boolean): void;
    getPedInSeat(index: number): MpPed | object;
    setInteriorLight(toggle: boolean): void;
    isHeliPartBroken(p0: boolean, p1: boolean, p2: boolean): boolean;
    isDamaged(): boolean;
    setPlayersLast(): void;
    setPedTargettableDestory(vehicleComponent: number, destroyType: number): void;
    setNameDebug(name: string): void;
    isSearchlightOn(): boolean;
    detachFromTowTruck(vehicle: MpVehicle | object): void;
    getEngineHealth(): number;
    removeUpsidedownCheck(): void;
    jitter(p0: boolean, yaw: number, pitch: number, roll: number): void;
    getCargobobHookPosition(): MpVector3;
    setAlarm(state: boolean): void;
    setLandingGear(state: number): void;
    detachFromAnyTowTruck(): boolean;
    isExtraTurnedOn(extraId: number): boolean;
    isAttachedToCargobob(vehicleAttached: MpVehicle | object): boolean;
    setDoorBroken(doorIndex: number, createDoorObject: boolean): void;
    resetStuckTimer(reset: boolean): void;
    disableImpactExplosionActivation(toggle: boolean): void;
    lowerConvertibleRoof(instantlyLower: boolean): void;
    setAllsSpawns(p0: boolean, p1: boolean, p2: boolean): void;
    ejectJb700Roof(x: number, y: number, z: number): void;
    getNumMods(modType: number): number;
    getCauseOfDestruction(): string | number;
    getHeliMainRotorHealth(): number;
    isAttachedToTrailer(): boolean;
    getModColor1(paintType: number | number, color: number, p2: number): {
        readonly paintType: number | number;
        readonly color: number;
        readonly p2: number;
    };
    setTyresCanBurst(toggle: boolean): void;
    setTyreBurst(tyreIndex: number, onRim: boolean, p2: number): void;
    getAttachedToTowTruck(): MpEntity | object;
    getIsPrimaryColourCustom(): boolean;
    getNumberPlateTextIndex(): number;
    setOutOfControl(killDriver: boolean, explodeOnImpact: boolean): void;
    getBodyHealth(): number;
    setDoorControl(doorIndex: number, speed: number, angle: number): void;
    setConvertibleRoof(p0: boolean): void;
    getColor(r: number, g: number, b: number): {
        readonly r: number;
        readonly g: number;
        readonly b: number;
    };
    setSiren(toggle: boolean): void;
    getDoorsLockedForPlayer(player: MpPlayer): boolean;
    setIsWanted(state: boolean): void;
    getConvertibleRoofState(): number;
    setBurnout(toggle: boolean): void;
    setNeedsToBeHotwired(toggle: boolean): void;
    getModKitType(): number;
    setHeliBladeSpeed(speed: number): void;
    getDoorAngleRatio(door: number): number;
    setTowTruckCraneHeight(height: number): void;
    movable(): boolean;
}

declare interface MpVehiclePool extends MpPool<MpVehicle> {

}