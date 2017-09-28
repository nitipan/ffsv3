import { DatePipe } from '@angular/common';
import { AfterViewInit, ChangeDetectorRef, Component, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Rx';

import { FFSInputBase } from '../../common/inputs/ffs-input-base';
import { routerTransition } from '../../common/router.animations';
import { ModuleBase } from '../module-base.component';
import { IUnit } from './../../common/unit';
import { EventService } from './../../event.service';
import { InputBase } from './../../model/inputbase';
import { DesignInputComponent } from './../common/design-input/design-input.component';
import { EquipmentInputComponent } from './../common/equipment-input/equipment-input.component';
import { LoadInputComponent } from './../common/load-input/load-input.component';
import { MaterialInputComponent } from './../common/material-input/material-input.component';
import { ResultComponent } from './../common/result/result.component';



@Component({
  selector: 'app-hydrogen',
  templateUrl: './hydrogen.component.html',
  styleUrls: ['./hydrogen.component.scss'],
  animations: [routerTransition()],
  host: { '[@routerTransition]': '' }
})

export class HydrogenComponent extends ModuleBase implements OnInit, AfterViewInit {
  name = 'Hydrogen Blister';

  form: FormGroup;
  unit: Observable<IUnit>;

  @ViewChildren(FFSInputBase) inputs: QueryList<FFSInputBase>;

  @ViewChild(EquipmentInputComponent) equipmentInput: EquipmentInputComponent;
  @ViewChild(DesignInputComponent) designInput: DesignInputComponent;
  @ViewChild(MaterialInputComponent) materialInput: MaterialInputComponent;
  @ViewChild(LoadInputComponent) loadInput: LoadInputComponent;
  @ViewChild(ResultComponent) result: ResultComponent;

  constructor(private http: Http, private cdRef: ChangeDetectorRef, eventService: EventService, private datePipe: DatePipe) {
    super(eventService);
    this.unit = this.moduleEvent.unit.asObservable();
  }

  ngAfterViewInit(): void {
    this.form = FFSInputBase.toFormGroup(this.inputs);

    this.result.reportFactory = this.reportFactory;
    this.result.summaryFactory = this.summaryFactory;

    this.equipmentInput.form.valueChanges.subscribe(f => {
      const inputs = f as InputBase;
      this.valueChangedSubject.next(inputs);
    });

    // calculate
    this.moduleEvent.requestCalculateSubject.subscribe(() => {


      // TODO check form valid ?

      // NEED GET RAWDATA because to include disabled value
      const equipmentInput = this.equipmentInput.form.getRawValue() as InputBase;
      const designInput = this.designInput.form.getRawValue() as InputBase;
      const materialInput = this.materialInput.form.getRawValue() as InputBase;
      const loadInput = this.loadInput.form.getRawValue() as InputBase;
      const flawInput = this.form.getRawValue();

      // merge
      const calculationParam = {
        ...equipmentInput,
        ...designInput,
        ...materialInput,
        ...flawInput,
        ...loadInput
      };


      this.moduleEvent.calculatingSubject.emit(null);

      this.http.post(`/api/hydrogen/calculation/level${equipmentInput.assessmentLevel}/unit${equipmentInput.unitID}`, calculationParam)
        .map(r => r.json())
        .subscribe(r => {
          this.moduleEvent.calculatingSubject.emit(r);
          this.moduleEvent.calculatedSubject.emit({ param: calculationParam, result: r, module: this });
        });
    });



    this.cdRef.detectChanges();
  }

  ngOnInit() {
  }

  initDesignInput() {

  }
  initMaterialInput() {

  }
  initFlawInput() {

  }
  initLoadInput() {

  }

  reportFactory(result) {
    const module = result.module;

    const content: any[] = [
      {
        'text': 'STEP 1 – Determine the starting point for the MAT',
        'style': 'h3'
      },
      {
        'text': 'STEP 1.1 – Determine the following parameters',
        'style': 'h4'
      },
      {
        'text': 'A. Nominal uncorroded thickness at each weld joint',
        'style': 'h5'
      },
      {
        layout: 'noBorders',
        margin: [50, 15, 0, 10],
        table: {
          'widths': [
            200,
            '*'
          ],
          body: [['t.nom', `${result.param.TheUncorrodedGoverningThickness} ${module.currentUnit.distance}`]]
        }
      },
      {
        'text': 'B. Material of construction',
        'style': 'h5'
      },
      {
        layout: 'noBorders',
        margin: [50, 15, 0, 10],
        table: {
          'widths': [
            200,
            '*'
          ],
          body: [['Material', result.param.materialText]]
        }
      }, {
        'text': 'STEP 1.2 – Determine the uncorroded governing thickness',
        'style': 'h4'
      }, {
        layout: 'noBorders',
        margin: [50, 15, 0, 10],
        table: {
          'widths': [
            200,
            '*'
          ],
          body: [['t,g', `${result.param.TheUncorrodedGoverningThickness}`]]
        }
      }, {
        'text': 'STEP 1.3 – Determine the applicable materialtoughness curve',
        'style': 'h4'
      }, {
        layout: 'noBorders',
        margin: [50, 15, 0, 10],
        table: {
          'widths': [
            200,
            '*'
          ],
          body: [['MAT,LV' + result.param.assessmentLevel, `${module.currentUnit.temperature}`],
          ['CET', `${module.currentUnit.temperature}`]]
        }
      }

    ];


    let r: any = {};

    if (result.param.assessmentLevel == 1) {
      r = {
        text: result.result.result, style: 'h3'
      };

      content.push(r);
    } else {
      const level2Grid = [[{ text: 'Pratio*Pdesign', style: 'tableHeader' },
      { text: 'TR', style: 'tableHeader' },
      { text: 'MATreduce', style: 'tableHeader' },
      { text: 'Result', style: 'tableHeader' }]];
      for (let i = 0; i < this.result.result.resultDataGrid.length; i += 4) {
        level2Grid.push([
          this.result.result.resultDataGrid[i],
          this.result.result.resultDataGrid[i + 1],
          this.result.result.resultDataGrid[i + 2],
          this.result.result.resultDataGrid[i + 3]
        ]);
      }

      r = {
        margin: [30, 30, 0, 0],
        table: {
          headerRows: 1,
          body: level2Grid
        },
        layout: 'lightHorizontalLines'
      };
      content.push({
        text: 'Result', pageBreak: 'before', style: 'h2'
      }
      );
      content.push(r);
    }


    return content;
  }

  summaryFactory(result) {
    let PWHTtext = 'No';
    let summary = '';
    if (result.resultBool) {
      summary = 'The component is safe from brittle fracture.';
    } else {
      summary = 'The component is unsafe from brittle fracture.';
    }

    if (result.param.PWHT) {
      PWHTtext = 'Yes';
    }

    let assesment: any[][] = [
      [
        { text: '2. Assessments', style: 'h2' }, ''
      ],
      [
        { text: '2.1 Overview', style: 'subheader' }, ''
      ],
      [
        { text: '- Methodlogy', style: 'p' }, { style: 'p', text: result.param.methodologyText }
      ],
      [
        { text: '- Level', style: 'p' }, { style: 'p', text: result.param.assessmentLevel }
      ],
      [
        { text: '- Assessor\' name', style: 'p' }, { style: 'p', text: result.param.analysisBy }
      ],
      [
        { text: '- Date', style: 'p' }, { style: 'p', text: this.datePipe.transform(result.param.analysisDate, 'MM/dd/yyyy') }
      ],
      [
        { text: '2.2 Required data', style: 'subheader' }, ''
      ],
      [
        { text: '- Nominal wall thickness of component, tn ', style: 'p' },
        { style: 'p', text: result.param.nominalThickness + ' ' + result.module.currentUnit.distance }
      ],
      [
        { text: '- Uncorroded governing thickness, tg ', style: 'p' },
        { style: 'p', text: result.param.TheUncorrodedGoverningThickness + ' ' + result.module.currentUnit.distance }
      ],
      [
        { text: '- Weld joint eff., E ', style: 'p' }, { style: 'p', text: result.param.weldJointEfficiency }
      ],
      [
        { text: '- Uniform metal loss, LOSS', style: 'p' },
        { style: 'p', text: result.param.loss + ' ' + result.module.currentUnit.distance }
      ],
      [
        { text: '- Future corrosion allowance, FCA', style: 'p' },
        { style: 'p', text: result.param.fca + ' ' + result.module.currentUnit.distance }
      ],
      [
        { text: '- PWHT done at initial construction and after all repairs?', style: 'p' }, { style: 'p', text: PWHTtext }
      ],
      [
        { text: '2.3 Calculation Result', style: 'subheader' }, ''
      ],
      [
        { text: '- Allowable stress', style: 'p' }, { style: 'p', text: result.param.allowableStress }
      ],
      [
        { text: '- Min. required thickness, tmin', style: 'p' },
        { style: 'p', text: result.param.minRequireLongitutinalThickness }
      ],
      [
        { text: '- Applicable ASME exemption curve', style: 'p' }, { style: 'p', text: 'ASME Exemption Curves B' }
      ],
      [
        { text: '- Min. allowable temp., MAT', style: 'p' },
        { style: 'p', text: result.param.TheCriticalExposureTemperature + ' ' + result.module.currentUnit.temperature }
      ],
      [
        { text: '2.4 Summary', style: 'subheader' }, { style: 'subheader', text: summary }
      ]
    ];


    if (result.param.assessmentLevel === 2) {
      assesment = assesment.filter(function (y: any) {
        return y[0].text !== '- Allowable stress' && y[0].text !== '- Min. required thickness, tmin';
      });
    }

    const content = [{
      layout: 'noBorders',
      table: {
        body: assesment
      }
    }];
    return content;
  }

}
