import { Input, EventEmitter, Output } from '@angular/core';
export abstract class WizardStepBase {

    @Input()
    title: string;

    @Input()
    active = false;

    @Input()
    nextVisible = true;

    @Input()
    backVisible = true;

    @Input()
    calculateVisible = false;

    @Input()
    requestActive: EventEmitter<WizardStepBase> = new EventEmitter();

    @Output()
    onCalculate: EventEmitter<WizardStepBase> = new EventEmitter();


}