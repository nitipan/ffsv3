import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppCommonModule } from '../common/common.module';
import { CanActivateViaAuthGuard } from '../can-activate-via-auth-guard';
import { CalculationCommonModule } from './calculation-common.module';
const appRoutes: Routes = [

    {
        path: 'brittle', loadChildren: './brittle-fracture/brittle-fracture.module#BrittleFractureModule', canActivate: [CanActivateViaAuthGuard]
    },
    // { path: 'localmetalloss', component: LocalMetalLossComponent, canActivate: [CanActivateViaAuthGuard] },
    // { path: 'generalmetalloss', component: GeneralMetalLossComponent, canActivate: [CanActivateViaAuthGuard] },
    // { path: 'pittingcorrosion', component: PittingCorrosionComponent, canActivate: [CanActivateViaAuthGuard] },
    // { path: 'weld', component: WeldMisalignmentComponent, canActivate: [CanActivateViaAuthGuard] },
    // { path: 'dent', component: DentComponent, canActivate: [CanActivateViaAuthGuard] },
    // { path: 'creeprupture', component: CreepRuptureComponent, canActivate: [CanActivateViaAuthGuard] },
    // { path: 'hydrogen', component: HydrogenComponent, canActivate: [CanActivateViaAuthGuard] },
    // { path: 'crack', component: CrackComponent, canActivate: [CanActivateViaAuthGuard] },
    { path: 'fire', loadChildren: './fire-damage/fire-damage.module#FireDamageModule', canActivate: [CanActivateViaAuthGuard] },
    // { path: 'lamination', component: LaminationComponent, canActivate: [CanActivateViaAuthGuard] },
    // { path: 'fatigue', component: FatigueComponent, canActivate: [CanActivateViaAuthGuard] },
];

@NgModule({
    imports: [
        RouterModule.forChild(appRoutes)],
    exports: [],
    declarations: [

        // BrittleFractureComponent,
        // LocalMetalLossComponent,
        // GeneralMetalLossComponent,
        // PittingCorrosionComponent,
        // WeldMisalignmentComponent,
        // DentComponent,
        // MetalLossInputComponent,
        // CreepRuptureComponent,
        // HydrogenComponent,
        // CrackComponent,
        // LaminationComponent,
        // FatigueComponent,
        // MetalLossInputComponent,
        // CreepRuptureComponent,
        // HydrogenComponent,
        // CrackComponent,
        // LaminationComponent,
        // FatigueComponent,
    ],
    providers: [],
})
export class CalculationModule { }
