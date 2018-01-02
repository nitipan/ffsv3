import { Component, OnInit, SimpleChanges } from '@angular/core';
import { Input } from '@angular/core';
import { KV } from '../../model/kv';
import { OnChanges } from '@angular/core/src/metadata/lifecycle_hooks';
import * as _ from 'underscore';
@Component({
    selector: 'app-datagrid',
    templateUrl: 'datagrid.component.html',
    styleUrls: ['datagrid.component.scss']
})

export class DataGridComponent implements OnInit, OnChanges {

    @Input() header: string;
    @Input() rowModels: KV[];
    @Input() numberColumns: number;

    columns = [];

    private _data = [];

    constructor() { }

    ngOnInit() {

    }

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

    get data() {
        return this._data;
    }
}
