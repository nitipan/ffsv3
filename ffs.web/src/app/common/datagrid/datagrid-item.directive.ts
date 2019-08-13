import { Directive, ElementRef, Input } from '@angular/core';

@Directive({
  selector: '[appDataGridItem]'
})
export class DataGridItemDirective {
  @Input() dataGridItemRow: any;
  @Input() dataGridItemColumn: any;
  constructor(private el: ElementRef) {}

  setData(value) {
    this.el.nativeElement.value = value;
  }
}
