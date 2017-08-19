import { CanActivateViaAuthGuard } from './can-activate-via-auth-guard';
import { DatePipe } from '@angular/common';
import { TabItemComponent } from './common/tab/tab-item.component';
import { EventService } from './event.service';
import { FFSInputModule } from './app.modue.ffs-input';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { EquipmentStepComponent, DesignStepComponent, MaterialStepComponent, FlawStepComponent, LoadsStepComponent, ResultStepComponent } from './modules/steps/steps.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { TopnavComponent } from './common/topnav/topnav.component';
import { SidenavComponent } from './common/sidenav/sidenav.component';
import { BrittleFractureComponent } from './modules/brittle-fracture/brittle-fracture.component';
import { WizardComponent } from './common/wizard/wizard.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { PanelComponent } from './common/panel/panel.component';
import { EquipmentInputComponent } from './modules/common/equipment-input/equipment-input.component';
import { DesignInputComponent } from './modules/common/design-input/design-input.component';
import { MaterialInputComponent } from './modules/common/material-input/material-input.component';
import { LoadInputComponent } from './modules/common/load-input/load-input.component';
import { HomeComponent } from './home/home.component';
import { ResultComponent } from './modules/common/result/result.component';
import { LocalMetalLossComponent } from './modules/local-metal-loss/local-metal-loss.component';
import { GeneralMetalLossComponent } from './modules/general-metal-loss/general-metal-loss.component';
import { TabComponent } from './common/tab/tab.component';
import { PlaygroundComponent } from './playground/playground.component';
import { PittingCorrosionComponent } from './modules/pitting-corrosion/pitting-corrosion.component';
import { WeldMisalignmentComponent } from './modules/weld-misalignment/weld-misalignment.component';
import { DentComponent } from './modules/dent/dent.component';
import { HelpComponent } from './help/help.component';
import { FocusDirective } from './common/focus.directive';
import { FourthOrderPolyNomialComponent } from './common/inputs/fourth-order-poly-nomial/fourth-order-poly-nomial.component';
import { EquipmentImageComponent } from './modules/common/equipment-image/equipment-image.component';


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
