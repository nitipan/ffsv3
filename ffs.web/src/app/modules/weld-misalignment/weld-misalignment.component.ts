import { FFSSelectComponent } from './../../common/inputs/ffs-input.component';
import { InputBase } from './../../model/inputbase';
import { FormGroup } from '@angular/forms';
import { IUnit } from '../../common/unit';
import { Observable } from 'rxjs/Rx';
import { EventService } from '../../event.service';
import { Http } from '@angular/http';
import { KV } from './../../model/kv';
import { ModuleBase } from '../module-base.component';
import { routerTransition } from '../../common/router.animations';
import { animation } from '@angular/animations';
import { AfterViewInit, ChangeDetectorRef, Component, OnInit, ViewChildren, ViewChild, QueryList } from '@angular/core';
import { FFSInputBase } from "../../common/inputs/ffs-input-base";
import { DesignInputComponent } from './../common/design-input/design-input.component';
import { EquipmentInputComponent } from './../common/equipment-input/equipment-input.component';
import { LoadInputComponent } from './../common/load-input/load-input.component';
import { MaterialInputComponent } from './../common/material-input/material-input.component';

@Component({
  selector: 'app-weld-misalignment',
  templateUrl: './weld-misalignment.component.html',
  styleUrls: ['./weld-misalignment.component.scss'],
  animations: [routerTransition()],
  host: { '[@routerTransition]': '' }
})

export class WeldMisalignmentComponent extends ModuleBase implements OnInit, AfterViewInit {
  name = "Weld Misalignment";

  form: FormGroup;
  unit: Observable<IUnit>;
  FabricationTolerance: Observable<KV[]>;
  WeldOrientarion: Observable<KV[]>;
  outOfPattern: boolean = true;

  @ViewChildren(FFSInputBase) inputs: QueryList<FFSInputBase>;

  @ViewChild(EquipmentInputComponent) equipmentInput: EquipmentInputComponent;
  @ViewChild(DesignInputComponent) designInput: DesignInputComponent;
  @ViewChild(MaterialInputComponent) materialInput: MaterialInputComponent;
  @ViewChild(LoadInputComponent) loadInput: LoadInputComponent;

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

    this.moduleEvent.equipmentTypeSubject.subscribe(equipmentType => {
      this.FabricationTolerance = this.http.get(`/api/lookup/fabricationTolerance/${equipmentType}`)
        .map(response => response.json() as any[])
        .map(arr => arr.map(a => { return { key: a.fabricationToleranceID, value: a.fabricationToleranceName }; }));

      this.FabricationTolerance.subscribe((m: KV[]) => {
        this.form.get('FabricationTolerance').setValue(m[0].key);
      });
    })

    this.form.get('FabricationTolerance').valueChanges.subscribe(v => {
      let farbricId = parseInt(v);
      if (farbricId === 2 || farbricId === 3 || farbricId === 5 || farbricId === 8) {
        this.outOfPattern = false;
      } else {
        this.outOfPattern = true;
      }
    });

    this.designInput.form.get("autoCalculateMinRequireThickness").setValue(true);
    this.designInput.form.get('autoCalculateMinRequireThickness').disable();
    this.designInput.form.get('minRequireLongitutinalThickness').disable();
    this.designInput.form.get('minRequireCircumferentialThickness').disable();
    this.moduleEvent.assessmentLevelSubject.subscribe(assessmentLevel => {
      if (assessmentLevel == 1) {
        this.designInput.form.get('designPressure').enable();
        this.designInput.form.get('designTemperature').enable();
        this.loadInput.form.get('operatingPressure').enable();

        this.materialInput.form.get('materialTypeID').enable();
        //this.materialInput.form.get('material').enable();
        this.materialInput.form.get('asmeExemptionCurvesID').enable();
        this.materialInput.form.get('automaticallyCalculationAllowableStress').enable();
        this.materialInput.form.get('allowableStress').enable();
        this.materialInput.form.get('ultimatedTensileStrength').enable();
        this.materialInput.form.get('youngModulus').enable();
        this.materialInput.form.get('yieldStrength').enable();
        this.materialInput.form.get('poissonRatio').enable();

        this.loadInput.form.get('automaticallyCalculationTheNominalStressOfTheComponent').enable();
        this.loadInput.form.get('operatingPressure').enable();
        this.loadInput.form.get('operatingTemperature').enable();

      } else {
        this.designInput.form.get('designPressure').disable();
        this.designInput.form.get('designTemperature').disable();
        this.loadInput.form.get('operatingPressure').disable();

        this.materialInput.form.get('materialTypeID').disable();
        //this.materialInput.form.get('material').disable();
        this.materialInput.form.get('asmeExemptionCurvesID').disable();
        this.materialInput.form.get('automaticallyCalculationAllowableStress').disable();
        this.materialInput.form.get('allowableStress').disable();
        this.materialInput.form.get('ultimatedTensileStrength').disable();
        this.materialInput.form.get('youngModulus').disable();
        this.materialInput.form.get('yieldStrength').disable();
        this.materialInput.form.get('poissonRatio').disable();

        this.loadInput.form.get('automaticallyCalculationTheNominalStressOfTheComponent').disable();
        this.loadInput.form.get('operatingPressure').disable();
        this.loadInput.form.get('operatingTemperature').disable();

      }
    });

    // calculate
    this.moduleEvent.requestCalculateSubject.subscribe(() => {


      // TODO check form valid ?

      // NEED GET RAWDATA because to include disabled value
      let equipmentInput = this.equipmentInput.form.getRawValue() as InputBase;
      let designInput = this.designInput.form.getRawValue() as InputBase;
      let materialInput = this.materialInput.form.getRawValue() as InputBase;
      let loadInput = this.loadInput.form.getRawValue() as InputBase;
      let flawInput = this.form.getRawValue();

      // merge
      let calculationParam = {
        ...equipmentInput,
        ...designInput,
        ...materialInput,
        ...flawInput,
        ...loadInput
      }


      this.moduleEvent.calculatingSubject.emit(null);

      this.http.post(`/api/weld/calculation/level${equipmentInput.assessmentLevel}/unit${equipmentInput.unitID}`, calculationParam)
        .map(r => r.json())
        .subscribe(r => {
          this.moduleEvent.calculatingSubject.emit(r);
          this.moduleEvent.calculatedSubject.emit({ param: calculationParam, result: r, module: this });
        });
    });


    this.cdRef.detectChanges();
    this.moduleEvent.equipmentTypeSubject.emit(1);
  }

  ngOnInit() {
    this.WeldOrientarion = this.http.get('/api/lookup/weldorientarion/')
      .map(response => response.json() as any[])
      .map(arr => arr.map(a => { return { key: a.weldOrientarionID, value: a.weldOrientarionName }; }));

    // this.WeldOrientarion.subscribe((m: KV[]) => {
    //   this.form.get('WeldOrientarion').setValue(m[0].key);
    // });
  }

  optionReady(select: FFSSelectComponent) {
    if (select.options.length > 0)
      select.setValue(select.options[0].key);
    else
      select.setValue("");
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
