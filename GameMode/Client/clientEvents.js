/// <reference path="../GT-MP/typescripts/index.d.ts" />
// Allow repeated events when holding down a key?
var noRepeatCheck = false;
var lastKey = null;
API.onServerEventTrigger.connect(function (eventName, args) {
    var player = API.getLocalPlayer();
    if (eventName === "GTMPVOICE") {
        API.voiceEnable(args[0], args[1], args[2], args[3], args[4], args[5]);
        return;
    }
    if (eventName === "LIPSYNC") {
        API.playPlayerFacialAnimation(args[0], args[1], args[2]);
        return;
    }
});
API.onKeyDown.connect(function (sender, e) {
    if (!noRepeatCheck && (lastKey == e.KeyCode))
        return;
    lastKey = e.KeyCode;
    switch (e.KeyCode) {
        case Keys.PageDown:
            API.triggerServerEvent("RADIO_TOGGLE_SPEAK");
            break;
        case Keys.PageUp:
            API.triggerServerEvent("RADIO_MUTE");
            break;
        case Keys.End:
            API.triggerServerEvent("RADIO_NEXT");
            break;
        case Keys.OemBackslash:
            API.triggerServerEvent("RADIO_PTT", true);
            break;
    }
});
API.onKeyUp.connect(function (sender, e) {
    lastKey = null;
    switch (e.KeyCode) {
        case Keys.OemBackslash:
            API.triggerServerEvent("RADIO_PTT", false);
            break;
    }
});
//# sourceMappingURL=clientEvents.js.map