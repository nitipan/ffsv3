import { DesignStepComponent, MaterialStepComponent, FlawStepComponent, LoadsStepComponent, ResultStepComponent } from './steps/steps.component';
import { QueryList } from '@angular/core';
import { FormControl, Validators, FormGroup } from '@angular/forms';
import { FFSInputBase } from "../common/inputs/ffs-input-base";
import { WizardStepBase } from "../common/wizard/wizard-step-base";

export abstract class ModuleBase {

    onStepChange(step: WizardStepBase) {
        if (step instanceof DesignStepComponent)
            this.initDesignInput();
        else if (step instanceof MaterialStepComponent)
            this.initMaterialInput();
        else if (step instanceof FlawStepComponent)
            this.initFlawInput();
        else if (step instanceof LoadsStepComponent)
            this.initLoadInput();
    }

    abstract initDesignInput();

    abstract initMaterialInput();

    abstract initFlawInput();

    abstract initLoadInput();
}