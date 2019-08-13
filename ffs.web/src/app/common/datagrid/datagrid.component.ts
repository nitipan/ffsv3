import {
  Component,
  OnInit,
  SimpleChanges,
  ViewChild,
  ViewChildren,
  QueryList,
  ChangeDetectorRef,
  Output,
  EventEmitter
} from '@angular/core';
import { Input } from '@angular/core';
import { KV } from '../../model/kv';
import {
  OnChanges,
  AfterViewInit
} from '@angular/core/src/metadata/lifecycle_hooks';
import * as _ from 'underscore';
import { FFSBrowseComponent } from '../inputs/ffs-input.component';
import { DataGridItemDirective } from './datagrid-item.directive';
@Component({
  selector: 'app-datagrid',
  templateUrl: 'datagrid.component.html',
  styleUrls: ['datagrid.component.scss']
})
export class DataGridComponent implements OnInit, OnChanges, AfterViewInit {
  @Input() header: string;
  @Input() rowModels: KV[];
  @Input() numberColumns: number;

  @Output() onLoaded: EventEmitter<any> = new EventEmitter<any>();
  @ViewChild('browse') browse: FFSBrowseComponent;

  @ViewChildren(DataGridItemDirective) datagridItems: QueryList<
    DataGridItemDirective
  >;

  columns = [];

  private _data = [];

  constructor(private cdRef: ChangeDetectorRef) {}

  ngOnInit() {}

  ngOnChanges(changes: SimpleChanges): void {
    if (changes.numberColumns) {
      this.columns = _.range(0, changes.numberColumns.currentValue, 1);
      if (this.columns.length < this._data.length) {
        this._data = this._data.slice(0, this.columns.length);
        console.log(this._data);
      }
    }
  }

  onDataChange(row, index, value: string) {
    // console.log(row, index, value);
    if (index + 1 > this._data.length) {
      const obj = {};
      this.rowModels.forEach(v => {
        obj[v.key] = null;
      });
      obj[row.key] = Number.parseFloat(value);
      this._data.push(obj);
    } else {
      this._data[index][row.key] = Number.parseFloat(value);
    }
  }

  ngAfterViewInit(): void {
    this.browse.form.get('excelDatas').valueChanges.subscribe(v => {
      if (v) {
        let change0: any = {
          numberColumns: {
            currentValue: 1
          }
        };
        this.ngOnChanges(change0);
        let change: any = {
          numberColumns: {
            currentValue: v[0].length
          }
        };
        this.ngOnChanges(change);

        for (let i = 0; i < this.rowModels.length; i++) {
          for (let c = 0; c < v[0].length; c++) {
            this.onDataChange(this.rowModels[i], c, v[i][c]);
          }
        }

        this.cdRef.detectChanges();
        for (let i = 0; i < this.rowModels.length; i++) {
          for (let c = 0; c < v[0].length; c++) {
            const item = this.datagridItems.find(
              g =>
                g.dataGridItemRow === this.rowModels[i].key &&
                g.dataGridItemColumn === c
            );
            if (item) item.setData(v[i][c]);
          }
        }

        this.onLoaded.emit(this.data);
      }
    });
  }

  get data() {
    return this._data;
  }
}
