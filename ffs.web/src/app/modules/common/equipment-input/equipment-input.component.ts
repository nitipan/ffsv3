import { FFSSelectComponent } from './../../../common/inputs/ffs-input.component';
import { SIUnit, MatricUnit } from './../../../common/unit';
import { EventService } from './../../../event.service';
import { InputBase } from './../../../model/inputbase';
import { ModuleBase } from './../../module-base.component';
import { FormGroup } from '@angular/forms';
import { KV } from './../../../model/kv';
import { Component, OnInit, ViewChildren, QueryList, AfterViewInit } from '@angular/core';
import { Http } from "@angular/http";
import 'rxjs/Rx';
import { Observable } from "rxjs/Rx";
import { FFSInputBase } from "../../../common/inputs/ffs-input-base";
import { InputBaseComponent } from "../input-base.component";

@Component({
  selector: 'app-equipment-input',
  templateUrl: './equipment-input.component.html',
  styleUrls: ['./equipment-input.component.scss']
})
export class EquipmentInputComponent extends InputBaseComponent implements OnInit, AfterViewInit {

  equipmentTypes: Observable<KV[]>;
  methodologies: Observable<KV[]>;
  units: Observable<KV[]>
  assessmentLevel: Observable<KV[]>;


  form: FormGroup;

  currentValue: InputBase;

  @ViewChildren(FFSInputBase) private inputs: QueryList<FFSInputBase>;

  constructor(private http: Http) {
    super();
  }

  ngOnInit() {
    this.equipmentTypes = this.http.get("/api/lookup/equipmenttypes")
      .map(response => response.json() as any[])
      .map(arr => arr.map(a => { return { key: a.equipmentTypeID, value: a.equipmentTypeName }; }));

    this.methodologies = this.http.get("/api/lookup/methodologies")
      .map(response => response.json() as any[])
      .map(arr => arr.map(a => { return { key: a.methodologyID, value: a.methodologyName }; }));

    this.units = this.http.get("/api/lookup/units")
      .map(response => response.json() as any[])
      .map(arr => arr.map(a => { return { key: a.unitID, value: a.unitName }; }));

    this.assessmentLevel = this.http.get("/api/lookup/assessmentLevel")
      .map(response => response.json() as any[])
      .map(arr => arr.map(a => { return { key: a.assessmentLevelID, value: a.assessmentLevelName }; }));
    this.assessmentLevel.subscribe((m: KV[]) => {
      this.form.get("assessmentLevel").setValue(m[0].key);
    });
  }

  ngAfterViewInit(): void {
    this.form = FFSInputBase.toFormGroup(this.inputs);

    this.form.valueChanges.subscribe((v) => {
      this.currentValue = v as InputBase;

      this.moduleEvent.equipmentInputSubject.emit(v);
    });

    this.form.get("unitID").valueChanges.subscribe(v => {
      this.moduleEvent.unit.next(v == 1 ? new SIUnit() : new MatricUnit());
    });
    this.form.get("unitID").setValue(1);

    this.form.get("equipmentType").valueChanges.subscribe(v => {
      this.moduleEvent.equipmentTypeSubject.emit(v);
    });
  }

  get Inputs(): InputBase {
    return this.form.value as InputBase;
  }
}
