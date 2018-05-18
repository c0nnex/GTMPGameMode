/// <reference path="../index.d.ts" />

declare interface MpWorld {
    weather: string;
    time: MpTime;
    trafficLights: {
        locked: boolean,
        state: number,
    };

    setWeatherTransition(newWeather: string): void;
    removeIpl(name: string): void;
    requestIpl(name: string): void;
}

declare interface MpTime {
    hour: number;
    minute: number;
    second: number;
}
