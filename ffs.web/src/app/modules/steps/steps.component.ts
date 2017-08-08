import { trigger, state, style, transition, animate } from '@angular/animations';
import { EventService } from './../../event.service';
import { Component, forwardRef } from '@angular/core';
import { WizardStepBase } from "../../common/wizard/wizard-step-base";


@Component({
    selector: 'equipment',
    templateUrl: './steps.component.html',
    styleUrls: ['./steps.component.scss'],
    providers: [{ provide: WizardStepBase, useExisting: forwardRef(() => EquipmentStepComponent) }],
    animations: [
        trigger('fadeInOut', [
            transition(':enter', [   // :enter is alias to 'void => *'
                style({ opacity: 0 }),
                animate(300, style({ opacity: 1 }))
            ]),
            transition(':leave', [   // :leave is alias to '* => void'
                animate(1, style({ opacity: 0 }))
            ])
        ])
    ]
})
export class EquipmentStepComponent extends WizardStepBase {
    title = "Equipment";
}

@Component({
    selector: 'design',
    templateUrl: './steps.component.html',
    styleUrls: ['./steps.component.scss'],
    providers: [{ provide: WizardStepBase, useExisting: forwardRef(() => DesignStepComponent) }],
    animations: [
        trigger('fadeInOut', [
            transition(':enter', [   // :enter is alias to 'void => *'
                style({ opacity: 0 }),
                animate(300, style({ opacity: 1 }))
            ]),
            transition(':leave', [   // :leave is alias to '* => void'
                animate(1, style({ opacity: 0 }))
            ])
        ])
    ]
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

    // constructor(private eventService: EventService) {
    //     super();
    //     this.eventService.calculatedSubject.subscribe(result => {
    //         this.requestActive.emit(this);
    //     });
    // }
}

