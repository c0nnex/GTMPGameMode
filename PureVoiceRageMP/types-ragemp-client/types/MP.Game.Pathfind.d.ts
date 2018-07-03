/// <reference path="../index.d.ts" />

declare interface MpGamePathfind {
    setRoadsBackToOriginalInAngledArea(x1: number, y1: number, z1: number, x2: number, y2: number, z2: number, p6: number): void;
    getVehicleNodeProperties(x: number, y: number, z: number, density: number, flags: number): {
        readonly density: number;
        readonly flags: number;
    };
    updateNavmeshBlockingObject(p0: object, p1: number, p2: number, p3: number, p4: number, p5: number, p6: number, p7: number, p8: object): void;
    isPointOnRoad(x: number, y: number, z: number, vehicle: MpVehicle | object): boolean;
    getClosestRoad(x: number, y: number, z: number, p3: object, p4: object, p5: object, p6: object, p7: object, p8: object, p9: object, p10: object): object;
    setRoadsInAngledArea(x1: number, y1: number, z1: number, x2: number, y2: number, z2: number, angle: number, unknown1: boolean, unknown2: boolean, unknown3: boolean): void;
    isVehicleNodeIdValid(vehicleNodeId: number): boolean;
    setRoadsBackToOriginal(p0: object, p1: object, p2: object, p3: object, p4: object, p5: object): void;
    getNthClosestVehicleNodeId(x: number, y: number, z: number, nth: number, nodetype: number, p5: number, p6: number): number;
    getClosestVehicleNode(x: number, y: number, z: number, outPosition: MpVector3, nodeType: number, p5: number, p6: number): MpVector3;
    getClosestMajorVehicleNode(x: number, y: number, z: number, outPosition: MpVector3, unknown1: number, unknown2: number): MpVector3;
    getStreetNameAtCoord(x: number, y: number, z: number, streetName: number, crossingRoad: number): {
        readonly streetName: number;
        readonly crossingRoad: number;
    };
    setPedPathsInArea(x1: number, y1: number, z1: number, x2: number, y2: number, z2: number, unknown: boolean): void;
    addNavmeshRequiredRegion(p0: number, p1: number, p2: number): void;
    getNthClosestVehicleNodeFavourDirection(x: number, y: number, z: number, desiredX: number, desiredY: number, desiredZ: number, nthClosest: number, outPosition: MpVector3, outHeading: number, nodetype: number, p10: object, p11: object): {
        readonly outPosition: MpVector3;
        readonly outHeading: number;
    };
    removeNavmeshBlockingObject(p0: object): void;
    disableNavmeshInArea(p0: object, p1: object, p2: object, p3: object, p4: object, p5: object, p6: object): void;
    getIsSlowRoadFlag(nodeID: number): boolean;
    getNthClosestVehicleNodeIdWithHeading(x: number, y: number, z: number, nthClosest: number, outPosition: MpVector3, outHeading: number, p6: object, p7: number, p8: number): MpVector3;
    getVehicleNodePosition(nodeId: number, outPosition: MpVector3): MpVector3;
    setIgnoreNoGpsFlag(ignore: boolean): void;
    getNthClosestVehicleNodeWithHeading(x: number, y: number, z: number, nthClosest: number, outPosition: MpVector3, heading: number, unknown1: object, unknown2: number, unknown3: number, unknown4: number): {
        readonly outPosition: MpVector3;
        readonly heading: number;
        readonly unknown1: object;
    };
    loadAllPathNodes(keepInMemory: boolean): boolean;
    getRandomVehicleNode(x: number, y: number, z: number, radius: number, p4: boolean, p5: boolean, p6: boolean, outPosition: MpVector3, heading: number): {
        readonly outPosition: MpVector3;
        readonly outHeading: number;
    };
    getSupportsGpsRouteFlag(nodeID: number): boolean;
    calculateTravelDistanceBetweenPoints(x1: number, y1: number, z1: number, x2: number, y2: number, z2: number): number;
    getSafeCoordForPed(x: number, y: number, z: number, onGround: boolean, outPosition: MpVector3, flags: number): MpVector3;
    setRoadsInArea(x1: number, y1: number, z1: number, x2: number, y2: number, z2: number, unknown1: boolean, unknown2: boolean): void;
    setGpsDisabledZone(p0: object, p1: object, p2: object, p3: object, p4: object, p5: object): void;
    setPedPathsBackToOriginal(p0: object, p1: object, p2: object, p3: object, p4: object, p5: object): void;
    getNthClosestVehicleNode(x: number, y: number, z: number, nthClosest: number, outPosition: MpVector3, unknown1: object, unknown2: object, unknown3: object): MpVector3;
    isNavmeshLoadedInArea(x1: number, y1: number, z1: number, x2: number, y2: number, z2: number): boolean;
    generateDirectionsToCoord(x: number, y: number, z: number, p3: object, p4: number, vehicle: MpVehicle, p6: number): {
        readonly p4: number;
        readonly vehicle: MpVehicle;
        readonly p6: number;
    };
    addNavmeshBlockingObject(p0: number, p1: number, p2: number, p3: number, p4: number, p5: number, p6: number, p7: boolean, p8: object): object;
    getClosestVehicleNodeWithHeading(x: number, y: number, z: number, outPosition: MpVector3, outHeading: number, nodeType: number, p6: number, p7: number): {
        readonly outPosition: MpVector3;
        readonly outHeading: number;
    };
}