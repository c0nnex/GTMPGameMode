/// <reference path="../index.d.ts" />

declare interface MpGameScript {
    getThreadName(threadId: number): string;
    getEventdata(p0: number, eventIndex: number, eventData: number, p3: number): number;
    getNumberOfInstancesOfStreamedScript(scriptHash: string | number): number;
    isThreadActive(threadId: number): boolean;
    setNoLoadingScreen(toggle: boolean): void;
    triggerScriptEvent(p0: number, argsStruct: MpVector3, argsStructSize: number, bitset: number): MpVector3;
    hasStreamedScriptLoaded(scriptHash: string | number): boolean;
    getNumberOfEvents(p0: number): number;
    requestScript(scriptName: string): void;
    getEventExists(p0: number, eventIndex: number): boolean;
    setStreamedScriptAsNoLongerNeeded(scriptHash: string | number): void;
    terminateThread(threadId: number): void;
    setScriptAsNoLongerNeeded(scriptName: string): void;
    requestStreamedScript(scriptHash: string | number): void;
    getEventAtIndex(p0: number, eventIndex: number): number;
    hasScriptLoaded(scriptName: string): boolean;
    isStreamedScriptRunning(scriptHash: string | number): boolean;
    doesScriptExist(scriptName: string): boolean;
}
