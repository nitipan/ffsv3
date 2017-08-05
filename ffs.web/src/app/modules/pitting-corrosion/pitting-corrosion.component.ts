import { InputBase } from './../../model/inputbase';
import { IUnit } from '../../common/unit';
import { Observable } from 'rxjs/Rx';
import { FormGroup } from '@angular/forms';
import { EventService } from '../../event.service';
import { Http } from '@angular/http';
import { KV } from './../../model/kv';
import { routerTransition } from '../../common/router.animations';
import { ModuleBase } from '../module-base.component';
import { AfterViewInit, ChangeDetectorRef, Component, OnInit, ViewChildren, ViewChild, QueryList } from '@angular/core';
import { FFSInputBase } from "../../common/inputs/ffs-input-base";
import { DesignInputComponent } from './../common/design-input/design-input.component';
import { EquipmentInputComponent } from './../common/equipment-input/equipment-input.component';
import { LoadInputComponent } from './../common/load-input/load-input.component';
import { MaterialInputComponent } from './../common/material-input/material-input.component';

@Component({
  selector: 'app-pitting-corrosion',
  templateUrl: './pitting-corrosion.component.html',
  styleUrls: ['./pitting-corrosion.component.scss'],
  animations: [routerTransition()],
  host: { '[@routerTransition]': '' }
})

export class PittingCorrosionComponent extends ModuleBase implements OnInit, AfterViewInit {

  name = "Pitting Corrosion";
  @ViewChildren(FFSInputBase) inputs: QueryList<FFSInputBase>;

  @ViewChild(EquipmentInputComponent) equipmentInput: EquipmentInputComponent;
  @ViewChild(DesignInputComponent) designInput: DesignInputComponent;
  @ViewChild(MaterialInputComponent) materialInput: MaterialInputComponent;
  @ViewChild(LoadInputComponent) loadInput: LoadInputComponent;

  form: FormGroup;
  unit: Observable<IUnit>;
  theStandardPitCharts: Observable<KV[]>;
  imgPath: String;
  assessmentLevel: any = 1;

  constructor(private http: Http, private cdRef: ChangeDetectorRef, eventService: EventService) {
    super(eventService);
    this.unit = this.moduleEvent.unit.asObservable();
  }

  ngAfterViewInit(): void {
    this.form = FFSInputBase.toFormGroup(this.inputs);
    this.equipmentInput.form.valueChanges.subscribe(f => {
      let inputs = f as InputBase;
      this.valueChangedSubject.next(inputs);
    });
    this.equipmentInput.form.get('assessmentLevel').valueChanges.subscribe((assessmentLevel) => {
      this.assessmentLevel = assessmentLevel;
      if (this.assessmentLevel == 1) {
        this.form.get('theMaximumPitDepth').enable();
        this.form.get('theStandardPitChart').enable();
        this.form.get('browseBtn').disable();
        this.form.get('dataRow').disable();
      } else if (this.assessmentLevel == 2) {
        this.form.get('theMaximumPitDepth').disable();
        this.form.get('theStandardPitChart').disable();
        this.form.get('browseBtn').enable();
        this.form.get('dataRow').enable();
      }
    });

    this.form.get('theStandardPitChart').valueChanges.subscribe(x => {
      this.imgPath = 'assets/chart' + x + '.bmp';
    });

    this.form.get('autoAllowableRSF').setValue(true);
    this.form.get('allowRSF').disable();
    this.designInput.form.get('autoCalculateMinRequireThickness').setValue(true);
    this.cdRef.detectChanges();

  }
  ngOnInit() {
    this.theStandardPitCharts = this.http.get("/api/lookup/standardpitcharts")
      .map(response => response.json() as any[])
      .map(arr => arr.map(a => { return { key: a.theStandardPitChartID, value: a.theStandardPitChartName }; }));
    this.theStandardPitCharts.subscribe((m: KV[]) => {
      this.form.get('theStandardPitChart').setValue(m[0].key);
    });
  }

  initDesignInput() {

  }

  initMaterialInput() {

  }

  initFlawInput() {

  }

  initLoadInput() {

  }

}
