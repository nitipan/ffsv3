import { FormGroup } from '@angular/forms';
import { KV } from './../../../model/kv';
import { Observable } from 'rxjs/Rx';
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
  form: FormGroup;

  @ViewChildren(FFSInputBase) private inputs: QueryList<FFSInputBase>;

  private materialObjects: any[];
  private currentMaterial: any;

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


  }

  ngAfterViewInit(): void {
    this.form = FFSInputBase.toFormGroup(this.inputs);
    this.form.get("material").valueChanges.subscribe(m => {
      this.materialSubject.next(this.materialObjects.find(o => o.materialID == m));
    });


    this.materialSubject.subscribe(m => {
      this.form.get("allowableStress").setValue(m.yieldStrength);
      this.form.get("ultimatedTensileStrength").setValue(m.tensileStrength);
      this.form.get("youngModulus").setValue(m.youngModulas);
      this.form.get("yieldStrength").setValue(m.yieldStrength);
      this.form.get("poissonRatio").setValue(m.possionRatio);
    });


  }
}
