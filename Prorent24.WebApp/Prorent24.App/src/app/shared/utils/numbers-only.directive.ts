import { Directive, ElementRef, HostListener, Input } from "@angular/core";

@Directive({
  selector: "input[numbersOnly]"
})
export class NumberDirective {
  @Input() minValue: number;
  @Input() maxValue: number;
  @Input() numType: "number" | "decimal" = "number";

  private el: HTMLInputElement;

  private regex = {
    number: new RegExp(/^\d+$/),
    decimal: new RegExp(/^(\d+(?:[\.\,]\d{0,8})?)$/)
  };

  private specialKeys = {
    number: ["Backspace", "Tab", "End", "Home", "ArrowLeft", "ArrowRight"],
    decimal: ["Backspace", "Tab", "End", "Home", "ArrowLeft", "ArrowRight"]
  };

  constructor(private _el: ElementRef) {
    this.el = this._el.nativeElement;
  }

  @HostListener("keydown", ["$event"]) onKeyDown(e) {
    switch (this.numType) {
      case "decimal":
        if (this.specialKeys[this.numType].indexOf(e.key) !== -1) {
          return;
        }

        let current: string = this._el.nativeElement.value;
        let next = current.concat(e.key);
        if (next && !String(next).match(this.regex.decimal)) {
          event.preventDefault();
        }
        break;
      default:
        if (
          // Allow: Delete, Backspace, Tab, Escape, Enter
          [46, 8, 9, 27, 13].indexOf(e.keyCode) !== -1 ||
          (e.keyCode === 65 && e.ctrlKey === true) || // Allow: Ctrl+A
          (e.keyCode === 67 && e.ctrlKey === true) || // Allow: Ctrl+C
          (e.keyCode === 86 && e.ctrlKey === true) || // Allow: Ctrl+V
          (e.keyCode === 88 && e.ctrlKey === true) || // Allow: Ctrl+X
          (e.keyCode === 65 && e.metaKey === true) || // Cmd+A (Mac)
          (e.keyCode === 67 && e.metaKey === true) || // Cmd+C (Mac)
          (e.keyCode === 86 && e.metaKey === true) || // Cmd+V (Mac)
          (e.keyCode === 88 && e.metaKey === true) || // Cmd+X (Mac)
          (e.keyCode >= 35 && e.keyCode <= 39) // Home, End, Left, Right
        ) {
          return; // let it happen, don't do anything
        }
        // Ensure that it is a number and stop the keypress
        if (
          (e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) &&
          (e.keyCode < 96 || e.keyCode > 105)
        ) {
          e.preventDefault();
        }
    }
  }

  @HostListener("input", ["$event"]) onchange(event: any) {
    let value = event.target.value;

    if (this.maxValue) {
      if (parseFloat(event.target.value) > this.maxValue) {
        value = value.substr(0, value.length - 1);
        this.el.value = value;
        let event = new Event("input", { bubbles: true });
        this.el.dispatchEvent(event);
      }
    }

    if (this.minValue) {
      if (parseFloat(event.target.value) < this.minValue) {
        value = this.minValue;
        this.el.value = value;
        let event = new Event("input", { bubbles: true });
        this.el.dispatchEvent(event);
      }
    }
  }
}
