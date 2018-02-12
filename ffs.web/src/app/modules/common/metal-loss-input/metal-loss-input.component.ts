import { FFSSelectComponent, FFSBrowseComponent } from './../../../common/inputs/ffs-input.component';
import { IUnit } from './../../../common/unit';
import { Observable } from 'rxjs/Rx';
import { EventService } from './../../../event.service';
import { FormGroup } from '@angular/forms';
import { InputBase } from './../../../model/inputbase';
import { Http } from '@angular/http';
import { KV } from './../../../model/kv';
import { Component, OnInit, ViewChildren, QueryList, AfterViewInit, ChangeDetectorRef, Input } from '@angular/core';
import { FFSInputBase } from '../../../common/inputs/ffs-input-base';
import { InputBaseComponent } from '../input-base.component';
import { ColorRange } from './../../../model/colorRange';
import * as _ from 'underscore';
import { ViewChild } from '@angular/core';
import { DataGridComponent } from '../../../common/datagrid/datagrid.component';
import { ChartComponent } from '../../../common/chart/chart.component';

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
  arrayDatas: any;
  limitRow: number;
  limitColumn: number;
  columnChartData: any;
  rowChartData: any;

  rowModels: KV[];

  @Input() isLocal = false;

  constructor(private http: Http, private cdRef: ChangeDetectorRef) {
    super();
  }

  @ViewChild('chart') chart: ChartComponent;
  @ViewChild('chartLong') chartLong: ChartComponent;
  @ViewChild('chartCir') chartCir: ChartComponent;
  @ViewChild('browse') browse: FFSBrowseComponent;

  @ViewChildren(FFSInputBase) private inputs: QueryList<FFSInputBase>;

  ngOnInit() {
    this.unit = this.moduleEvent.unit.asObservable();
    this.thicknessDatas = this.http.get('/api/lookup/thicknessdatas')
      .map(response => response.json() as any[])
      .map(arr => arr.map(a => { return { key: a.thicknessDataID, value: a.thicknessDataName }; }));
  }

  ngAfterViewInit(): void {
    this.form = FFSInputBase.toFormGroup(this.inputs);
    if (this.isLocal)
      this.form.get('thicknessDataID').disable();
    this.InitColor();

    // common behaviour
    this.form.get('autoAllowableRSF').setValue(true);
    this.form.get('allowRSF').disable();

    this.form.get('thicknessDataID').valueChanges.subscribe(x => {
      this.thicknessImage = x;
    });

    this.form.get('thicknessDataID').valueChanges.subscribe(x => {
      this.form.reset({}, { emitEvent: false });
      this.browse.removeFile();
      this.InitColor();
      this.arrayDatas = undefined;
      if (this.chart)
        this.chart.clear();

      if (this.chartCir)
        this.chartCir.clear();

      if (this.chartLong)
        this.chartLong.clear();

      this.form.patchValue({ thicknessDataID: x }, { emitEvent: false });

      if (+x === 1) {
        this.form.get('widthOfTheLongGrid').disable();
        this.form.get('widthOfTheCirGrid').disable();
        this.form.get('numberOfInspectionRow').disable();
        this.form.get('numberOfInspectionRow').setValue(2);
        this.limitColumn = 1;
      } else {
        this.form.get('widthOfTheLongGrid').enable();
        this.form.get('widthOfTheCirGrid').enable();
        this.form.get('numberOfInspectionRow').enable();
      }
    });


    // get data from excel
    this.form.get('excelDatas').valueChanges.subscribe(x => {

      if (x !== null && x !== undefined && x !== '') {

        if (this.value.thicknessDataID === 1) {
          this.form.patchValue({ numberOfInspectionColumn: x.length });
        } else {
          this.form.patchValue({ numberOfInspectionColumn: x.length });
          this.form.patchValue({ numberOfInspectionRow: x[0].length });
        }

        this.arrayDatas = x;
        if (this.chart)
          this.chart.clear();

        if (this.chartCir)
          this.chartCir.clear();

        if (this.chartLong)
          this.chartLong.clear();


        let data = [];
        if (+this.value.thicknessDataID == 1) {
          data = x.map(m => m[1]);
          this.chart.setChartData({
            labels: x.map(m => m[0]),
            datasets: [
              {
                label: 'Thickness Inspection',
                data: data,
                borderWidth: 2,
                borderColor: '#ee2524',
                backgroundColor: '#ee2524',
                fill: false
              }
            ]
          });

        } else {

          data = x.reduce(function (p, c) {
            return p.concat(c);
          });

          let cir = _.zip.apply(_, x)
            .map(r => {
              return _.min(r);
            });

          let long = x.map(r => {
            return _.min(r);
          });

          let i = 0;

          this.chartLong.setChartData({
            labels: long.map(m => {
              i++;
              return `C${i}`;
            }),
            datasets: [
              {
                label: 'Longitudinal CTP',
                data: long,
                borderWidth: 2,
                borderColor: '#ee2524',
                backgroundColor: '#ee2524',
                fill: false
              }
            ]
          });

          i = 0;
          this.chartCir.setChartData({
            labels: cir.map(m => {
              i++;
              return `M${i}`;
            }),
            datasets: [
              {
                label: 'Longitudinal CTP',
                data: cir,
                borderWidth: 2,
                borderColor: '#ee2524',
                backgroundColor: '#ee2524',
                fill: false
              }
            ]
          });
        }
        this.setColorRange(data);
      }
    });


    this.form.get('numberOfInspectionRow').valueChanges.subscribe(x => {
      if (x !== '') {
        this.limitRow = x;
      }
    });
    this.form.get('numberOfInspectionColumn').valueChanges.subscribe(x => {
      if (x !== '' && this.value.thicknessDataID != 1) {
        this.limitColumn = x;
      }
    });

    this.cdRef.detectChanges();
  }


  private InitColor() {
    this.form.get('color1').setValue('#ed0404');
    this.form.get('color2').setValue('#f1f700');
    this.form.get('color3').setValue('#24db15');
  }

  setColorRange(data) {
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


  get value() {
    return this.form.getRawValue();
  }
}
