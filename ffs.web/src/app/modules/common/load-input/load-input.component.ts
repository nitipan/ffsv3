import { FourthOrderPolyNomialComponent } from './../../../common/inputs/fourth-order-poly-nomial/fourth-order-poly-nomial.component';
import { IUnit } from './../../../common/unit';
import { Observable } from 'rxjs/Rx';
import { EventService } from './../../../event.service';
import { InputBase } from './../../../model/inputbase';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Component, OnInit, ViewChildren, QueryList, AfterViewInit, ChangeDetectorRef } from '@angular/core';
import { FFSInputBase } from "../../../common/inputs/ffs-input-base";
import { InputBaseComponent } from "../input-base.component";

@Component({
  selector: 'app-load-input',
  templateUrl: './load-input.component.html',
  styleUrls: ['./load-input.component.scss']
})
export class LoadInputComponent extends InputBaseComponent implements OnInit, AfterViewInit {

  @ViewChildren(FourthOrderPolyNomialComponent) polynomials: QueryList<FourthOrderPolyNomialComponent>;
  @ViewChildren(FFSInputBase) inputs: QueryList<FFSInputBase>;
  form: FormGroup;
  calculating: boolean;
  unit: Observable<IUnit>;

  constructor(private cdRef: ChangeDetectorRef, private fb: FormBuilder) {
    super();
  }

  ngOnInit() {
    this.moduleEvent.calculatingSubject.subscribe(v => {
      this.calculating = v == null;
    });

    this.moduleEvent.stepChanged.subscribe(() => {
      this.polynomials.forEach((i) => {
        i.redraw();
      });
    });

    this.unit = this.moduleEvent.unit.asObservable();
  }

  ngAfterViewInit(): void {


    this.form = FFSInputBase.toFormGroup(this.inputs);

    this.polynomials.forEach((i) => {
      i.keys.forEach((key) => {
        this.form.addControl(key, i.form.controls[key]);
      });
    });

    this.form.get("automaticallyCalculationTheNominalStressOfTheComponent").valueChanges.subscribe((v: boolean) => {
      if (v) {
        this.form.get("theNominalStressOfTheComponent").disable();
      } else {
        this.form.get("theNominalStressOfTheComponent").enable();
      }
    });

    // this.form.get("automaticcallyPrimaryStress").valueChanges.subscribe((v: boolean) => {
    //   if (v) {
    //     this.form.get("primaryStress").disable();
    //   } else {
    //     this.form.get("primaryStress").enable();
    //   }
    // });

    // this.form.get("supplementalLoad").valueChanges.subscribe((v: boolean) => {
    //   if (v) {
    //     this.form.get("supplementalStress").disable();
    //   } else {
    //     this.form.get("supplementalStress").enable();
    //   }
    // });


    this.form.get("automaticallyCalculationTheNominalStressOfTheComponent").setValue(true);
    this.form.get("automaticallyCalculationTheNominalStressOfTheComponent").disable();


    // this.form.get("automaticcallyPrimaryStress").setValue(true);
    // this.form.get("automaticcallyPrimaryStress").disable();

    // this.form.get("supplementalLoad").setValue(true);
    // this.form.get("supplementalLoad").disable();

    this.polynomials.forEach(p => p.disable());



    this.moduleEvent.designInputSubject.subscribe(v => {

      this.form.get("operatingTemperature").setValue(v.designTemperature);
    });

    this.cdRef.detectChanges();
  }
}
