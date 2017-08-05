import { FormGroup, ValidatorFn, AbstractControl, ValidationErrors } from '@angular/forms';
import { Component, OnInit, AfterViewInit, QueryList, ViewChildren } from '@angular/core';
import { FFSInputBase } from "../common/inputs/ffs-input-base";

@Component({
  selector: 'app-playground',
  templateUrl: './playground.component.html',
  styleUrls: ['./playground.component.scss']
})
export class PlaygroundComponent implements OnInit, AfterViewInit {

  @ViewChildren(FFSInputBase) input: QueryList<FFSInputBase>

  form: FormGroup;

  ngAfterViewInit(): void {
    this.form = FFSInputBase.toFormGroup(this.input);


  }


  constructor() { }

  ngOnInit() {
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
