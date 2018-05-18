/// <reference path="../index.d.ts" />

declare interface MpBlip extends MpEntity {
    color: number;
    shortRange: boolean;
    sprite: number;
    colour: number;
    name: string;
    scale: number;
    alpha: number;
    drawDistance: number;
    rotation: number;

    routeFor(player: MpPlayer, colour: number, scale: number): void;
    routeFor(players: MpPlayer[], colour: number, scale: number): void;
    unrouteFor(player: MpPlayer): void;
    unrouteFor(players: MpPlayer[]): void;
}

declare interface MpBlipPool extends MpPool<MpBlip> {
    'new'(model: number, position: MpVector3, options?: MpBlipOptions): MpBlip;
    newStreamed(model: number, position: MpVector3, streamRange: number): MpBlip;
}

declare interface MpBlipOptions {
    alpha: number;
    color: number;
    dimension: number;
    drawDistance: number;
    name: string;
    rotation: number;
    scale: number;
    shortRange: boolean;
}