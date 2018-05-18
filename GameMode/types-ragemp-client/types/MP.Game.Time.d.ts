/// <reference path="../index.d.ts" />

declare interface MpGameTime {
    pauseClock(toggle: boolean): void;
    setClockTime(hour: number, minute: number, second: number): void;
    getLocalTime(year: number, month: number, day: number, hour: number, minute: number, second: number): {
        readonly year: number;
        readonly month: number;
        readonly day: number;
        readonly hour: number;
        readonly minute: number;
        readonly second: number;
    };
    getLocalTimeGmt(year: number, month: number, day: number, hour: number, minute: number, second: number): {
        readonly year: number;
        readonly month: number;
        readonly day: number;
        readonly hour: number;
        readonly minute: number;
        readonly second: number;
    };
    setClockDate(day: number, month: number, year: number): void;
    advanceClockTimeTo(hour: number, minute: number, second: number): void;
    addToClockTime(hour: number, minute: number, second: number): void;
    getPosixTime(year: number, month: number, day: number, hour: number, minute: number, second: number): {
        readonly year: number;
        readonly month: number;
        readonly day: number;
        readonly hour: number;
        readonly minute: number;
        readonly second: number;
    };
}
