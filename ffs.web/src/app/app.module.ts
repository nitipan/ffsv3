import { FFSTextComponent, FFSSelectComponent, FFSCheckComponent } from './common/inputs/ffs-input.component';


import { EquipmentStepComponent, DesignStepComponent, MaterialStepComponent, FlawStepComponent, LoadsStepComponent, ResultStepComponent } from './modules/steps/steps.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { TopnavComponent } from './common/topnav/topnav.component';
import { SidenavComponent } from './common/sidenav/sidenav.component';
import { BrittleFractureComponent } from './modules/brittle-fracture/brittle-fracture.component';
import { WizardComponent } from './common/wizard/wizard.component';
import { ReactiveFormsModule, FormsModule } from "@angular/forms";
import { PanelComponent } from './common/panel/panel.component';

@NgModule({
  declarations: [
    FFSTextComponent,
    FFSSelectComponent,
    FFSCheckComponent,
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
    PanelComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
