import { Component, forwardRef } from '@angular/core';
import { WizardStepBase } from "../../common/wizard/wizard-step-base";


@Component({
    selector: 'equipment',
    templateUrl: './steps.component.html',
    styleUrls: ['./steps.component.scss'],
    providers: [{ provide: WizardStepBase, useExisting: forwardRef(() => EquipmentStepComponent) }]
})
export class EquipmentStepComponent extends WizardStepBase {
    title = "Equipment";
}

@Component({
    selector: 'design',
    templateUrl: './steps.component.html',
    styleUrls: ['./steps.component.scss'],
    providers: [{ provide: WizardStepBase, useExisting: forwardRef(() => DesignStepComponent) }]
})
export class DesignStepComponent extends WizardStepBase {
    title = "Design";
}

@Component({
    selector: 'material',
    templateUrl: './steps.component.html',
    styleUrls: ['./steps.component.scss'],
    providers: [{ provide: WizardStepBase, useExisting: forwardRef(() => MaterialStepComponent) }]
})
export class MaterialStepComponent extends WizardStepBase {
    title = "Material";
}

@Component({
    selector: 'flaw',
    templateUrl: './steps.component.html',
    styleUrls: ['./steps.component.scss'],
    providers: [{ provide: WizardStepBase, useExisting: forwardRef(() => FlawStepComponent) }]
})
export class FlawStepComponent extends WizardStepBase {
    title = "Flaw";
}

@Component({
    selector: 'loads',
    templateUrl: './steps.component.html',
    styleUrls: ['./steps.component.scss'],
    providers: [{ provide: WizardStepBase, useExisting: forwardRef(() => LoadsStepComponent) }]
})
export class LoadsStepComponent extends WizardStepBase {
    title = "Loads";
}

@Component({
    selector: 'result',
    templateUrl: './steps.component.html',
    styleUrls: ['./steps.component.scss'],
    providers: [{ provide: WizardStepBase, useExisting: forwardRef(() => ResultStepComponent) }]
})
export class ResultStepComponent extends WizardStepBase {
    title = "Result";
}

