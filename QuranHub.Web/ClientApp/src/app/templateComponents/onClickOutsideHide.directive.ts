import { Directive, ElementRef, HostListener, Output, EventEmitter } from '@angular/core';

@Directive({
  selector: '[clickOutside]'
})

export class onClickOutsideHideDirective {
  constructor(private elementRef: ElementRef) {}

  @Output()
  clickOutside: EventEmitter<null> = new EventEmitter<null>();

  @HostListener('document:click', ['$event.target'])
  onMouseEnter(targetElement: any) {
    const clickedInside = this.elementRef.nativeElement.contains(targetElement);
    if (!clickedInside) {
      this.clickOutside.emit(null);
    }
  }
}
