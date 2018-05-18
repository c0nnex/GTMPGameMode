/// <reference path="../index.d.ts" />

declare interface MpCheckpoint extends MpEntity {
    radius: number;
    destination: MpVector3;
    visible: boolean;

    showFor(player: MpPlayer): void;
    showFor(players: MpPlayer[]): void;
    hideFor(player: MpPlayer): void;
    hideFor(players: MpPlayer[]): void;

    getColor(): number[];
    setColor(red: number, green: number, blue: number, alpha: number): void;
}

declare interface MpCheckpointPool extends MpPool<MpCheckpoint> {
    'new'(type: number, position: MpVector3, radius: number, options?: MpCheckpointOptions): MpCheckpoint;
}

declare interface MpCheckpointOptions {
    color: [number, number, number, number];
    dimension: number;
    direction: MpVector3;
    visible: boolean;
}