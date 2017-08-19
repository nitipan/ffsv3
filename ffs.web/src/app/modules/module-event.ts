
import { IUnit } from './../common/unit';
import { EventEmitter } from '@angular/core';
import { Subject } from 'rxjs/Rx';

export class ModuleEvent {

    requestCalculateSubject: EventEmitter<any> = new EventEmitter();
    calculatingSubject: EventEmitter<any> = new EventEmitter();
    calculatedSubject: EventEmitter<any> = new EventEmitter();
    equipmentTypeSubject: EventEmitter<any> = new EventEmitter();

    equipmentInputSubject: EventEmitter<any> = new EventEmitter();

    stepChanged: EventEmitter<any> = new EventEmitter();

    unit: Subject<IUnit> = new Subject();

}
