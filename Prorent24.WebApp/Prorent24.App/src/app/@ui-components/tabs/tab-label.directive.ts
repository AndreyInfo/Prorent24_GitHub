import { Directive, ElementRef, HostBinding, Input } from "@angular/core";
import { toBoolean } from "@shared/utils/convert";

@Directive({
  selector: "[pg-tab-label]",
  host: {
    "[class.nav-item]": "true"
  }
})
export class pgTabLabelDirective {
  public _disabled = false;

  @Input()
  @HostBinding("class.nav-item-disabled")
  set disabled(value: boolean) {
    this._disabled = toBoolean(value);
  }

   get disabled(): boolean {
    return this._disabled;
  }

  constructor(public elementRef: ElementRef) {}

  getOffsetLeft(): number {
    return this.elementRef.nativeElement.offsetLeft;
  }

  getOffsetWidth(): number {
    return this.elementRef.nativeElement.offsetWidth;
  }

  getOffsetTop(): number {
    return this.elementRef.nativeElement.offsetTop;
  }

  getOffsetHeight(): number {
    return this.elementRef.nativeElement.offsetHeight;
  }
}
