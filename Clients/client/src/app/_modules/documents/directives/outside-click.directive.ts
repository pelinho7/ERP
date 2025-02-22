import { Directive, ElementRef, EventEmitter, HostListener, Output, Renderer2 } from '@angular/core';

@Directive({
  selector: '[appOutsideClick]'
})
export class OutsideClickDirective {

  @Output() public appOutsideClick = new EventEmitter<ElementRef>();

  constructor(private el: ElementRef, private renderer: Renderer2) {}

  @HostListener('document:click', ['$event'])
  public onClick(event: Event): void {
    const clickedInside = this.el.nativeElement.contains(event.target);
    if (!clickedInside) {
      this.appOutsideClick.emit(this.el);
    }
  }
}
