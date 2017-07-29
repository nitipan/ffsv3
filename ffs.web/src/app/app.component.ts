import { EventService } from './event.service';
import { Component } from '@angular/core';
import { ModuleBase } from "./modules/module-base.component";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  private currentModule: ModuleBase = null;

  constructor(private eventService: EventService) {

    eventService.calculationModuleSubject.subscribe(c => {
      this.currentModule = c;
    });

  }
}
