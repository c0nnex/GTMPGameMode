/// <reference path="../index.d.ts" />

declare interface MpGameDecisionevent {
    isShockingEventInSphere(type: number, x: number, y: number, z: number, radius: number): boolean;
    removeShockingEvent(event: MpGameScript): boolean;
    suppressShockingEvent(type: number | number): void;
    clearDecisionMakerEventResponse(name: string | number, type: number): void;
    addShockingEventForEntity(type: number, entity: MpEntity | object, duration: number): MpGameScript;
    unblockDecisionMakerEvent(name: string | number, type: number): void;
    addShockingEventAtPosition(type: number, x: number, y: number, z: number, duration: number): MpGameScript;
    blockDecisionMakerEvent(name: string | number, type: number): void;
    removeAllShockingEvents(p0: boolean): void;
}
