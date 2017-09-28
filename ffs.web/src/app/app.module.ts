import { DatePipe } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { FFSInputModule } from './app.modue.ffs-input';
import { CanActivateViaAuthGuard } from './can-activate-via-auth-guard';
import { FocusDirective } from './common/focus.directive';
import { FourthOrderPolyNomialComponent } from './common/inputs/fourth-order-poly-nomial/fourth-order-poly-nomial.component';
import { PanelComponent } from './common/panel/panel.component';
import { SidenavComponent } from './common/sidenav/sidenav.component';
import { TabItemComponent } from './common/tab/tab-item.component';
import { TabComponent } from './common/tab/tab.component';
import { TopnavComponent } from './common/topnav/topnav.component';
import { WizardComponent } from './common/wizard/wizard.component';
import { EventService } from './event.service';
import { HelpComponent } from './help/help.component';
import { HomeComponent } from './home/home.component';
import { BrittleFractureComponent } from './modules/brittle-fracture/brittle-fracture.component';
import { DesignInputComponent } from './modules/common/design-input/design-input.component';
import { EquipmentImageComponent } from './modules/common/equipment-image/equipment-image.component';
import { EquipmentInputComponent } from './modules/common/equipment-input/equipment-input.component';
import { LoadInputComponent } from './modules/common/load-input/load-input.component';
import { MaterialInputComponent } from './modules/common/material-input/material-input.component';
import { MetalLossInputComponent } from './modules/common/metal-loss-input/metal-loss-input.component';
import { ResultComponent } from './modules/common/result/result.component';
import { CrackComponent } from './modules/crack/crack.component';
import { CreepRuptureComponent } from './modules/creep-rupture/creep-rupture.component';
import { DentComponent } from './modules/dent/dent.component';
import { FatigueComponent } from './modules/fatigue/fatigue.component';
import { FireDamageComponent } from './modules/fire-damage/fire-damage.component';
import { GeneralMetalLossComponent } from './modules/general-metal-loss/general-metal-loss.component';
import { HydrogenComponent } from './modules/hydrogen/hydrogen.component';
import { LaminationComponent } from './modules/lamination/lamination.component';
import { LocalMetalLossComponent } from './modules/local-metal-loss/local-metal-loss.component';
import { PittingCorrosionComponent } from './modules/pitting-corrosion/pitting-corrosion.component';
import {
    DesignStepComponent,
    EquipmentStepComponent,
    FlawStepComponent,
    LoadsStepComponent,
    MaterialStepComponent,
    ResultStepComponent,
} from './modules/steps/steps.component';
import { WeldMisalignmentComponent } from './modules/weld-misalignment/weld-misalignment.component';
import { PlaygroundComponent } from './playground/playground.component';


const appRoutes: Routes = [
  { path: 'playground', component: PlaygroundComponent },
  { path: 'home', component: HomeComponent },
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
  { path: 'fatigue', component: FatigueComponent, canActivate: [CanActivateViaAuthGuard] },
  {
    path: '',
    redirectTo: '/home',
    pathMatch: 'full'
  }
];



@NgModule({
  declarations: [
    AppComponent,
    TopnavComponent,
    SidenavComponent,
    BrittleFractureComponent,
    WizardComponent,
    EquipmentStepComponent,
    DesignStepComponent,
    MaterialStepComponent,
    FlawStepComponent,
    LoadsStepComponent,
    ResultStepComponent,
    PanelComponent,
    EquipmentInputComponent,
    DesignInputComponent,
    MaterialInputComponent,
    LoadInputComponent,
    HomeComponent,
    ResultComponent,
    LocalMetalLossComponent,
    GeneralMetalLossComponent,
    TabComponent,
    TabItemComponent,
    PlaygroundComponent,
    PittingCorrosionComponent,
    WeldMisalignmentComponent,
    DentComponent,
    HelpComponent,
    FocusDirective,
    FourthOrderPolyNomialComponent,
    EquipmentImageComponent,
    MetalLossInputComponent,
    CreepRuptureComponent,
    HydrogenComponent,
    CrackComponent,
    FireDamageComponent,
    LaminationComponent,
    FatigueComponent,
  ],
  imports: [
    RouterModule.forRoot(
      appRoutes
    ),
    FFSInputModule,
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    HttpModule
  ],
  providers: [EventService, DatePipe, CanActivateViaAuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
