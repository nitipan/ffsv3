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
      if (v) {
        this.form.get("minRequireLongitutinalThickness").disable();
        this.form.get("minRequireCircumferentialThickness").disable();
      } else {
        this.form.get("minRequireLongitutinalThickness").enable();
        this.form.get("minRequireCircumferentialThickness").enable();
      }
    });

    this.form.get("componentShapeID").setValue("");
    this.componentShape = this.http.get(`/api/lookup/componentshapes`)
      .map(response => response.json() as any[])
      .map(arr => arr.map(a => { return { key: a.componentShapeID, value: a.componentShapeName }; }));

    this.componentShape.subscribe(c => {
      if (c.length > 0 && this.form.get("componentShapeID").value === "") {
        this.form.get("componentShapeID").setValue(c[0].key);
      }
    });


    this.moduleEvent.equipmentTypeSubject.subscribe(equipmentType => {

      this.form.get("componentTypeID").setValue("");
      this.componentType = this.http.get(`/api/lookup/componenttypes/${equipmentType}`)
        .map(response => response.json() as any[])
        .map(arr => arr.map(a => { return { key: a.componentTypeID, value: a.componentTypeName }; }));

      this.componentType.subscribe(c => {
        if (c.length > 0 && this.form.get("componentTypeID").value === "") {
          this.form.get("componentTypeID").setValue(c[0].key);
        }
      });


      this.form.get("designCode").setValue("");
      this.codeDesign = this.http.get(`/api/lookup/designcodes/${equipmentType}`)
        .map(response => response.json() as any[])
        .map(arr => arr.map(a => { return { key: a.designCodeID, value: a.designCodeName }; }));

      this.codeDesign.subscribe(c => {
        if (c.length > 0 && this.form.get("designCode").value === "") {
          this.form.get("designCode").setValue(c[0].key);
        }
      });
    });


    // SPECIAL CODE
    this.moduleEvent.equipmentTypeSubject.emit(1);
  }




}
