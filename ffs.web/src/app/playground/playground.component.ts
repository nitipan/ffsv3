import { FourthOrderPolyNomialComponent } from './../common/inputs/fourth-order-poly-nomial/fourth-order-poly-nomial.component';
import { FormGroup, ValidatorFn, AbstractControl, ValidationErrors } from '@angular/forms';
import { Component, OnInit, AfterViewInit, QueryList, ViewChildren, ViewChild } from '@angular/core';
import { FFSInputBase } from "../common/inputs/ffs-input-base";
import '../common/functions';
import { toDataURL } from "../common/functions";

@Component({
  selector: 'app-playground',
  templateUrl: './playground.component.html',
  styleUrls: ['./playground.component.scss']
})
export class PlaygroundComponent implements OnInit, AfterViewInit {

  @ViewChildren(FFSInputBase) input: QueryList<FFSInputBase>


  @ViewChild(FourthOrderPolyNomialComponent) polyNomial: FourthOrderPolyNomialComponent;

  form: FormGroup;


  ngAfterViewInit(): void {
    this.form = FFSInputBase.toFormGroup(this.input);
  }


  constructor() { }

  getPValue() {
    console.log(this.polyNomial.form.getRawValue());
  }

  ngOnInit() {
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
      if (this.form == undefined)
        return null;

      const name = control.value;

      let value1: number = this.form.get("value1").value

      var valid = value1 > name;

      if (!valid)
        this.form.get("file").enable();
      else
        this.form.get("file").disable();

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
