import { FormGroup, FormControl, FormBuilder } from '@angular/forms';
import { ModuleBase } from './../module-base.component';
import { Component, OnInit, Injectable, Input, QueryList, ContentChildren, AfterContentInit, forwardRef, AfterViewInit, ViewChildren } from '@angular/core';
import { FFSInputBase } from "../../common/inputs/ffs-input-base";


@Component({
  selector: 'app-brittle-fracture',
  templateUrl: './brittle-fracture.component.html',
  styleUrls: ['./brittle-fracture.component.scss']
})
export class BrittleFractureComponent extends ModuleBase implements OnInit, AfterViewInit {

  @ViewChildren(FFSInputBase) inputs: QueryList<FFSInputBase>;

  form: FormGroup;

  constructor() {
    super();
  }

  ngAfterViewInit(): void {
    this.form = this.toFormGroup(this.inputs);
    this.form.valueChanges.subscribe((v) => {
      // do something
      console.log(v);
    });

    this.form.get("isEnabled").valueChanges.subscribe((v) => {
      if (v)
        this.form.get("someInput").enable();
      else
        this.form.get("someInput").disable();
    });

  }

  ngOnInit() {
  }

  onSubmit() {
    console.log(this.form.value);
  }

}



