import { Http } from '@angular/http';
import { InputBase } from './../../model/inputbase';
import { EventService } from './../../event.service';
import { IUnit } from './../../common/unit';
import { Observable } from 'rxjs/Rx';
import { FormGroup } from '@angular/forms';
import { LoadInputComponent } from './../common/load-input/load-input.component';
import { MaterialInputComponent } from './../common/material-input/material-input.component';
import { DesignInputComponent } from './../common/design-input/design-input.component';
import { EquipmentInputComponent } from './../common/equipment-input/equipment-input.component';
import { Component, OnInit, AfterViewInit, ViewChildren, QueryList, ViewChild, ChangeDetectorRef } from '@angular/core';
import { ModuleBase } from "../module-base.component";
import { FFSInputBase } from "../../common/inputs/ffs-input-base";

@Component({
  selector: 'app-dent',
  templateUrl: './dent.component.html',
  styleUrls: ['./dent.component.scss']
})
export class DentComponent extends ModuleBase implements OnInit, AfterViewInit {
  name = "Dent, Gouges and dent-gouge combinations";

  @ViewChildren(FFSInputBase) inputs: QueryList<FFSInputBase>;

  @ViewChild(EquipmentInputComponent) equipmentInput: EquipmentInputComponent;
  @ViewChild(DesignInputComponent) designInput: DesignInputComponent;
  @ViewChild(MaterialInputComponent) materialInput: MaterialInputComponent;
  @ViewChild(LoadInputComponent) loadInput: LoadInputComponent;

  form: FormGroup;
  unit: Observable<IUnit>;

  assessmentLevel: any = 1;


  initDesignInput() {

  }
  initMaterialInput() {

  }
  initFlawInput() {

  }
  initLoadInput() {

  }

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

      this.http.post(`/api/dent/calculation/level${equipmentInput.assessmentLevel}/unit${equipmentInput.unitID}`, calculationParam)
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

}
