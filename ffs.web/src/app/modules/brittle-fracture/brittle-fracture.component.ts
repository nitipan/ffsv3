import { InputBase } from './../../model/inputbase';
import { EventService } from './../../event.service';
import { DesignInputComponent } from './../common/design-input/design-input.component';
import { EquipmentInputComponent } from './../common/equipment-input/equipment-input.component';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';
import { ModuleBase } from './../module-base.component';
import { Component, OnInit, Injectable, Input, QueryList, ContentChildren, AfterContentInit, forwardRef, AfterViewInit, ViewChildren, ViewChild, AfterViewChecked, ChangeDetectorRef } from '@angular/core';
import { FFSInputBase } from "../../common/inputs/ffs-input-base";


@Component({
  selector: 'app-brittle-fracture',
  templateUrl: './brittle-fracture.component.html',
  styleUrls: ['./brittle-fracture.component.scss']
})
export class BrittleFractureComponent extends ModuleBase implements OnInit, AfterViewInit {

  // MUST BE NAMED
  name = "Brittle Fracture";

  @ViewChildren(FFSInputBase) inputs: QueryList<FFSInputBase>;

  @ViewChild(EquipmentInputComponent) equipmentInput: EquipmentInputComponent;
  @ViewChild(DesignInputComponent) designInput: DesignInputComponent;

  form: FormGroup;

  constructor(private cdRef: ChangeDetectorRef, eventService: EventService) {
    super(eventService);
  }

  ngAfterViewInit(): void {
    this.equipmentInput.form.valueChanges.subscribe(f => {
      let inputs = f as InputBase;
      this.valueChangedSubject.next(inputs);
    });
    // please see condition in UCDesign.cs line 110 - 180 in C# solution


    this.designInput.form.get("componentShapeID").disable();
    this.designInput.form.get("autoCalculateMinRequireThickness").setValue(true);

    // !!! NEED THIS LINE TO TELL ANGULAR THERE ARE FORM INPUT CHANGE ABOVE
    this.cdRef.detectChanges();


  }

  // get level(): number {
  //   if (this.equipmentInput.form == undefined)
  //     return null;

  //   return this.equipmentInput.form.get("assessmentLevel").value;
  // }

  ngOnInit() {

  }

  onSubmit() {
    console.log(this.form.value);
  }

  initDesignInput() {
    this.designInput.init(this.equipmentInput.Inputs);
  }

  initMaterialInput() {

  }

  initFlawInput() {

  }
  initLoadInput() {

  }


}


