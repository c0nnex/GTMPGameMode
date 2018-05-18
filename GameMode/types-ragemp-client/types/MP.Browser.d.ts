/// <reference path="../index.d.ts" />

declare interface MpBrowser {
    url: string;
    active: boolean;

    reload(ignoreCache: boolean): void;
    execute(executedCode: string): void;
    destroy(): void;
    markAsChat(): void;
}

declare interface MpBrowserPool extends MpPool<MpBrowser> {
    'new'(url: string): MpBrowser;
}
