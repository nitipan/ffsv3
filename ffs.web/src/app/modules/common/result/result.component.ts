import { EventService } from './../../../event.service';
import { Component, OnInit, ChangeDetectorRef, AfterViewInit } from '@angular/core';
import { InputBaseComponent } from "../input-base.component";

import pdfMake from 'pdfmake/build/pdfmake';
import pdfFonts from 'pdfmake/build/vfs_fonts';

pdfMake.vfs = pdfFonts.pdfMake.vfs;

import { ModuleBase } from "../../module-base.component";
@Component({
  selector: 'app-result',
  templateUrl: './result.component.html',
  styleUrls: ['./result.component.scss']
})
export class ResultComponent extends InputBaseComponent implements OnInit, AfterViewInit {


  result: any;

  level2Grid: any[];

  module: ModuleBase;

  ngOnInit() {
    this.moduleEvent.calculatedSubject.subscribe(r => {
      this.result = r;
      console.log(this.result);

      this.module = r.module;

      if (this.result.param.assessmentLevel == 2) {
        this.level2Grid = [];
        for (let i = 0; i < this.result.result.resultDataGrid.length; i += 4) {
          this.level2Grid.push({
            col1: this.result.result.resultDataGrid[i],
            col2: this.result.result.resultDataGrid[i + 1],
            col3: this.result.result.resultDataGrid[i + 2],
            col4: this.result.result.resultDataGrid[i + 3],
            result: (this.result.result.resultDataGrid[i + 3].toLowerCase().indexOf("not") == -1)
          });
        }
        console.log(this.level2Grid);
      }

    });
  }

  ngAfterViewInit(): void {


  }

  report() {


    var docDefinition = {
      "pageSize": "A4",
      "pageOrientation": "portrait",
      "pageMargins": [
        20,
        20,
        20,
        20
      ],
      watermark: { text: 'Fitness for service software v3', opacity: 0.2 },
      "content": [

        { text: this.module.name + ' Report', style: 'h1' },
        { text: 'Level ' + this.result.param.assessmentLevel + ' Assessment', style: 'h2' },

        //   {
        //   style : 't1',
        //   margin : [20,20,0,0],
        //   layout: 'noBorders',
        //   table: {

        //     widths: [ 150, '*' ],
        //     body: [
        //       [ {text : 'Equipment number' , bold:true}, '1234'  ],
        //       [ {text : 'Equipment type', bold:true}, 'Pipe Components'  ],
        //       [ {text : 'Component type', bold:true}, 'Straight Pipe'  ],
        //       [ {text : 'Design Code', bold:true}, 'ASME 012'  ],
        //       [ {text : 'Failure Mode', bold:true}, 'Brittle Fracture'  ],
        //       [ {text : 'Methodology', bold:true}, 'API 579-1/ASME FFS-1 2007 Fitness-For-Service'  ],

        //       [ {text : 'Analysis By', bold:true}, 'Nitipan Pompan'  ],
        //       [ {text : 'Analysis Date', bold:true}, '27/7/2017'  ],
        //       [ {text : 'Analysis Detail', bold:true}, ''  ],
        //     ]
        //   }
        // }

      ],
      styles: {
        h1: {
          fontSize: 22,
          bold: true
        },
        h2: {
          fontSize: 20,
          bold: true,
          margin: [0, 10, 0, 0]
        },
        t1: {
          fontSize: 14,
        },
      }
    }

    pdfMake.createPdf(docDefinition).open();
  }

  summary() {

    var docDefinition = {
      "pageSize": "A4",
      "pageOrientation": "portrait",
      "pageMargins": [
        20,
        20,
        20,
        20
      ],
      watermark: { text: 'Fitness for service software v3', opacity: 0.2 },
      "content": [

        {
          text: this.module.name + ' Summary', style: 'h1'
        },
        { text: 'Level ' + this.result.param.assessmentLevel + ' Assessment', style: 'h2' },

        //   {
        //   style : 't1',
        //   margin : [20,20,0,0],
        //   layout: 'noBorders',
        //   table: {

        //     widths: [ 150, '*' ],
        //     body: [
        //       [ {text : 'Equipment number' , bold:true}, '1234'  ],
        //       [ {text : 'Equipment type', bold:true}, 'Pipe Components'  ],
        //       [ {text : 'Component type', bold:true}, 'Straight Pipe'  ],
        //       [ {text : 'Design Code', bold:true}, 'ASME 012'  ],
        //       [ {text : 'Failure Mode', bold:true}, 'Brittle Fracture'  ],
        //       [ {text : 'Methodology', bold:true}, 'API 579-1/ASME FFS-1 2007 Fitness-For-Service'  ],

        //       [ {text : 'Analysis By', bold:true}, 'Nitipan Pompan'  ],
        //       [ {text : 'Analysis Date', bold:true}, '27/7/2017'  ],
        //       [ {text : 'Analysis Detail', bold:true}, ''  ],
        //     ]
        //   }
        // }

      ],
      styles: {
        h1: {
          fontSize: 22,
          bold: true
        },
        h2: {
          fontSize: 20,
          bold: true,
          margin: [0, 10, 0, 0]
        },
        t1: {
          fontSize: 14,
        },
      }
    }

    pdfMake.createPdf(docDefinition).open();
  }
}
