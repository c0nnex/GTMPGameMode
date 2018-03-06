﻿declare namespace GTA.Math {

	class Matrix {
		readonly Zero: GTA.Math.Matrix;
		readonly Identity: GTA.Math.Matrix;
		Item: number;
		readonly IsIdentity: boolean;
		readonly HasInverse: boolean;
		constructor(values: any[]);
		Determinant(): number;
		Det3x3(M11: number, M12: number, M13: number, M21: number, M22: number, M23: number, M31: number, M32: number, M33: number): number;
		Invert(): void;
		TransformPoint(vector: GTA.Math.Vector3): GTA.Math.Vector3;
		InverseTransformPoint(vector: GTA.Math.Vector3): GTA.Math.Vector3;
		Add(left: GTA.Math.Matrix, right: GTA.Math.Matrix): GTA.Math.Matrix;
		Subtract(left: GTA.Math.Matrix, right: GTA.Math.Matrix): GTA.Math.Matrix;
		Multiply(left: GTA.Math.Matrix, right: GTA.Math.Matrix): GTA.Math.Matrix;
		Multiply(left: GTA.Math.Matrix, right: number): GTA.Math.Matrix;
		Divide(left: GTA.Math.Matrix, right: GTA.Math.Matrix): GTA.Math.Matrix;
		Divide(left: GTA.Math.Matrix, right: number): GTA.Math.Matrix;
		Negate(matrix: GTA.Math.Matrix): GTA.Math.Matrix;
		Invert(matrix: GTA.Math.Matrix): GTA.Math.Matrix;
		Lerp(start: GTA.Math.Matrix, end: GTA.Math.Matrix, amount: number): GTA.Math.Matrix;
		RotationX(angle: number): GTA.Math.Matrix;
		RotationY(angle: number): GTA.Math.Matrix;
		RotationZ(angle: number): GTA.Math.Matrix;
		RotationAxis(axis: GTA.Math.Vector3, angle: number): GTA.Math.Matrix;
		RotationQuaternion(rotation: GTA.Math.Quaternion): GTA.Math.Matrix;
		RotationYawPitchRoll(yaw: number, pitch: number, roll: number): GTA.Math.Matrix;
		Scaling(x: number, y: number, z: number): GTA.Math.Matrix;
		Scaling(scale: GTA.Math.Vector3): GTA.Math.Matrix;
		Translation(x: number, y: number, z: number): GTA.Math.Matrix;
		Translation(amount: GTA.Math.Vector3): GTA.Math.Matrix;
		Transpose(matrix: GTA.Math.Matrix): GTA.Math.Matrix;
		ToArray(): any[];
		ToString(): string;
		ToString(format: string): string;
		GetHashCode(): number;
		Equals(obj: any): boolean;
		Equals(other: GTA.Math.Matrix): boolean;
	}

	class Quaternion {
		readonly Zero: GTA.Math.Quaternion;
		readonly One: GTA.Math.Quaternion;
		readonly Identity: GTA.Math.Quaternion;
		readonly Axis: GTA.Math.Vector3;
		readonly Angle: number;
		constructor(x: number, y: number, z: number, w: number);
		constructor(value: GTA.Math.Vector3, w: number);
		Length(): number;
		Add(left: GTA.Math.Quaternion, right: GTA.Math.Quaternion): GTA.Math.Quaternion;
		Divide(left: GTA.Math.Quaternion, right: GTA.Math.Quaternion): GTA.Math.Quaternion;
		Dot(left: GTA.Math.Quaternion, right: GTA.Math.Quaternion): number;
		Invert(quaternion: GTA.Math.Quaternion): GTA.Math.Quaternion;
		Lerp(start: GTA.Math.Quaternion, end: GTA.Math.Quaternion, amount: number): GTA.Math.Quaternion;
		Slerp(start: GTA.Math.Quaternion, end: GTA.Math.Quaternion, amount: number): GTA.Math.Quaternion;
		SlerpUnclamped(a: GTA.Math.Quaternion, b: GTA.Math.Quaternion, t: number): GTA.Math.Quaternion;
		FromToRotation(fromDirection: GTA.Math.Vector3, toDirection: GTA.Math.Vector3): GTA.Math.Quaternion;
		RotateTowards(from: GTA.Math.Quaternion, to: GTA.Math.Quaternion, maxDegreesDelta: number): GTA.Math.Quaternion;
		Multiply(left: GTA.Math.Quaternion, right: GTA.Math.Quaternion): GTA.Math.Quaternion;
		Multiply(quaternion: GTA.Math.Quaternion, scale: number): GTA.Math.Quaternion;
		Negate(quaternion: GTA.Math.Quaternion): GTA.Math.Quaternion;
		Normalize(quaternion: GTA.Math.Quaternion): GTA.Math.Quaternion;
		AngleBetween(a: GTA.Math.Quaternion, b: GTA.Math.Quaternion): number;
		Euler(x: number, y: number, z: number): GTA.Math.Quaternion;
		Euler(euler: GTA.Math.Vector3): GTA.Math.Quaternion;
		RotationAxis(axis: GTA.Math.Vector3, angle: number): GTA.Math.Quaternion;
		RotationMatrix(matrix: GTA.Math.Matrix): GTA.Math.Quaternion;
		RotationYawPitchRoll(yaw: number, pitch: number, roll: number): GTA.Math.Quaternion;
		Subtract(left: GTA.Math.Quaternion, right: GTA.Math.Quaternion): GTA.Math.Quaternion;
		ToString(): string;
		ToString(format: string): string;
		GetHashCode(): number;
		Equals(obj: any): boolean;
		Equals(other: GTA.Math.Quaternion): boolean;
	}

	class Vector3 {
		readonly Normalized: GTA.Math.Vector3;
		readonly Zero: GTA.Math.Vector3;
		readonly UnitX: GTA.Math.Vector3;
		readonly UnitY: GTA.Math.Vector3;
		readonly UnitZ: GTA.Math.Vector3;
		readonly WorldUp: GTA.Math.Vector3;
		readonly WorldDown: GTA.Math.Vector3;
		readonly WorldNorth: GTA.Math.Vector3;
		readonly WorldSouth: GTA.Math.Vector3;
		readonly WorldEast: GTA.Math.Vector3;
		readonly WorldWest: GTA.Math.Vector3;
		readonly RelativeRight: GTA.Math.Vector3;
		readonly RelativeLeft: GTA.Math.Vector3;
		readonly RelativeFront: GTA.Math.Vector3;
		readonly RelativeBack: GTA.Math.Vector3;
		readonly RelativeTop: GTA.Math.Vector3;
		readonly RelativeBottom: GTA.Math.Vector3;
		Item: number;
		constructor(x: number, y: number, z: number);
		Length(): number;
		LengthSquared(): number;
		Normalize(): void;
		DistanceTo(position: GTA.Math.Vector3): number;
		DistanceToSquared(position: GTA.Math.Vector3): number;
		DistanceTo2D(position: GTA.Math.Vector3): number;
		DistanceToSquared2D(position: GTA.Math.Vector3): number;
		Distance(position1: GTA.Math.Vector3, position2: GTA.Math.Vector3): number;
		DistanceSquared(position1: GTA.Math.Vector3, position2: GTA.Math.Vector3): number;
		Distance2D(position1: GTA.Math.Vector3, position2: GTA.Math.Vector3): number;
		DistanceSquared2D(position1: GTA.Math.Vector3, position2: GTA.Math.Vector3): number;
		Angle(from: GTA.Math.Vector3, to: GTA.Math.Vector3): number;
		SignedAngle(from: GTA.Math.Vector3, to: GTA.Math.Vector3, planeNormal: GTA.Math.Vector3): number;
		ToHeading(): number;
		Around(distance: number): GTA.Math.Vector3;
		RandomXY(): GTA.Math.Vector3;
		RandomXYZ(): GTA.Math.Vector3;
		Add(left: GTA.Math.Vector3, right: GTA.Math.Vector3): GTA.Math.Vector3;
		Subtract(left: GTA.Math.Vector3, right: GTA.Math.Vector3): GTA.Math.Vector3;
		Multiply(value: GTA.Math.Vector3, scale: number): GTA.Math.Vector3;
		Multiply(left: GTA.Math.Vector3, right: GTA.Math.Vector3): GTA.Math.Vector3;
		Divide(value: GTA.Math.Vector3, scale: number): GTA.Math.Vector3;
		Negate(value: GTA.Math.Vector3): GTA.Math.Vector3;
		Clamp(value: GTA.Math.Vector3, min: GTA.Math.Vector3, max: GTA.Math.Vector3): GTA.Math.Vector3;
		Lerp(start: GTA.Math.Vector3, end: GTA.Math.Vector3, amount: number): GTA.Math.Vector3;
		Normalize(vector: GTA.Math.Vector3): GTA.Math.Vector3;
		Dot(left: GTA.Math.Vector3, right: GTA.Math.Vector3): number;
		Cross(left: GTA.Math.Vector3, right: GTA.Math.Vector3): GTA.Math.Vector3;
		Project(vector: GTA.Math.Vector3, onNormal: GTA.Math.Vector3): GTA.Math.Vector3;
		ProjectOnPlane(vector: GTA.Math.Vector3, planeNormal: GTA.Math.Vector3): GTA.Math.Vector3;
		Reflect(vector: GTA.Math.Vector3, normal: GTA.Math.Vector3): GTA.Math.Vector3;
		Minimize(left: GTA.Math.Vector3, right: GTA.Math.Vector3): GTA.Math.Vector3;
		Maximize(left: GTA.Math.Vector3, right: GTA.Math.Vector3): GTA.Math.Vector3;
		ToString(): string;
		ToString(format: string): string;
		GetHashCode(): number;
		Equals(obj: any): boolean;
		Equals(other: GTA.Math.Vector3): boolean;
	}

}
