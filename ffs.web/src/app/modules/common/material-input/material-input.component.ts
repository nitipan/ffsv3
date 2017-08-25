import { FFSSelectComponent } from './../../../common/inputs/ffs-input.component';
import { EventService } from './../../../event.service';
import { IUnit, SIUnit } from './../../../common/unit';
import { Observable } from 'rxjs/Rx';
import { FormGroup } from '@angular/forms';
import { KV } from './../../../model/kv';
import { Http } from '@angular/http';
import { Component, OnInit, AfterViewInit, QueryList, ViewChildren } from '@angular/core';
import { FFSInputBase } from "../../../common/inputs/ffs-input-base";
import { Subject } from "rxjs/Subject";
import { InputBaseComponent } from "../input-base.component";

@Component({
  selector: 'app-material-input',
  templateUrl: './material-input.component.html',
  styleUrls: ['./material-input.component.scss']
})
export class MaterialInputComponent extends InputBaseComponent implements OnInit, AfterViewInit {

  materialTypes: Observable<KV[]>;
  materials: Observable<KV[]>;
  asmeExemptionCurves: Observable<KV[]>;

  form: FormGroup;

  @ViewChildren(FFSInputBase) inputs: QueryList<FFSInputBase>;

  private materialObjects: any[];
  private currentMaterial: any;

  showOther: boolean = false;

  private materialSubject: Subject<any> = new Subject();

  unit: Observable<IUnit>;

  currentUnit: IUnit = new SIUnit();

  assesmentLevel: number;
  constructor(private http: Http) {
    super();

  }

  ngOnInit() {
    this.unit = this.moduleEvent.unit.asObservable();

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


    this.asmeExemptionCurves = this.http.get("/api/lookup/asmeexemptioncurves")
      .map(response => response.json() as any[])
      .map(arr => arr.map(a => { return { key: a.aSMEExemptionCurvesID, value: a.aSMEExemptionCurvesName }; }));

    this.asmeExemptionCurves.subscribe((m: KV[]) => {
      this.form.get("asmeExemptionCurvesID").setValue(m[0].key);
    });


  }

  optionReady(select: FFSSelectComponent) {
    if (select.options.length > 0)
      select.setValue(select.options[0].key);
    else
      select.setValue("");
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
      if (this.assesmentLevel != 1) {
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
      }
    });

    this.materialSubject.subscribe(m => {

      if (m.curve != null) {
        this.form.get("asmeExemptionCurvesID").setValue(m.curve);
        this.form.get("asmeExemptionCurvesID").disable();
      } else {
        this.form.get("asmeExemptionCurvesID").enable();
      }

      if (this.currentUnit instanceof SIUnit) {
        this.form.get("allowableStress").setValue(m.allowableStress);
        this.form.get("ultimatedTensileStrength").setValue(m.tensileStrength);
        this.form.get("youngModulus").setValue(m.youngModulas);
        this.form.get("yieldStrength").setValue(m.yieldStrength);
        this.form.get("poissonRatio").setValue(m.possionRatio);
      } else {
        this.form.get("allowableStress").setValue(m.allowableStressKSI);
        this.form.get("ultimatedTensileStrength").setValue(m.tensileStrengthKSI);
        this.form.get("youngModulus").setValue(m.youngModulasKSI);
        this.form.get("yieldStrength").setValue(m.yieldStrengthKSI);
        this.form.get("poissonRatio").setValue(m.possionRatio);
      }

    });


    this.unit.subscribe(u => {
      this.currentUnit = u;
      this.form.get("material").setValue(this.form.get("material").value);
    });

    this.moduleEvent.assessmentLevelSubject.subscribe(assessmentLevel => {
      this.assesmentLevel = assessmentLevel;
      if (assessmentLevel == 1) {
        this.form.get('allowableStress').disable();
        this.form.get('ultimatedTensileStrength').disable();
        this.form.get('yieldStrength').disable();
        this.form.get("automaticallyCalculationAllowableStress").disable();
      } else {
        let autoCalChkbox = this.form.get("automaticallyCalculationAllowableStress").value;
        if (autoCalChkbox) {
          this.form.get("allowableStress").disable();
          this.form.get("yieldStrength").enable();
          this.form.get("ultimatedTensileStrength").enable();
        } else {
          this.form.get("allowableStress").enable();
          this.form.get("yieldStrength").disable();
          this.form.get("ultimatedTensileStrength").disable();
        }
      }
    });



  }
}
