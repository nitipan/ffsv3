import { IUnit } from './../../common/unit';
import { Http } from '@angular/http';
import { KV } from './../../model/kv';
import { Observable } from 'rxjs/Rx';
import { LoadInputComponent } from './../common/load-input/load-input.component';
import { MaterialInputComponent } from './../common/material-input/material-input.component';
import { InputBase } from './../../model/inputbase';
import { EventService } from './../../event.service';
import { DesignInputComponent } from './../common/design-input/design-input.component';
import { EquipmentInputComponent } from './../common/equipment-input/equipment-input.component';
import { MetalLossInputComponent } from './../common/metal-loss-input/metal-loss-input.component';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';
import { ModuleBase } from './../module-base.component';
import { Component, OnInit, Injectable, Input, QueryList, ContentChildren, AfterContentInit, forwardRef, AfterViewInit, ViewChildren, ViewChild, AfterViewChecked, ChangeDetectorRef } from '@angular/core';
import { FFSInputBase } from '../../common/inputs/ffs-input-base';
import { routerTransition } from "../../common/router.animations";

@Component({
  selector: 'app-general-metal-loss',
  templateUrl: './general-metal-loss.component.html',
  styleUrls: ['./general-metal-loss.component.scss'],
  animations: [routerTransition()],
  host: { '[@routerTransition]': '' }
})
export class GeneralMetalLossComponent extends ModuleBase implements OnInit, AfterViewInit {


  name = 'General Metal Loss';

  @ViewChildren(FFSInputBase) inputs: QueryList<FFSInputBase>;

  @ViewChild(EquipmentInputComponent) equipmentInput: EquipmentInputComponent;
  @ViewChild(DesignInputComponent) designInput: DesignInputComponent;
  @ViewChild(MaterialInputComponent) materialInput: MaterialInputComponent;
  @ViewChild(LoadInputComponent) loadInput: LoadInputComponent;
  @ViewChild(MetalLossInputComponent) metalLossInput: MetalLossInputComponent;

  form: FormGroup;
  thicknessImage: any;
  thicknessDatas: Observable<KV[]>;
  unit: Observable<IUnit>;

  constructor(private http: Http,
    private cdRef: ChangeDetectorRef,
    eventService: EventService) {
    super(eventService);
    this.unit = this.moduleEvent.unit.asObservable();
  }


  ngAfterViewInit(): void {
    this.form = FFSInputBase.toFormGroup(this.inputs);
    this.equipmentInput.form.valueChanges.subscribe(f => {
      let inputs = f as InputBase;
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

      this.http.post(`/api/localmetalloss/calculation/level${equipmentInput.assessmentLevel}/unit${equipmentInput.unitID}`, calculationParam)
        .map(r => r.json())
        .subscribe(r => {
          this.moduleEvent.calculatingSubject.emit(r);
          this.moduleEvent.calculatedSubject.emit({ param: calculationParam, result: r });
        });
    });

    // please see condition in UCDesign.cs line 110 - 180 in C# solution
    // this.form.get('ThicknessDataTypeID').disable();
    //this.thicknessImage = this.form.get('thicknessDataID').value();

    this.designInput.form.get('autoCalculateMinRequireThickness').setValue(true);

    // !!! NEED THIS LINE TO TELL ANGULAR THERE ARE FORM INPUT CHANGE ABOVE
    this.cdRef.detectChanges();
  }

  ngOnInit() {
  }


  initDesignInput() {
    // NO NEED
  }

  initMaterialInput() {

  }

  initFlawInput() {

  }
  initLoadInput() {

  }

}
