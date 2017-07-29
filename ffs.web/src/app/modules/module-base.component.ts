import { DesignStepComponent, MaterialStepComponent, FlawStepComponent, LoadsStepComponent, ResultStepComponent, EquipmentStepComponent } from './steps/steps.component';
import { QueryList } from '@angular/core';
import { FormControl, Validators, FormGroup } from '@angular/forms';
import { FFSInputBase } from "../common/inputs/ffs-input-base";
import { WizardStepBase } from "../common/wizard/wizard-step-base";

export abstract class ModuleBase {

    onStepChange(step: WizardStepBase) {
        if (step instanceof EquipmentStepComponent) {
            step.backVisible = false;
        }
        else if (step instanceof DesignStepComponent)
            this.initDesignInput();
        else if (step instanceof MaterialStepComponent)
            this.initMaterialInput();
        else if (step instanceof FlawStepComponent)
            this.initFlawInput();
        else if (step instanceof LoadsStepComponent) {
            step.nextVisible = false;
            step.calculateVisible = true;
            this.initLoadInput();
        } else if (step instanceof ResultStepComponent) {
            step.nextVisible = false;
            step.backVisible = false;
        }
    }

    abstract initDesignInput();

    abstract initMaterialInput();

    abstract initFlawInput();

    abstract initLoadInput();
}