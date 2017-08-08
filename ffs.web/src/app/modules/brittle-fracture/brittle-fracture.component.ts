import { ResultComponent } from './../common/result/result.component';
import { IUnit } from './../../common/unit';
import { Subject } from 'rxjs/Subject';
import { Observable } from 'rxjs/Rx';
import { Http } from '@angular/http';
import { KV } from './../../model/kv';
import { LoadInputComponent } from './../common/load-input/load-input.component';
import { MaterialInputComponent } from './../common/material-input/material-input.component';
import { InputBase } from './../../model/inputbase';
import { EventService } from './../../event.service';
import { DesignInputComponent } from './../common/design-input/design-input.component';
import { EquipmentInputComponent } from './../common/equipment-input/equipment-input.component';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';
import { ModuleBase } from './../module-base.component';
import { Component, OnInit, Injectable, Input, QueryList, ContentChildren, AfterContentInit, forwardRef, AfterViewInit, ViewChildren, ViewChild, AfterViewChecked, ChangeDetectorRef } from '@angular/core';
import { FFSInputBase } from "../../common/inputs/ffs-input-base";



import { routerTransition } from "../../common/router.animations";

@Component({
  selector: 'app-brittle-fracture',
  templateUrl: './brittle-fracture.component.html',
  styleUrls: ['./brittle-fracture.component.scss'],
  animations: [routerTransition()],
  host: { '[@routerTransition]': '' }
})
export class BrittleFractureComponent extends ModuleBase implements OnInit, AfterViewInit {

  // MUST BE NAMED
  name = "Brittle Fracture";

  @ViewChildren(FFSInputBase) inputs: QueryList<FFSInputBase>;

  @ViewChild(EquipmentInputComponent) equipmentInput: EquipmentInputComponent;
  @ViewChild(DesignInputComponent) designInput: DesignInputComponent;
  @ViewChild(MaterialInputComponent) materialInput: MaterialInputComponent;
  @ViewChild(LoadInputComponent) loadInput: LoadInputComponent;
  @ViewChild(ResultComponent) result: ResultComponent;

  form: FormGroup;
  unit: Observable<IUnit>;
  stressRatios: Observable<KV[]>;

  constructor(
    private http: Http,
    private cdRef: ChangeDetectorRef,
    eventService: EventService) {
    super(eventService);
    this.unit = this.moduleEvent.unit.asObservable();
  }

  ngAfterViewInit(): void {
    this.form = FFSInputBase.toFormGroup(this.inputs);

    this.result.reportFactory = this.reportFactory;
    this.result.summaryFactory = this.summaryFactory;

    this.equipmentInput.form.valueChanges.subscribe(f => {
      let inputs = f as InputBase;
      this.valueChangedSubject.next(inputs);
    });

    // calculate
    this.moduleEvent.requestCalculateSubject.subscribe(() => {


      // TODO check form valid ?

      // NEED GET RAWDATA because to include disabled value
      let equipmentInput = this.equipmentInput.form.getRawValue() as InputBase;
      let designInput = this.designInput.form.getRawValue() as InputBase;
      let materialInput = this.materialInput.form.getRawValue() as InputBase;
      let loadInput = this.loadInput.form.getRawValue() as InputBase;
      let flawInput = this.form.getRawValue();

      // merge
      let calculationParam = {
        ...equipmentInput,
        ...designInput,
        ...materialInput,
        ...flawInput,
        ...loadInput
      }


      this.moduleEvent.calculatingSubject.emit(null);

      this.http.post(`/api/brittle/calculation/level${equipmentInput.assessmentLevel}/unit${equipmentInput.unitID}`, calculationParam)
        .map(r => r.json())
        .subscribe(r => {
          this.moduleEvent.calculatingSubject.emit(r);
          this.moduleEvent.calculatedSubject.emit({ param: calculationParam, result: r, module: this });
        });
    });

    // please see condition in UCDesign.cs line 110 - 180 in C# solution

    this.form.get("AutomaticcallyTheMinimumAllowableTemperature").valueChanges.subscribe((v: boolean) => {
      if (v)
        this.form.get("TheMinimumAllowableTemperature").disable();
      else
        this.form.get("TheMinimumAllowableTemperature").enable();
    });

    this.designInput.form.get("componentShapeID").disable();
    this.designInput.form.get("autoCalculateMinRequireThickness").setValue(true);
    this.form.get("ReductionInTheMATID").disable();
    this.form.get("AutomaticcallyTheMinimumAllowableTemperature").disable();
    this.form.get("AutomaticcallyTheMinimumAllowableTemperature").setValue(true);
    // !!! NEED THIS LINE TO TELL ANGULAR THERE ARE FORM INPUT CHANGE ABOVE
    this.cdRef.detectChanges();
  }

  ngOnInit() {
    this.stressRatios = this.http.get("/api/lookup/reductions")
      .map(response => response.json() as any[])
      .map(arr => arr.map(a => { return { key: a.reductionInTheMATID, value: a.reductionInTheMATName }; }));
  }

  initDesignInput() {

  }

  initMaterialInput() {

  }

  initFlawInput() {

  }

  initLoadInput() {

  }



  reportFactory(result) {
    var content: any[] = [
      { text: "STEP 1 – Determine the starting point for the MAT", style: 'h3' },
      { text: "STEP 1.1 – Determine the following parameters", style: 'h4' },
      { text: "A. Nominal uncorroded thickness at each weld joint", style: 'h5' },
      { text: "A. Nominal uncorroded thickness at each weld joint", style: 'h5' }
    ];
    return content;
  }

  summaryFactory(result) {
    var content: any[] = [
      { text: "mix code", style: 'h3' },
      { text: "STEP 1.1 – Determine the following parameters", style: 'h4' },
      { text: "A. Nominal uncorroded thickness at each weld joint", style: 'h5' },
      { text: "A. Nominal uncorroded thickness at each weld joint", style: 'h5' }
    ];
    return content;
  }

}


