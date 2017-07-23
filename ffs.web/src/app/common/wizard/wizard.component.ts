
import { Component, OnInit, ContentChildren, QueryList, Input, AfterContentInit } from '@angular/core';
import { WizardStepBase } from "./wizard-step-base";

@Component({
  selector: 'app-wizard',
  templateUrl: './wizard.component.html',
  styleUrls: ['./wizard.component.scss']
})
export class WizardComponent implements OnInit, AfterContentInit {

  @ContentChildren(WizardStepBase) steps: QueryList<WizardStepBase>;

  step: WizardStepBase;

  canBack = false;
  canNext = true;

  constructor() { }

  ngOnInit() {
  }

  ngAfterContentInit(): void {

    let activetab = this.steps.filter((t) => t.active);
    if (activetab.length == 0)
      this.selectStep(this.steps.first);
  }

  selectStep(step: WizardStepBase) {

    this.steps.forEach((t) => t.active = false);

    this.step = step;
    this.step.active = true;

    var currentIndex = this.steps.toArray().indexOf(this.step);
    this.canBack = currentIndex > 0;
    this.canNext = currentIndex < this.steps.length - 1;
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
}



