import { FFSSelectComponent } from './../../../common/inputs/ffs-input.component';
import { IUnit } from './../../../common/unit';
import { Observable } from 'rxjs/Rx';
import { EventService } from './../../../event.service';
import { FormGroup } from '@angular/forms';
import { InputBase } from './../../../model/inputbase';
import { Http } from '@angular/http';
import { KV } from './../../../model/kv';
import { Component, OnInit, ViewChildren, QueryList, AfterViewInit } from '@angular/core';
import { FFSInputBase } from "../../../common/inputs/ffs-input-base";
import { InputBaseComponent } from "../input-base.component";

@Component({
  selector: 'app-design-input',
  templateUrl: './design-input.component.html',
  styleUrls: ['./design-input.component.scss']
})
export class DesignInputComponent extends InputBaseComponent implements OnInit, AfterViewInit {

  componentType: Observable<KV[]>;
  componentShape: Observable<KV[]>;

  codeDesign: Observable<KV[]>;

  unit: Observable<IUnit>;

  assesmentLevel: number;

  constructor(private http: Http) {
    super();

  }
  form: FormGroup;

  @ViewChildren(FFSInputBase) private inputs: QueryList<FFSInputBase>;

  ngOnInit() {
    this.unit = this.moduleEvent.unit.asObservable();
  }

  ngAfterViewInit(): void {

    this.form = FFSInputBase.toFormGroup(this.inputs);
    this.form.get("autoCalculateMinRequireThickness").valueChanges.subscribe((v: boolean) => {
      if (this.assesmentLevel != 1) {
        if (v) {
          this.form.get("minRequireLongitutinalThickness").disable();
          this.form.get("minRequireCircumferentialThickness").disable();
        } else {
          this.form.get("minRequireLongitutinalThickness").enable();
          this.form.get("minRequireCircumferentialThickness").enable();
        }
      }
    });

    this.form.get("componentShapeID").setValue("");
    this.componentShape = this.http.get(`/api/lookup/componentshapes`)
      .map(response => response.json() as any[])
      .map(arr => arr.map(a => { return { key: a.componentShapeID, value: a.componentShapeName }; }));


    this.moduleEvent.equipmentTypeSubject.subscribe(equipmentType => {

      this.form.get("componentTypeID").setValue("");
      this.componentType = this.http.get(`/api/lookup/componenttypes/${equipmentType}`)
        .map(response => response.json() as any[])
        .map(arr => arr.map(a => { return { key: a.componentTypeID, value: a.componentTypeName }; }));

      this.form.get("designCode").setValue("");
      this.codeDesign = this.http.get(`/api/lookup/designcodes/${equipmentType}`)
        .map(response => response.json() as any[])
        .map(arr => arr.map(a => { return { key: a.designCodeID, value: a.designCodeName }; }));

      if (equipmentType == 1) {
        this.form.get("componentShapeID").disable();
        this.form.get("componentShapeID").setValue(1);
      } else {
        this.form.get("componentShapeID").enable();
      }
    });

    this.moduleEvent.assessmentLevelSubject.subscribe(assessmentLevel => {
      this.assesmentLevel = assessmentLevel;

      if (assessmentLevel == 1) {
        this.form.get('insideDiameter').disable();
        this.form.get('designPressure').disable();
        this.form.get('weldJointEfficiency').disable();
        this.form.get("minRequireLongitutinalThickness").disable();
        this.form.get("minRequireCircumferentialThickness").disable();
      } else {
        this.form.get('insideDiameter').enable();
        this.form.get('designPressure').enable();
        this.form.get('weldJointEfficiency').enable();
      }
    });

    this.form.valueChanges.subscribe((v) => {
      this.moduleEvent.designInputSubject.emit(v);
    });
    // SPECIAL CODE
    this.moduleEvent.equipmentTypeSubject.emit(1);
  }

  optionReady(select: FFSSelectComponent) {
    if (select.options.length > 0)
      select.setValue(select.options[0].key);
    else
      select.setValue("");
  }


}
