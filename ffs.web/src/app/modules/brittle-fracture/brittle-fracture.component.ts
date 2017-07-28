import { DesignInputComponent } from './../common/design-input/design-input.component';
import { EquipmentInputComponent } from './../common/equipment-input/equipment-input.component';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';
import { ModuleBase } from './../module-base.component';
import { Component, OnInit, Injectable, Input, QueryList, ContentChildren, AfterContentInit, forwardRef, AfterViewInit, ViewChildren, ViewChild } from '@angular/core';
import { FFSInputBase } from "../../common/inputs/ffs-input-base";


@Component({
  selector: 'app-brittle-fracture',
  templateUrl: './brittle-fracture.component.html',
  styleUrls: ['./brittle-fracture.component.scss']
})
export class BrittleFractureComponent extends ModuleBase implements OnInit, AfterViewInit {

  @ViewChildren(FFSInputBase) inputs: QueryList<FFSInputBase>;


  @ViewChild(EquipmentInputComponent) equipmentInput: EquipmentInputComponent;
  @ViewChild(DesignInputComponent) designInput: DesignInputComponent;

  form: FormGroup;

  constructor() {
    super();
  }

  ngAfterViewInit(): void {

    // please see condition in UCDesign.cs line 110 - 180 in C# solution

    this.designInput.form.get("componentShapeID").disable();
    this.designInput.form.get("autoCalculateMinRequireThickness").setValue(true);
  }

  ngOnInit() {

  }

  onSubmit() {
    console.log(this.form.value);
  }

  initDesignInput() {
    this.designInput.init(this.equipmentInput.Inputs);
  }


}


