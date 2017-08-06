import { EventService } from './../../../event.service';
import { Component, OnInit, ChangeDetectorRef, AfterViewInit } from '@angular/core';
import { InputBaseComponent } from "../input-base.component";

import pdfMake from 'pdfmake/build/pdfmake';
import pdfFonts from 'pdfmake/build/vfs_fonts';

pdfMake.vfs = pdfFonts.pdfMake.vfs;

import { ModuleBase } from "../../module-base.component";
import { toDataURL } from "../../../common/functions";
import { DatePipe } from "@angular/common";
@Component({
  selector: 'app-result',
  templateUrl: './result.component.html',
  styleUrls: ['./result.component.scss']
})
export class ResultComponent extends InputBaseComponent implements OnInit, AfterViewInit {

  result: any;

  level2Grid: any[];

  module: ModuleBase;

  logo: any;

  reportFactory: any;

  constructor(private datePipe: DatePipe) {


    super();

    toDataURL("/assets/logo_round.png").subscribe(v => {
      this.logo = v;
    });
  }

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

    let equipmentImage: any = {};

    if (this.result.param.equipmentImage != '') {
      equipmentImage.image = this.result.param.equipmentImage;
      equipmentImage.fit = [400, 400];
      equipmentImage.margin = [20, 20, 0, 0];

    }

    var contents = [
      {
        layout: 'noBorders',
        table: {
          widths: [50, '*'],
          body: [
            [{ width: 50, image: this.logo }, { text: this.module.name + ' Report', style: 'h1', margin: [10, 10, 0, 0] }],
          ]
        }
      },
      { text: 'Level ' + this.result.param.assessmentLevel + ' Assessment', style: 'h2' },

      {
        style: 't1',
        margin: [20, 20, 0, 0],
        layout: 'noBorders',
        table: {

          widths: [150, '*'],
          body: [
            [{ text: 'Equipment number', bold: true }, this.result.param.equipmentNumber],
            [{ text: 'Equipment type', bold: true }, this.result.param.equipmentTypeText],
            [{ text: 'Component type', bold: true }, this.result.param.componentTypeText],
            [{ text: 'Design Code', bold: true }, this.result.param.designCodeText],
            [{ text: 'Failure Mode', bold: true }, this.module.name],
            [{ text: 'Methodology', bold: true }, this.result.param.methodologyText],

            [{ text: 'Analysis By', bold: true }, this.result.param.analysisBy],
            [{ text: 'Analysis Date', bold: true }, this.datePipe.transform(this.result.param.analysisDate, "MM/dd/yyyy")],
            [{ text: 'Analysis Detail', bold: true }, ''],
          ]
        }
      },
      equipmentImage
    ];

    if (this.reportFactory != undefined) {
      contents.push({ text: '', pageBreak: 'after' });
      contents.push(...this.reportFactory(this.result));
    }

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
      "content": contents,
      styles: {
        h1: {
          fontSize: 22,
          bold: true
        },
        h2: {
          fontSize: 20,
          bold: true,
          margin: [0, 10, 0, 0]
        }, h3: {
          fontSize: 14,
          bold: true,
          margin: [20, 10, 0, 0]
        },
        h4: {
          fontSize: 14,
          bold: true,
          margin: [30, 10, 0, 0]
        },
        h5: {
          fontSize: 14,
          bold: true,
          margin: [40, 10, 0, 0]
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
          layout: 'noBorders',
          table: {
            widths: [50, '*'],
            body: [
              [{ width: 50, image: this.logo }, { text: this.module.name + ' Summary', style: 'h1', margin: [10, 10, 0, 0] }],
            ]
          }
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
