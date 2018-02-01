import { NgModule } from '@angular/core';
import { EquipmentInputComponent } from './common/equipment-input/equipment-input.component';
import { DesignInputComponent } from './common/design-input/design-input.component';
import { MaterialInputComponent } from './common/material-input/material-input.component';
import { LoadInputComponent } from './common/load-input/load-input.component';
import { ResultComponent } from './common/result/result.component';
import { EquipmentImageComponent } from './common/equipment-image/equipment-image.component';
import { EquipmentStepComponent, DesignStepComponent, MaterialStepComponent, LoadsStepComponent, FlawStepComponent, ResultStepComponent } from './steps/steps.component';
import { ModuleEvent } from './module-event';
import { AppCommonModule } from '../common/common.module';
import { MetalLossInputComponent } from './common/metal-loss-input/metal-loss-input.component';

@NgModule({
    imports: [
        AppCommonModule
    ],
    exports: [
        AppCommonModule,
        EquipmentInputComponent,
        DesignInputComponent,
        MaterialInputComponent,
        LoadInputComponent,
        ResultComponent,
        EquipmentImageComponent,
        MetalLossInputComponent,
        EquipmentStepComponent,
        DesignStepComponent,
        MaterialStepComponent,
        FlawStepComponent,
        LoadsStepComponent,
        ResultStepComponent],
    declarations: [EquipmentInputComponent,
        DesignInputComponent,
        MaterialInputComponent,
        LoadInputComponent,
        ResultComponent,
        EquipmentImageComponent,
        MetalLossInputComponent,
        EquipmentStepComponent,
        DesignStepComponent,
        MaterialStepComponent,
        FlawStepComponent,
        LoadsStepComponent,
        ResultStepComponent],
    providers: [ModuleEvent],
})
export class CalculationCommonModule { }
