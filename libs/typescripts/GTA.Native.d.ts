declare namespace GTA.Native {

	class InputArgument {
		constructor(value: any);
		ToString(): string;
	}

	class OutputArgument {
		constructor();
		constructor(value: any);
		Finalize(): void;
		Dispose(): void;
		GetResult<T>(): any;
	}

}
