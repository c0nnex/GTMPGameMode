/// <reference path="../index.d.ts" />

declare interface MpGameRope {
    setDisableFragDamage(object: MpObject | object, toggle: boolean): void;
    startRopeWinding(rope: MpObject | object): void;
    getRopeLastVertexCoord(rope: MpObject | object): void;
    pinRopeVertex(rope: MpObject | object, vertex: number, x: number, y: number, z: number): void;
    breakEntityGlass(p0: object, p1: number, p2: number, p3: number, p4: number, p5: number, p6: number, p7: number, p8: number, p9: object, p10: boolean): void;
    getRopeVertexCount(rope: MpObject | object): number;
    attachEntitiesToRope(rope: MpObject | object, ent1: MpEntity | object, ent2: MpEntity | object, ent1_x: number, ent1_y: number, ent1_z: number, ent2_x: number, ent2_y: number, ent2_z: number, length: number, p10: boolean, p11: boolean, p12: object, p13: object): void;
    attachRopeToEntity(rope: MpObject | object, entity: MpEntity | object, x: number, y: number, z: number, p5: boolean): void;
    unpinRopeVertex(rope: MpObject | object, vertex: number): object;
    deleteRope(rope: MpObject | object): MpObject;
    ropeConvertToSimple(rope: MpObject | object): void;
    startRopeUnwindingFront(rope: MpObject | object): void;
    setDisableBreaking(rope: MpObject | object, enabled: boolean): object;
    getRopeLength(rope: MpObject | object): number;
    getCgoffset(rope: MpObject | object): MpVector3;
    deleteChildRope(rope: MpObject | object): object;
    detachRopeFromEntity(rope: MpObject | object, entity: MpEntity | object): void;
    setCgAtBoundcenter(rope: MpObject | object): void;
    ropeResetLength(rope: MpObject | object, length: boolean): object;
    ropeSetUpdatePinverts(rope: MpObject | object): void;
    stopRopeWinding(rope: MpObject | object): void;
    loadRopeData(rope: MpObject | object, rope_preset: string): object;
    ropeForceLength(rope: MpObject | object, length: number): object;
    setCgoffset(rope: MpObject | object, x: number, y: number, z: number): void;
    applyImpulseToCloth(posX: number, posY: number, posZ: number, vecX: number, vecY: number, vecZ: number, impulse: number): void;
    addRope(x: number, y: number, z: number, rotX: number, rotY: number, rotZ: number, length: number, ropeType: number, maxLength: number, minLength: number, p10: number, p11: boolean, p12: boolean, rigid: boolean, p14: number, breakWhenShot: boolean, unkPtr: object): MpObject | object;
    getRopeVertexCoord(rope: MpObject | object, vertex: number): object;
    setDamping(rope: MpObject | object, vertex: number, value: number): void;
    ropeDrawShadowEnabled(rope: MpObject | object, toggle: boolean): MpObject | object;
    doesRopeExist(rope: MpObject | object): MpObject | object;
    stopRopeUnwindingFront(rope: MpObject | object): void;
}
