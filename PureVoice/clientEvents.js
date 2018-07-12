/// <reference path="../libs/typescripts/index.d.ts" />
// Allow repeated events when holding down a key?
var noRepeatCheck = false;
var lastKey = null;
API.onServerEventTrigger.connect(function (eventName, args) {
    var player = API.getLocalPlayer();
    if (eventName === "PUREVOICE") {
        API.voiceEnable(args[0], args[1], args[2], args[3], args[4], args[5]);
        return;
    }
    if (eventName === "LIPSYNC") {
        API.playPlayerFacialAnimation(args[0], args[1], args[2]);
        return;
    }
});
//# sourceMappingURL=clientEvents.js.map