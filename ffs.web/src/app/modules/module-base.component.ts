import { InputBase } from './../model/inputbase';
import { Subject } from 'rxjs/Subject';
import { Observable } from 'rxjs/Rx';
import { EventService } from './../event.service';
import { DesignStepComponent, MaterialStepComponent, FlawStepComponent, LoadsStepComponent, ResultStepComponent, EquipmentStepComponent } from './steps/steps.component';
import { QueryList, OnDestroy, AfterViewInit, Input } from '@angular/core';
import { FormControl, Validators, FormGroup } from '@angular/forms';
import { FFSInputBase } from "../common/inputs/ffs-input-base";
import { WizardStepBase } from "../common/wizard/wizard-step-base";

export abstract class ModuleBase implements OnDestroy {

    protected valueChangedSubject: Subject<InputBase> = new Subject();

    constructor(protected eventService: EventService) {

        this.valueChanges = this.valueChangedSubject.asObservable();

        this.eventService.calculationModuleSubject.next(this);
    }
    ngOnDestroy(): void {
        this.eventService.calculationModuleSubject.next(null);
    }

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

    abstract name: string;

    abstract initDesignInput();

    abstract initMaterialInput();

    abstract initFlawInput();

    abstract initLoadInput();

    @Input()
    valueChanges: Observable<InputBase>
}