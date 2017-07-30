import { Subject } from 'rxjs/Rx';
import { IUnit } from './common/unit';
import { Injectable, EventEmitter } from '@angular/core';
import { ModuleBase } from "./modules/module-base.component";

@Injectable()
export class EventService {

  constructor() { }

  calculationModuleSubject: Subject<ModuleBase> = new Subject();
  requestCalculateSubject: EventEmitter<any> = new EventEmitter();
  calculatingSubject: EventEmitter<any> = new EventEmitter();
  calculatedSubject: EventEmitter<any> = new EventEmitter();
  equipmentTypeSubject: EventEmitter<any> = new EventEmitter();

  unit: Subject<IUnit> = new Subject();
}
