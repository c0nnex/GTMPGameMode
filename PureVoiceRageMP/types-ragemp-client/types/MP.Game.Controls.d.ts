/// <reference path="../index.d.ts" />

declare interface MpGameControls {
    getControlActionName(inputGroup: number, control: number, p2: boolean): string;
    getDisabledControlNormal(inputGroup: number, control: number): number;
    isInputJustDisabled(inputGroup: number): boolean;
    isControlEnabled(inputGroup: number, control: number): boolean;
    isDisabledControlJustReleased(inputGroup: number, control: number): boolean;
    enableControlAction(inputGroup: number, control: number, enable: boolean): void;
    stopPadShake(p0: object): void;
    setPadShake(p0: number, duration: number, frequency: number): void;
    isControlJustReleased(inputGroup: number, control: number): boolean;
    isControlJustPressed(inputGroup: number, control: number): boolean;
    disableAllControlActions(inputGroup: number): void;
    isControlReleased(inputGroup: number, control: number): boolean;
    setPlayerpadShakesWhenControllerDisabled(toggle: boolean): void;
    isDisabledControlJustPressed(inputGroup: number, control: number): boolean;
    isInputDisabled(inputGroup: number): boolean;
    enableAllControlActions(inputGroup: number): void;
    getControlValue(inputGroup: number, control: number): number;
    isControlPressed(inputGroup: number, control: number): boolean;
    setControlNormal(inputGroup: number, control: number, amount: number): boolean;
    getControlNormal(inputGroup: number, control: number): number;
    setInputExclusive(inputGroup: number, control: number): void;
    disableControlAction(inputGroup: number, control: number, disable: boolean): void;
}
