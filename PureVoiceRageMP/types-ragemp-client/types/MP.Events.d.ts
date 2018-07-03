/// <reference path="../index.d.ts" />

declare interface MpEvents {
    add(events: object): void;
    add(eventName: string, func: (...args: any[]) => void): void;
    call(eventName: string, ...args: any[]): void;
    addCommand(commandName: string, handlerFunction: (fullText: string, ...args: any[]) => void): void;
    callRemote( eventName: string, ...args: any[] ): void;
    remove( eventname: string, func: ( ...args: any[] ) => void ): void;
}
