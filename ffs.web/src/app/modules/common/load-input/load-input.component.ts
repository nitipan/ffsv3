import { IUnit } from './../../../common/unit';
import { Observable } from 'rxjs/Rx';
import { EventService } from './../../../event.service';
import { InputBase } from './../../../model/inputbase';
import { FormGroup } from '@angular/forms';
import { Component, OnInit, ViewChildren, QueryList, AfterViewInit, ChangeDetectorRef } from '@angular/core';
import { FFSInputBase } from "../../../common/inputs/ffs-input-base";

@Component({
  selector: 'app-load-input',
  templateUrl: './load-input.component.html',
  styleUrls: ['./load-input.component.scss']
})
export class LoadInputComponent implements OnInit, AfterViewInit {


  @ViewChildren(FFSInputBase) inputs: QueryList<FFSInputBase>;
  form: FormGroup;
  calculating: boolean;
  unit: Observable<IUnit>;
  constructor(private cdRef: ChangeDetectorRef, private eventService: EventService) {

    eventService.calculatingSubject.subscribe(v => {
      this.calculating = v == null;
    });

    this.unit = this.eventService.unit.asObservable();
  }

  ngOnInit() {
  }

  ngAfterViewInit(): void {
    this.form = FFSInputBase.toFormGroup(this.inputs);

    this.form.get("automaticallyCalculationTheNominalStressOfTheComponent").valueChanges.subscribe((v: boolean) => {
      if (v) {
        this.form.get("theNominalStressOfTheComponent").disable();
      } else {
        this.form.get("theNominalStressOfTheComponent").enable();
      }
    });

    this.form.get("automaticcallyPrimaryStress").valueChanges.subscribe((v: boolean) => {
      if (v) {
        this.form.get("primaryStress").disable();
      } else {
        this.form.get("primaryStress").enable();
      }
    });

    this.form.get("supplementalLoad").valueChanges.subscribe((v: boolean) => {
      if (v) {
        this.form.get("supplementalStress").disable();
      } else {
        this.form.get("supplementalStress").enable();
      }
    });


    this.form.get("automaticallyCalculationTheNominalStressOfTheComponent").setValue(true);
    this.form.get("automaticallyCalculationTheNominalStressOfTheComponent").disable();


    this.form.get("automaticcallyPrimaryStress").setValue(true);
    this.form.get("automaticcallyPrimaryStress").disable();

    this.form.get("supplementalLoad").setValue(true);
    this.form.get("supplementalLoad").disable();
    this.cdRef.detectChanges();
  }

}
