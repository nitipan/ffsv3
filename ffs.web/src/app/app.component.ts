import { InputBase } from './model/inputbase';
import { EventService } from './event.service';
import { Component, ChangeDetectorRef, AfterViewInit } from '@angular/core';
import { ModuleBase } from "./modules/module-base.component";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements AfterViewInit {

  commonInput: InputBase = null;
  currentModule: ModuleBase = null;

  constructor(private eventService: EventService) {

  }

  ngAfterViewInit(): void {
    this.eventService.calculationModuleSubject.subscribe(c => {

      this.currentModule = c;

      if (this.currentModule != null) {
        this.currentModule.valueChanges.subscribe(m => {
          this.commonInput = m;
        });

      }

    });
  }
}
