import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppCommonModule } from '../common/common.module';
import { CanActivateViaAuthGuard } from '../can-activate-via-auth-guard';
import { CalculationCommonModule } from './calculation-common.module';
import { FireDamageComponent } from './fire-damage/fire-damage.component';
import { HydrogenComponent } from './hydrogen/hydrogen.component';
import { BrittleFractureComponent } from './brittle-fracture/brittle-fracture.component';
import { LaminationComponent } from './lamination/lamination.component';
import { CrackComponent } from './crack/crack.component';
import { CreepRuptureComponent } from './creep-rupture/creep-rupture.component';
import { DentComponent } from './dent/dent.component';
import { WeldMisalignmentComponent } from './weld-misalignment/weld-misalignment.component';
import { PittingCorrosionComponent } from './pitting-corrosion/pitting-corrosion.component';
const appRoutes: Routes = [

    {
        path: 'brittle', component: BrittleFractureComponent, canActivate: [CanActivateViaAuthGuard]
    },
    // { path: 'localmetalloss', component: LocalMetalLossComponent, canActivate: [CanActivateViaAuthGuard] },
    // { path: 'generalmetalloss', component: GeneralMetalLossComponent, canActivate: [CanActivateViaAuthGuard] },
    { path: 'pittingcorrosion', component: PittingCorrosionComponent, canActivate: [CanActivateViaAuthGuard] },
    { path: 'weld', component: WeldMisalignmentComponent, canActivate: [CanActivateViaAuthGuard] },
    { path: 'dent', component: DentComponent, canActivate: [CanActivateViaAuthGuard] },
    { path: 'creeprupture', component: CreepRuptureComponent, canActivate: [CanActivateViaAuthGuard] },
    { path: 'hydrogen', component: HydrogenComponent, canActivate: [CanActivateViaAuthGuard] },
    { path: 'crack', component: CrackComponent, canActivate: [CanActivateViaAuthGuard] },
    { path: 'fire', component: FireDamageComponent, canActivate: [CanActivateViaAuthGuard] },
    { path: 'lamination', component: LaminationComponent, canActivate: [CanActivateViaAuthGuard] },
];

@NgModule({
    imports: [
        CalculationCommonModule,
        RouterModule.forChild(appRoutes)],
    exports: [],
    declarations: [
        FireDamageComponent,
        BrittleFractureComponent,
        // LocalMetalLossComponent,
        // GeneralMetalLossComponent,
        PittingCorrosionComponent,
        WeldMisalignmentComponent,
        DentComponent,
        // MetalLossInputComponent,            
        CrackComponent,
        LaminationComponent,
        CreepRuptureComponent,
        HydrogenComponent,
        CrackComponent,
        LaminationComponent
    ],
    providers: [],
})
export class CalculationModule { }
