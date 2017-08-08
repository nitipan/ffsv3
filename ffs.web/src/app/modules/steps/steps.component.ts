import { EventService } from './../../event.service';
import { Component, forwardRef } from '@angular/core';
import { WizardStepBase } from "../../common/wizard/wizard-step-base";
import { fadeInOut } from "../../common/router.animations";


@Component({
    selector: 'equipment',
    templateUrl: './steps.component.html',
    styleUrls: ['./steps.component.scss'],
    providers: [{ provide: WizardStepBase, useExisting: forwardRef(() => EquipmentStepComponent) }],
    animations: [fadeInOut()]
})
export class EquipmentStepComponent extends WizardStepBase {
    title = "Equipment";
}

@Component({
    selector: 'design',
    templateUrl: './steps.component.html',
    styleUrls: ['./steps.component.scss'],
    providers: [{ provide: WizardStepBase, useExisting: forwardRef(() => DesignStepComponent) }],
    animations: [fadeInOut()]
})
export class DesignStepComponent extends WizardStepBase {
    title = "Design";
}

@Component({
    selector: 'material',
    templateUrl: './steps.component.html',
    styleUrls: ['./steps.component.scss'],
    providers: [{ provide: WizardStepBase, useExisting: forwardRef(() => MaterialStepComponent) }],
    animations: [fadeInOut()]
})
export class MaterialStepComponent extends WizardStepBase {
    title = "Material";
}

@Component({
    selector: 'flaw',
    templateUrl: './steps.component.html',
    styleUrls: ['./steps.component.scss'],
    providers: [{ provide: WizardStepBase, useExisting: forwardRef(() => FlawStepComponent) }],
    animations: [fadeInOut()]
})
export class FlawStepComponent extends WizardStepBase {
    title = "Flaw";
}

@Component({
    selector: 'loads',
    templateUrl: './steps.component.html',
    styleUrls: ['./steps.component.scss'],
    providers: [{ provide: WizardStepBase, useExisting: forwardRef(() => LoadsStepComponent) }],
    animations: [fadeInOut()]
})
export class LoadsStepComponent extends WizardStepBase {
    title = "Loads";
}

@Component({
    selector: 'result',
    templateUrl: './steps.component.html',
    styleUrls: ['./steps.component.scss'],
    providers: [{ provide: WizardStepBase, useExisting: forwardRef(() => ResultStepComponent) }],
    animations: [fadeInOut()]
})
export class ResultStepComponent extends WizardStepBase {
    title = "Result";
}

