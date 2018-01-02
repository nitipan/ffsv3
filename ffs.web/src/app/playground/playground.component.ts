import { FourthOrderPolyNomialComponent } from './../common/inputs/fourth-order-poly-nomial/fourth-order-poly-nomial.component';
import { FormGroup, ValidatorFn, AbstractControl, ValidationErrors } from '@angular/forms';
import { Component, OnInit, AfterViewInit, QueryList, ViewChildren, ViewChild } from '@angular/core';
import { FFSInputBase } from '../common/inputs/ffs-input-base';
import '../common/functions';
import { toDataURL } from '../common/functions';

import { KV } from '../model/kv';

@Component({
  selector: 'app-playground',
  templateUrl: './playground.component.html',
  styleUrls: ['./playground.component.scss']
})
export class PlaygroundComponent implements OnInit, AfterViewInit {

  @ViewChildren(FFSInputBase) input: QueryList<FFSInputBase>;


  @ViewChild(FourthOrderPolyNomialComponent) polyNomial: FourthOrderPolyNomialComponent;

  form: FormGroup;


  url = 'api/lookup/generic/CrackType';

  ngAfterViewInit(): void {
    this.form = FFSInputBase.toFormGroup(this.input);
  }


  constructor() { }

  getPValue() {
    console.log(this.polyNomial.form.getRawValue());
  }
  nbOfFlaw: any;
  rowModels: KV[];

  updateNumberOfFlaw(val) {
    this.nbOfFlaw = val;
  }

  ngOnInit() {

    this.rowModels = [
      { key: 'LaminationHeight', value: 'Lamination Height' },
      { key: 'FlawDimensionCircumferentialDirection', value: 'Flaw Dimension in Circumferential Direction' },
      { key: 'FlawDimensionLongituidinalDirection', value: 'Flaw Dimension in Longitudinal Direction' },
      { key: 'MinimumMeasuredThickness', value: 'Minimum Measured Thickness' },
      { key: 'EdgeToEdgeSpacingToNearestLamination', value: 'Edge-To-Edge Spacing To Nearest Lamination' },
      { key: 'SpacingToNearestWeldJoint', value: 'Spacing To Nearest Weld Joint' },
      { key: 'SpacingToNearestMajorStructuralDiscontinuity', value: 'Spacing To Nearest Major Structural Discontinuity' },
    ];

    // MathJax.Hub.Queue(["Typeset", MathJax.Hub, this.el.nativeElement]);
    //  var reader = new FileReader();
    // reader.onload = function (e) {
    //   console.log(e);
    //   //inputForm.setValue((e.target as any).result);
    // }

    // reader.readAsDataURL()

  }

  getValue() {
    this.input.forEach(i => {
      i.checkErrors(true);
    });

    console.log(this.form.getRawValue());
  }

  rule1(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors => {
      if (this.form === undefined)
        return null;

      const name = control.value;

      const value1: number = this.form.get('value1').value

      const valid = value1 > name;

      if (!valid)
        this.form.get('file').enable();
      else
        this.form.get('file').disable();

      return valid ? { 'error': `should be lower than value 1 (${value1})` } : null;
    };
  }

  rule2(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors => {
      // const name = control.value;
      // const no = name == 'b';
      // //console.log(name, no);
      // return no ? { 'forbiddenName': { name } } : null;
      return null;
    };
  }


}
