import { IUnit } from './../../common/unit';
import { Http } from '@angular/http';
import { KV } from './../../model/kv';
import { Observable } from 'rxjs/Rx';
import { LoadInputComponent } from './../common/load-input/load-input.component';
import { MaterialInputComponent } from './../common/material-input/material-input.component';
import { InputBase } from './../../model/inputbase';
import { EventService } from './../../event.service';
import { DesignInputComponent } from './../common/design-input/design-input.component';
import { EquipmentInputComponent } from './../common/equipment-input/equipment-input.component';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';
import { ModuleBase } from './../module-base.component';
import { Component, OnInit, Injectable, Input, QueryList, ContentChildren, AfterContentInit, forwardRef, AfterViewInit, ViewChildren, ViewChild, AfterViewChecked, ChangeDetectorRef } from '@angular/core';
import { FFSInputBase } from '../../common/inputs/ffs-input-base';
import { routerTransition } from "../../common/router.animations";

@Component({
  selector: 'app-general-metal-loss',
  templateUrl: './general-metal-loss.component.html',
  styleUrls: ['./general-metal-loss.component.scss'],
  animations: [routerTransition()],
  host: { '[@routerTransition]': '' }
})
export class GeneralMetalLossComponent extends ModuleBase implements OnInit, AfterViewInit {


  name = 'General Metal Loss';

  @ViewChildren(FFSInputBase) inputs: QueryList<FFSInputBase>;

  @ViewChild(EquipmentInputComponent) equipmentInput: EquipmentInputComponent;
  @ViewChild(DesignInputComponent) designInput: DesignInputComponent;
  @ViewChild(MaterialInputComponent) materialInput: MaterialInputComponent;
  @ViewChild(LoadInputComponent) loadInput: LoadInputComponent;

  form: FormGroup;

  thicknessDatas: Observable<KV[]>;
  unit: Observable<IUnit>;

  constructor(private http: Http,
    private cdRef: ChangeDetectorRef,
    eventService: EventService) {
    super(eventService);
    this.unit = this.eventService.unit.asObservable();
  }


  ngAfterViewInit(): void {
    this.form = FFSInputBase.toFormGroup(this.inputs);



    // calculate
    this.eventService.requestCalculateSubject.subscribe(() => {
      // TODO check form valid ?

      // NEED GET RAWDATA because to include disabled value
      const equipmentInput = this.equipmentInput.form.getRawValue() as InputBase;
      const designInput = this.designInput.form.getRawValue() as InputBase;
      const materialInput = this.materialInput.form.getRawValue() as InputBase;
      const loadInput = this.loadInput.form.getRawValue() as InputBase;
      const flawInput = this.form.getRawValue();

      // merge
      const calculationParam = {
        ...equipmentInput,
        ...designInput,
        ...materialInput,
        ...flawInput,
        ...loadInput
      };

      this.eventService.calculatingSubject.emit(null);

      this.http.post(`/api/localmetalloss/calculation/level${equipmentInput.assessmentLevel}/unit${equipmentInput.unitID}`, calculationParam)
        .map(r => r.json())
        .subscribe(r => {
          this.eventService.calculatingSubject.emit(r);
          this.eventService.calculatedSubject.emit({ param: calculationParam, result: r });
        });
    });

    // please see condition in UCDesign.cs line 110 - 180 in C# solution
    // this.form.get('ThicknessDataTypeID').disable();

    this.form.get('autoAllowableRSF').setValue(true);
    this.form.get('allowRSF').disable();
    this.form.get('thicknessDataID').disable();
    this.designInput.form.get('autoCalculateMinRequireThickness').setValue(true);

    // !!! NEED THIS LINE TO TELL ANGULAR THERE ARE FORM INPUT CHANGE ABOVE
    this.cdRef.detectChanges();
  }

  ngOnInit() {
    this.thicknessDatas = this.http.get("/api/lookup/thicknessdatas")
      .map(response => response.json() as any[])
      .map(arr => arr.map(a => { return { key: a.thicknessDataID, value: a.thicknessDataName }; }));
  }


  initDesignInput() {
    // NO NEED
  }

  initMaterialInput() {

  }

  initFlawInput() {

  }
  initLoadInput() {

  }

}
