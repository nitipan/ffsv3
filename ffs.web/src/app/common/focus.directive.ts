import { Directive, ElementRef, Renderer, Input, EventEmitter, OnInit } from '@angular/core';

@Directive({
  selector: '[focus]'
})
export class FocusDirective implements OnInit {

  @Input('focus') focusEvent: EventEmitter<boolean>;


  constructor(private element: ElementRef, private renderer: Renderer) { }

  ngOnInit() {
    this.focusEvent.subscribe(event => {
      this.renderer.invokeElementMethod(this.element.nativeElement, 'focus', []);
    });
  }
}