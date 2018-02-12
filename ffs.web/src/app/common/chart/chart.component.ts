import { Component, OnInit } from '@angular/core';
import { ViewChild } from '@angular/core';
import { ElementRef } from '@angular/core';

@Component({
    selector: 'app-chart',
    templateUrl: './chart.component.html',

})
export class ChartComponent implements OnInit {
    @ViewChild('chart') chart: ElementRef;

    chartObj: any;

    constructor() { }

    ngOnInit() {
        let ctx = this.chart.nativeElement.getContext('2d');
        this.chartObj = new Chart(ctx, {
            type: 'line',
            data: {}
        });
    }

    setChartData(data) {
        this.chartObj.data = data;
        this.chartObj.update();
    }

    clear() {
        this.chartObj.clear();
    }
}
