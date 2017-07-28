import { Observable } from 'rxjs/Rx';
import { FormGroup } from '@angular/forms';
import { KV } from './../../../model/kv';
import { Http } from '@angular/http';
import { Component, OnInit, AfterViewInit, QueryList, ViewChildren } from '@angular/core';
import { FFSInputBase } from "../../../common/inputs/ffs-input-base";
import { Subject } from "rxjs/Subject";

@Component({
  selector: 'app-material-input',
  templateUrl: './material-input.component.html',
  styleUrls: ['./material-input.component.scss']
})
export class MaterialInputComponent implements OnInit, AfterViewInit {

  private materialTypes: Observable<KV[]>;
  private materials: Observable<KV[]>;
  private asmeExemptionCurves: Observable<KV[]>;

  form: FormGroup;

  @ViewChildren(FFSInputBase) private inputs: QueryList<FFSInputBase>;

  private materialObjects: any[];
  private currentMaterial: any;

  private showOther: boolean = false;

  private materialSubject: Subject<any> = new Subject();

  constructor(private http: Http) { }

  ngOnInit() {
    this.materialTypes = this.http.get("/api/lookup/materialtypes")
      .map(response => response.json() as any[])
      .map(arr => arr.map(a => { return { key: a.materialTypeID, value: a.materialTypeName }; }));

    this.materialTypes.subscribe((m: KV[]) => {
      this.form.get("materialTypeID").setValue(m[0].key);
    });

    this.materials = this.http.get("/api/lookup/materials")
      .map(response => response.json() as any[])
      .do(m => {
        this.materialObjects = m;
      })
      .map(arr => arr.map(a => { return { key: a.materialID, value: a.materialName }; }));

    this.materials.subscribe((m: KV[]) => {
      this.form.get("material").setValue(m[0].key);
    });

    this.asmeExemptionCurves = this.http.get("/api/lookup/asmeexemptioncurves")
      .map(response => response.json() as any[])
      .map(arr => arr.map(a => { return { key: a.aSMEExemptionCurvesID, value: a.aSMEExemptionCurvesName }; }));

    this.asmeExemptionCurves.subscribe((m: KV[]) => {
      this.form.get("asmeExemptionCurvesID").setValue(m[0].key);
    });
  }

  ngAfterViewInit(): void {
    this.form = FFSInputBase.toFormGroup(this.inputs);
    this.form.get("material").valueChanges.subscribe(m => {
      this.showOther = m == '999';
      this.materialSubject.next(this.materialObjects.find(o => o.materialID == m));
    });

    setTimeout(() => {
      this.form.get("automaticallyCalculationAllowableStress").setValue(true);

      this.form.get("automaticCalculationReferenceTemperature").setValue(true);
      this.form.get("automaticCalculationReferenceTemperature").disable();
    });

    this.form.get("automaticCalculationReferenceTemperature").valueChanges.subscribe((v: boolean) => {
      if (v)
        this.form.get("referenceTemperature").disable();
      else
        this.form.get("referenceTemperature").enable();
    });

    this.form.get("automaticallyCalculationAllowableStress").valueChanges.subscribe((v: boolean) => {
      if (v) {
        this.form.get("allowableStress").disable();
        this.form.get("yieldStrength").enable();
        this.form.get("ultimatedTensileStrength").enable();
        this.form.get("poissonRatio").disable();
        this.form.get("youngModulus").disable();
      } else {
        this.form.get("allowableStress").enable();
        this.form.get("yieldStrength").disable();
        this.form.get("ultimatedTensileStrength").disable();
        this.form.get("poissonRatio").disable();
        this.form.get("youngModulus").disable();
      }
    });

    this.materialSubject.subscribe(m => {

      if (m.curve != null) {
        this.form.get("asmeExemptionCurvesID").setValue(m.curve);
        this.form.get("asmeExemptionCurvesID").disable();
      } else {
        this.form.get("asmeExemptionCurvesID").enable();
      }

      // get by unit
      this.form.get("allowableStress").setValue(m.yieldStrength);
      this.form.get("ultimatedTensileStrength").setValue(m.tensileStrength);
      this.form.get("youngModulus").setValue(m.youngModulas);
      this.form.get("yieldStrength").setValue(m.yieldStrength);
      this.form.get("poissonRatio").setValue(m.possionRatio);
    });




  }
}
