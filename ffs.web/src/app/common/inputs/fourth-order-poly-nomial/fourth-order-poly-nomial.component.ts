import { FormGroup } from '@angular/forms';
import { Component, OnInit, Input, AfterViewChecked, ViewChildren, QueryList, AfterContentInit, AfterViewInit, ElementRef } from '@angular/core';
import { FFSInputBase } from "../ffs-input-base";

@Component({
  selector: 'app-fourth-order-poly-nomial',
  templateUrl: './fourth-order-poly-nomial.component.html',
  styleUrls: ['./fourth-order-poly-nomial.component.scss']
})
export class FourthOrderPolyNomialComponent implements OnInit, AfterViewInit {

  @ViewChildren(FFSInputBase) inputs: QueryList<FFSInputBase>;
  @Input() prefix: string;

  form: FormGroup;

  keys: string[] = [];

  constructor(private el: ElementRef) { }

  ngOnInit() {

  }

  ngAfterViewInit(): void {
    var formItems: any = {};
    this.inputs.forEach((i) => {
      let key = this.prefix + i.key.replace("s", "");
      formItems[key] = i.form.controls[i.key];
      this.keys.push(key);
    });
    this.form = new FormGroup(formItems);
  }

  redraw() {
    MathJax.Hub.Queue(["Typeset", MathJax.Hub, this.el.nativeElement]);
  }

  disable() {
    this.form.reset();
    this.form.disable();

  }
  enable() {
    this.form.enable();
    this.redraw();
  }
}
