import { FFSSelectComponent } from './../../../common/inputs/ffs-input.component';
import { IUnit } from './../../../common/unit';
import { Observable } from 'rxjs/Rx';
import { EventService } from './../../../event.service';
import { FormGroup } from '@angular/forms';
import { InputBase } from './../../../model/inputbase';
import { Http } from '@angular/http';
import { KV } from './../../../model/kv';
import { Component, OnInit, ViewChildren, QueryList, AfterViewInit, ChangeDetectorRef } from '@angular/core';
import { FFSInputBase } from '../../../common/inputs/ffs-input-base';
import { InputBaseComponent } from '../input-base.component';
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
  thicknessId: any;
  thicknessImage: any;
  colorPickers: ColorRange[];
  arrayDatas: any;
  limitRow: number;
  limitColumn: number;
  columnChartData: any;
  rowChartData: any;

  constructor(private http: Http, private cdRef: ChangeDetectorRef) {
    super();
  }

  @ViewChildren(FFSInputBase) private inputs: QueryList<FFSInputBase>;

  ngOnInit() {
    this.unit = this.moduleEvent.unit.asObservable();
    this.thicknessDatas = this.http.get('/api/lookup/thicknessdatas')
      .map(response => response.json() as any[])
      .map(arr => arr.map(a => { return { key: a.thicknessDataID, value: a.thicknessDataName }; }));

  }

  ngAfterViewInit(): void {
    this.form = FFSInputBase.toFormGroup(this.inputs);

    // init color
    this.form.get('color1').setValue('#ed0404');
    this.form.get('color2').setValue('#f1f700');
    this.form.get('color3').setValue('#24db15');


    // common behaviour
    this.form.get('autoAllowableRSF').setValue(true);
    this.form.get('allowRSF').disable();

    this.form.get('thicknessDataID').valueChanges.subscribe(x => {
      this.thicknessImage = x;
    });

    this.form.get('thicknessDataID').valueChanges.subscribe(x => {
      this.thicknessId = x;
      if (x == 1) {
        this.form.get('widthOfTheLongGrid').disable();
        this.form.get('widthOfTheCirGrid').disable();
        this.form.get('numberOfInspectionRow').disable();
        this.form.get('numberOfInspectionRow').setValue(2);
        this.limitColumn = 1;
      } else {
        this.form.get('widthOfTheLongGrid').enable();
        this.form.get('widthOfTheCirGrid').enable();
        this.form.get('numberOfInspectionRow').enable();
        if (this.form.get('excelDatas').value !== '') {
          this.form.get('numberOfInspectionRow').setValue(this.form.get('excelDatas').value[0].length);
          this.limitColumn = this.form.get('excelDatas').value[0].length;
        }
      }
    });


    // get data from excel
    this.form.get('excelDatas').valueChanges.subscribe(x => {
      if (x !== null && x !== undefined && x !== '') {
        if (this.form.get('thicknessDataID').value != 1) {
          this.form.get('numberOfInspectionRow').setValue(x.length);
        }
        this.form.get('numberOfInspectionColumn').setValue(x[0].length);
        this.arrayDatas = x;
        this.setColorRange(x);
      }
    });


    this.form.get('numberOfInspectionRow').valueChanges.subscribe(x => {
      if (x !== '') {
        this.limitRow = x;
      }
    });
    this.form.get('numberOfInspectionColumn').valueChanges.subscribe(x => {
      if (x !== '' && this.thicknessId != 1) {
        this.limitColumn = x;
      }
    });

    this.cdRef.detectChanges();
  }


  setColorRange(arrayDatas) {
    const data = arrayDatas.reduce(function (p, c) {
      return p.concat(c);
    });
    const max = _.max(data);
    const min = _.min(data);
    const step = (max - min) / 3;
    this.form.get('min1').setValue(min);
    this.form.get('max1').setValue(min + step);
    this.form.get('max2').setValue(min + (step * 2));
    this.form.get('min2').setValue(min + step);
    this.form.get('max3').setValue(max);
    this.form.get('min3').setValue(min + (step * 2));
  }

  getStyle(value) {
    let backgroundColor: any;
    if (value <= this.form.get('max1').value) {
      backgroundColor = { 'background-color': this.form.get('color1').value };
    } else if (value > this.form.get('min2').value && value <= this.form.get('max2').value) {
      backgroundColor = { 'background-color': this.form.get('color2').value };
    } else if (value > this.form.get('min3').value && value <= this.form.get('max3').value) {
      backgroundColor = { 'background-color': this.form.get('color3').value };
    }
    return backgroundColor;
  }
}
