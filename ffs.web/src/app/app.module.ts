import { FFSInputModule } from './app.modue.ffs-input';

import { EquipmentStepComponent, DesignStepComponent, MaterialStepComponent, FlawStepComponent, LoadsStepComponent, ResultStepComponent } from './modules/steps/steps.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { TopnavComponent } from './common/topnav/topnav.component';
import { SidenavComponent } from './common/sidenav/sidenav.component';
import { BrittleFractureComponent } from './modules/brittle-fracture/brittle-fracture.component';
import { WizardComponent } from './common/wizard/wizard.component';
import { ReactiveFormsModule, FormsModule } from "@angular/forms";
import { PanelComponent } from './common/panel/panel.component';
import { EquipmentInputComponent } from './modules/common/equipment-input/equipment-input.component';
import { DesignInputComponent } from './modules/common/design-input/design-input.component';
import { MaterialInputComponent } from './modules/common/material-input/material-input.component';

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
    MaterialInputComponent
  ],
  imports: [
    FFSInputModule,
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
