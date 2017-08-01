import { FormGroup } from '@angular/forms';
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
    console.log(this.form.getRawValue());
  }
}
