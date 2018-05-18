/// <reference path="../index.d.ts" />

declare interface MpEvents {
    add(events: object): void;
    add(eventName: string, func: (...args: any[]) => void): void;
    call(eventName: string, ...args: any[]): void;
    addCommand(commandName: string, callback: (player: MpPlayer, message: string, ...args: any[]) => void): void;
}