import { IUnit } from './unit';
export interface IUnit {
    distance: string;
    temperature: string;
    pressure: string;
    stress: string;
}

export class SIUnit implements IUnit {
    get distance(): string {
        return "mm";
    }
    get temperature(): string {
        return "°C";
    }
    get pressure(): string {
        return "MPa";
    }
    get stress(): string {
        return "MPa";
    }
}

export class MatricUnit implements IUnit {
    get distance(): string {
        return "in";
    }
    get temperature(): string {
        return "°F";
    }
    get pressure(): string {
        return "psi";
    }
    get stress(): string {
        return "psi";
    }
}