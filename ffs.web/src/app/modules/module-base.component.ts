import { DesignStepComponent } from './steps/steps.component';
import { QueryList } from '@angular/core';
import { FormControl, Validators, FormGroup } from '@angular/forms';
import { FFSInputBase } from "../common/inputs/ffs-input-base";
import { WizardStepBase } from "../common/wizard/wizard-step-base";

export abstract class ModuleBase {

    onStepChange(step: WizardStepBase) {
        if (step instanceof DesignStepComponent)
            this.initDesignInput();
    }

    abstract initDesignInput();
}