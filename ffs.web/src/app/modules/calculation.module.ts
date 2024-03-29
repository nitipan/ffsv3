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
import { LocalMetalLossComponent } from './local-metal-loss/local-metal-loss.component';
import { GeneralMetalLossComponent } from './general-metal-loss/general-metal-loss.component';
import { TestComponent } from './test.component';
import { FatigueComponent } from './fatigue/fatigue.component';
const appRoutes: Routes = [

    {
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
    { path: 'test', component: TestComponent, canActivate: [CanActivateViaAuthGuard] },
    { path: 'fatigue', component: FatigueComponent, canActivate: [CanActivateViaAuthGuard] },

];

@NgModule({
    imports: [
        CalculationCommonModule,
        RouterModule.forChild(appRoutes)],
    exports: [],
    declarations: [
        FireDamageComponent,
        BrittleFractureComponent,
        LocalMetalLossComponent,
        GeneralMetalLossComponent,
        PittingCorrosionComponent,
        WeldMisalignmentComponent,
        DentComponent,
        CrackComponent,
        LaminationComponent,
        CreepRuptureComponent,
        HydrogenComponent,
        CrackComponent,
        LaminationComponent,
        TestComponent,
        FatigueComponent
    ],
    providers: [],
})
export class CalculationModule { }
