/// <reference path="../index.d.ts" />

declare interface MpMarker extends MpEntity {
    scale: number;
    direction: MpVector3;
    visible: boolean;

    showFor(player: MpPlayer): void;
    showFor(players: MpPlayer[]): void;
    hideFor(player: MpPlayer): void;
    hideFor(players: MpPlayer[]): void;
    getColor(): number[];
    setColor(red: number, green: number, blue: number, alpha: number): void;
}

declare interface MpMarkerPool extends MpPool<MpMarker> {
    'new'(type: number, position: MpVector3, scale: number, options?: MpMarkerOptions): MpMarker;
}

declare interface MpMarkerOptions {
    color: [number, number, number, number];
    dimension: number;
    direction: MpVector3;
    rotation: MpVector3;
    visible: boolean;
}
