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
import { ColorRange } from './../../../model/colorRange';
import * as _ from 'underscore';

@Component({
  selector: 'app-metal-loss-input',
  templateUrl: './metal-loss-input.component.html',
  styleUrls: ['./metal-loss-input.component.scss']
})
export class MetalLossInputComponent extends InputBaseComponent implements OnInit, AfterViewInit {

  unit: Observable<IUnit>;
  form: FormGroup;
  thicknessDatas: Observable<KV[]>;
  thicknessImage: any;
  colorPickers: ColorRange[];
  arrayDatas: Array<Array<number>>;

  constructor(private http: Http) {
    super()
    this.colorPickers = [
      { min: 0, max: 1, color: 'red' },
      { min: 0, max: 1, color: 'blue' },
      { min: 0, max: 1, color: 'yellow' }
    ]
  }

  @ViewChildren(FFSInputBase) private inputs: QueryList<FFSInputBase>;

  ngOnInit() {
    this.unit = this.moduleEvent.unit.asObservable();
    this.thicknessDatas = this.http.get("/api/lookup/thicknessdatas")
      .map(response => response.json() as any[])
      .map(arr => arr.map(a => { return { key: a.thicknessDataID, value: a.thicknessDataName }; }));


    this.thicknessDatas.subscribe((m: KV[]) => {
      this.form.get('thicknessDataID').setValue(m[0].key);
    });
  }

  ngAfterViewInit(): void {
    this.form = FFSInputBase.toFormGroup(this.inputs);
    this.form.get('autoAllowableRSF').setValue(true);
    this.form.get('allowRSF').disable();


    this.form.get('thicknessDataID').valueChanges.subscribe(x => {
      this.thicknessImage = x;
    });

    this.form.get('excelDatas').valueChanges.subscribe(x => {
      if (x !== null && x !== undefined) {
        for (var i = 0; i < x.length; i++) {
          x[i] = _.toArray(x[i]);
        }
        console.log(x);
        this.form.get('numberOfInspectionRow').setValue(x.length);
        this.form.get('numberOfInspectionColumn').setValue(x[0].length);
      }
    });


  }
}
