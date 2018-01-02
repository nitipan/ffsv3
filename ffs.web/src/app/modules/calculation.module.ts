import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BrittleFractureComponent } from './brittle-fracture/brittle-fracture.component';
import { AppCommonModule } from '../common/common.module';
import { ModuleEvent } from './module-event';
import { EquipmentStepComponent, DesignStepComponent, MaterialStepComponent, FlawStepComponent, LoadsStepComponent, ResultStepComponent } from './steps/steps.component';
import { EquipmentInputComponent } from './common/equipment-input/equipment-input.component';
import { DesignInputComponent } from './common/design-input/design-input.component';
import { MaterialInputComponent } from './common/material-input/material-input.component';
import { LoadInputComponent } from './common/load-input/load-input.component';
import { ResultComponent } from './common/result/result.component';
import { EquipmentImageComponent } from './common/equipment-image/equipment-image.component';
import { LocalMetalLossComponent } from './local-metal-loss/local-metal-loss.component';
import { GeneralMetalLossComponent } from './general-metal-loss/general-metal-loss.component';
import { PittingCorrosionComponent } from './pitting-corrosion/pitting-corrosion.component';
import { WeldMisalignmentComponent } from './weld-misalignment/weld-misalignment.component';
import { DentComponent } from './dent/dent.component';
import { MetalLossInputComponent } from './common/metal-loss-input/metal-loss-input.component';
import { CreepRuptureComponent } from './creep-rupture/creep-rupture.component';
import { HydrogenComponent } from './hydrogen/hydrogen.component';
import { CrackComponent } from './crack/crack.component';
import { FireDamageComponent } from './fire-damage/fire-damage.component';
import { LaminationComponent } from './lamination/lamination.component';
import { FatigueComponent } from './fatigue/fatigue.component';
import { CanActivateViaAuthGuard } from '../can-activate-via-auth-guard';

const appRoutes: Routes = [{
    path: 'brittle', component: BrittleFractureComponent, canActivate: [CanActivateViaAuthGuard]
},
{ path: 'localmetalloss', component: LocalMetalLossComponent, canActivate: [CanActivateViaAuthGuard] },
{ path: 'generalmetalloss', component: GeneralMetalLossComponent, canActivate: [CanActivateViaAuthGuard] },
{ path: 'pittingcorrosion', component: PittingCorrosionComponent, canActivate: [CanActivateViaAuthGuard] },
{ path: 'weld', component: WeldMisalignmentComponent, canActivate: [CanActivateViaAuthGuard] },
{ path: 'dent', component: DentComponent, canActivate: [CanActivateViaAuthGuard] },
{ path: 'creeprupture', component: CreepRuptureComponent, canActivate: [CanActivateViaAuthGuard] },
{ path: 'hydrogen', component: HydrogenComponent, canActivate: [CanActivateViaAuthGuard] },
{ path: 'crack', component: CrackComponent, canActivate: [CanActivateViaAuthGuard] },
{ path: 'fire', component: FireDamageComponent, canActivate: [CanActivateViaAuthGuard] },
{ path: 'lamination', component: LaminationComponent, canActivate: [CanActivateViaAuthGuard] },
{ path: 'fatigue', component: FatigueComponent, canActivate: [CanActivateViaAuthGuard] },
];

@NgModule({
    imports: [
        AppCommonModule,
        RouterModule.forChild(appRoutes)],
    exports: [],
    declarations: [
        EquipmentInputComponent,
        DesignInputComponent,
        MaterialInputComponent,
        LoadInputComponent,
        ResultComponent,
        EquipmentImageComponent,

        EquipmentStepComponent,
        DesignStepComponent,
        MaterialStepComponent,
        FlawStepComponent,
        LoadsStepComponent,
        ResultStepComponent,
        BrittleFractureComponent,
        LocalMetalLossComponent,
        GeneralMetalLossComponent,
        PittingCorrosionComponent,
        WeldMisalignmentComponent,
        DentComponent,
        MetalLossInputComponent,
        CreepRuptureComponent,
        HydrogenComponent,
        CrackComponent,
        FireDamageComponent,
        LaminationComponent,
        FatigueComponent,
        MetalLossInputComponent,
        CreepRuptureComponent,
        HydrogenComponent,
        CrackComponent,
        FireDamageComponent,
        LaminationComponent,
        FatigueComponent,
    ],
    providers: [ModuleEvent],
})
export class CalculationModule { }
