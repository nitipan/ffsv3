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

@Component({
  selector: 'app-local-metal-loss',
  templateUrl: './local-metal-loss.component.html',
  styleUrls: ['./local-metal-loss.component.scss']
})
export class LocalMetalLossComponent extends ModuleBase implements OnInit, AfterViewInit {

  name = 'Local Metal Loss';

  @ViewChildren(FFSInputBase) inputs: QueryList<FFSInputBase>;

  @ViewChild(EquipmentInputComponent) equipmentInput: EquipmentInputComponent;
  @ViewChild(DesignInputComponent) designInput: DesignInputComponent;
  @ViewChild(MaterialInputComponent) materialInput: MaterialInputComponent;
  @ViewChild(LoadInputComponent) loadInput: LoadInputComponent;

  form: FormGroup;

  stressRatios: Observable<KV[]>;

  constructor(
    private http: Http,
    private cdRef: ChangeDetectorRef,
    eventService: EventService) {
    super(eventService);
  }

  ngAfterViewInit(): void {
    this.form = FFSInputBase.toFormGroup(this.inputs);

    this.equipmentInput.form.valueChanges.subscribe(f => {
      const inputs = f as InputBase;
      this.valueChangedSubject.next(inputs);
    });

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

    this.form.get('AutomaticcallyTheMinimumAllowableTemperature').valueChanges.subscribe((v: boolean) => {
      if (v) {
        this.form.get('TheMinimumAllowableTemperature').disable();
      } else {
        this.form.get('TheMinimumAllowableTemperature').enable();
      }
    });

    this.designInput.form.get('componentShapeID').disable();
    this.designInput.form.get('autoCalculateMinRequireThickness').setValue(true);
    this.form.get('ReductionInTheMATID').disable();
    this.form.get('AutomaticcallyTheMinimumAllowableTemperature').disable();
    this.form.get('AutomaticcallyTheMinimumAllowableTemperature').setValue(true);
    // !!! NEED THIS LINE TO TELL ANGULAR THERE ARE FORM INPUT CHANGE ABOVE
    this.cdRef.detectChanges();
  }

  // get level(): number {
  //   if (this.equipmentInput.form == undefined)
  //     return null;

  //   return this.equipmentInput.form.get("assessmentLevel").value;
  // }

  ngOnInit() {
    this.stressRatios = this.http.get('/api/lookup/reductions')
      .map(response => response.json() as any[])
      .map(arr => arr.map(a => { return { key: a.reductionInTheMATID, value: a.reductionInTheMATName }; }));
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
