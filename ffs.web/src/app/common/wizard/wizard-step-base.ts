import { Input } from '@angular/core';
export abstract class WizardStepBase {

    @Input()
    title: string;

    @Input()
    active = false;

    @Input()
    nextVisible = true;

    @Input()
    backVisible = true;
}