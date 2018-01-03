import { DatePipe } from '@angular/common';
import { AfterViewInit, Component, OnInit } from '@angular/core';
import { toDataURL } from '../../../common/functions';
import { ModuleBase } from '../../module-base.component';
import { InputBaseComponent } from '../input-base.component';
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
  summaryFactory: any;

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
        tableHeader: {
          bold: true
        }
      }
    }

    pdfMake.createPdf(docDefinition).open();
  }

  summary() {
    const equipmentImage: any = {};
    if (this.result.param.equipmentImage !== '') {
      equipmentImage.image = this.result.param.equipmentImage;
      equipmentImage.fit = [200, 200];
      equipmentImage.margin = [50, 10, 0, 0];
      equipmentImage.colSpan = 2;
    } else {
      equipmentImage.colSpan = 2;
      equipmentImage.text = '';
    }

    let equipment = [
      [
        { text: '1. Equipment & Component', style: 'h2' }, ''
      ],
      [
        { text: '1.1 Overview', style: 'subheader' }, ''
      ],
      [
        { text: '- Equipment Number', style: 'p' }, { style: 'p', text: this.result.param.equipmentNumber }
      ],
      [
        { text: '- Equipment Type', style: 'p' }, { style: 'p', text: this.result.param.equipmentTypeText }
      ],
      [
        { text: '- Component Type', style: 'p' }, { style: 'p', text: this.result.param.componentTypeText }
      ],
      [
        { text: '- Material of construction', style: 'p' }, { style: 'p', text: this.result.param.materialText }
      ],
      [
        { text: '- Design Code', style: 'p' }, { style: 'p', text: this.result.param.designCodeText }
      ],
      [
        equipmentImage
      ],
      [
        { text: '1.2 Design data', style: 'subheader' }, ''
      ],
      [
        { text: '- Pressure', style: 'p' }, { style: 'p', text: this.result.param.designPressure + ' ' + this.result.module.currentUnit.pressure }
      ],
      [
        { text: '- Temperature', style: 'p' }, { style: 'p', text: this.result.param.designTemperature + ' ' + this.result.module.currentUnit.temperature }
      ],
      [
        { text: '1.3 Operating data', style: 'subheader' }, ''
      ],
      [
        { text: '- Max. pressure', style: 'p' }, { style: 'p', text: this.result.param.operatingPressure + ' ' + this.result.module.currentUnit.pressure }
      ],
      [
        { text: '- Temperature', style: 'p' }, { style: 'p', text: this.result.param.operatingTemperature + ' ' + this.result.module.currentUnit.temperature }
      ],
      [
        { text: '- Critical exposure temp., CET ', style: 'p' },
        { style: 'p', text: this.result.param.TheCriticalExposureTemperature + ' ' + this.result.module.currentUnit.temperature }
      ]
    ];


    if (this.result.param.assessmentLevel === 2) {
      equipment = equipment.filter(function (x: any) {
        return x[0].text !== '- Pressure' && x[0].text !== '- Max. pressure';
      });
    }

    const contents: any[] = [
      {
        layout: 'noBorders',
        table: {
          widths: [50, '*'],
          body: [
            [{ width: 50, image: this.logo }, { text: this.module.name + ' Assessment Reports', style: 'h1', margin: [10, 10, 0, 0] }],
          ]
        }
      },
      {
        layout: 'noBorders',
        table: {
          body: equipment
        }
      },
      { text: '', pageBreak: 'after' },
      {
        layout: 'noBorders',
        table: {
          widths: [50, '*'],
          body: [
            [{ width: 50, image: this.logo }, { text: this.module.name + ' Assessment Reports', style: 'h1', margin: [10, 10, 0, 0] }],
          ]
        }
      }
    ];

    if (this.summaryFactory != undefined) {
      contents.push(...this.summaryFactory(this.result));
    }

    const docDefinition = {
      'pageSize': 'A4',
      'pageMargins': [
        20,
        20,
        20,
        20
      ],
      watermark: { text: 'Fitness for service software v3', opacity: 0.2 },
      'content': contents,
      styles: {
        h1: {
          fontSize: 22,
          bold: true
        },
        h2: {
          fontSize: 16,
          margin: [0, 10, 0, 0]
        },
        p: {
          fontSize: 10,
          margin: [20, 5, 0, 0]
        },
        subheader: {
          margin: [0, 10, 0, 0]
        },
        img: {
          margin: [50, 10, 0, 0]
        }
      },
      footer: function (currentPage, pageCount) { return currentPage.toString() + ' of ' + pageCount; },
    };

    pdfMake.createPdf(docDefinition).open();
  }
}
