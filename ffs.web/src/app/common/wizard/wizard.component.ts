import { EventService } from './../../event.service';

import { Component, OnInit, ContentChildren, QueryList, Input, AfterContentInit, Output, EventEmitter, AfterViewInit } from '@angular/core';
import { WizardStepBase } from "./wizard-step-base";

@Component({
  selector: 'app-wizard',
  templateUrl: './wizard.component.html',
  styleUrls: ['./wizard.component.scss']
})
export class WizardComponent implements OnInit, AfterContentInit {



  @ContentChildren(WizardStepBase) steps: QueryList<WizardStepBase>;

  @Output() stepChanged: EventEmitter<WizardStepBase> = new EventEmitter();

  step: WizardStepBase = { title: "", active: false, nextVisible: true, backVisible: true, calculateVisible: false, requestActive: new EventEmitter() };

  canBack = false;
  canNext = true;

  constructor(private eventService: EventService) { }

  ngOnInit() {
  }


  ngAfterContentInit(): void {
    let activetab = this.steps.filter((t) => t.active);
    if (activetab.length == 0)
      this.selectStep(this.steps.first);
    else
      this.selectStep(activetab[0]);

    this.steps.forEach(i => {
      i.requestActive.subscribe(s => {
        this.selectStep(s);
      });
    });
  }

  selectStep(step: WizardStepBase) {

    this.steps.forEach((t) => t.active = false);

    this.step = step;
    this.step.active = true;

    var currentIndex = this.steps.toArray().indexOf(this.step);
    this.canBack = currentIndex > 0;
    this.canNext = currentIndex < this.steps.length - 1;

    this.stepChanged.emit(this.step);
  }

  backStep() {
    var currentIndex = this.steps.toArray().indexOf(this.step);
    if (currentIndex > 0) {
      var backStep = this.steps.toArray()[currentIndex - 1];
      this.selectStep(backStep);
    }
  }

  nextStep() {
    var currentIndex = this.steps.toArray().indexOf(this.step);
    if (currentIndex < this.steps.length - 1) {
      var nextStep = this.steps.toArray()[currentIndex + 1];
      this.selectStep(nextStep);
    }
  }

  calculate() {
    this.eventService.requestCalculateSubject.emit();
  }
}



