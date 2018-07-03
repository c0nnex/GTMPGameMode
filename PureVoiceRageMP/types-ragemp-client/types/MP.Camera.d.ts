/// <reference path="../index.d.ts" />

declare interface MpCamera extends MpEntity {
    getDirection(): MpVector3;
    setActive(active: boolean): void;
    isRendering(): boolean;
    isInterpolating(): boolean;
    setUseShallowDofMode(toggle: boolean): void;
    setDebugName(name: string): void;
    getFarDof(): number;
    setDofMaxNearInFocuxDistanceBlendLevel(p0: number): void;
    setDofPlanes(p0: number, p1: number, p2: number, p3: number): void;
    setNearDof(nearDof: number): void;
    setAnimCurrentPhase(phase: number): void;
    setInheritRollVehicle(p1: boolean): void;
    setCoord(posX: number, posY: number, posZ: number): void;
    pointAt(entity: MpEntity, offsetX: number, offsetY: number, offsetZ: number, p4: boolean): void;
    setDofStrength(dofStrength: number): void;
    attachToPedBone(ped: MpPed, boneIndex: number, x: number, y: number, z: number, heading: boolean): void;
    pointAtPedBone(ped: MpPed, boneIndex: number, x: number, y: number, z: number, heading: boolean): void;
    shake(type: string, amplitude: number): void;
    isShaking(): boolean;
    setMotionBlurStrength(strength: number): void;
    getRot(p0: number): MpVector3;
    setDofFnumberOfLens(p1: number): void;
    setRot(rotX: number, rotY: number, rotZ: number, p3: number): void;
    destroy(destroy?: boolean): void;
    setAffectsAiming(toggle: boolean): void;
    playAnim(animName: string, animDictionary: string, x: number, y: number, z: number, xRot: number, yRot: number, zRot: number, p8: boolean, p9: number): void;
    playAnim(animName: string, propName: string, p2: number, p3: boolean, p4: boolean, p5: boolean, delta: number, bitset: object): boolean;
    setActiveWithInterp(camFrom: MpCamera, duration: number, easeLocation: number, easeRotation: number): void;
    getAnimCurrentPhase(): number;
    animatedShake(p0: string, p1: string, p2: string, p3: number): void;
    detach(): void;
    doesExist(): boolean;
    setFarClip(farClip: number): void;
    setFov(fieldOfView: number): void;
    getSplinePhase(): number;
    getFarClip(): number;
    getCoord(): MpVector3;
    stopShaking(p0: boolean): void;
    setParams(x: number, y: number, z: number, xRot: number, yRot: number, zRot: number, fov: number, duration: number, p8: number, p9: number, p10: number): void;
    getFov(): number;
    setDofMaxNearInFocusDistance(p0: number): void;
    getNearClip(): number;
    setDofFocusDistanceBias(p0: number): void;
    setNearClip(nearClip: number): void;
    isPlayingAnim(animName: string, animDictionary: string): boolean;
    setShakeAmplitude(amplitude: number): void;
    isActive(): boolean;
    setFarDof(farDof: number): void;
    stopPointing(): void;
    pointAtCoord(x: number, y: number, z: number): void;
    attachTo(entity: MpEntity | object, xOffset: number, yOffset: number, zOffset: number, isRelative: boolean): void;
    attachTo(entity: MpEntity | object, boneIndex: number, xPos: number, yPos: number, zPos: number, xRot: number, yRot: number, zRot: number, p8: boolean, useSoftPinning: boolean, collision: boolean, isPed: boolean, vertexIndex: number, fixedRot: boolean): void;
}

declare interface MpCameraPool extends MpPool<MpCamera> {
	"new"( name: string ): MpCamera;
}
