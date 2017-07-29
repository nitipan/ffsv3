import { Subject } from 'rxjs/Subject';
import { Injectable } from '@angular/core';
import { ModuleBase } from "./modules/module-base.component";

@Injectable()
export class EventService {

  constructor() { }

  calculationModuleSubject: Subject<ModuleBase> = new Subject();
}
