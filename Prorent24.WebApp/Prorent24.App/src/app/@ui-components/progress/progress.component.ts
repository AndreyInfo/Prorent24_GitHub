import { Component, Input } from "@angular/core";

@Component({
  selector: "pg-progress",
  templateUrl: "./progress.component.html"
})
export class ProgressComponent {
  _value: number;
  _value2: any;

  @Input() type: string;
  @Input() color: string;
  @Input() thick: boolean;
  @Input() indeterminate: boolean;

  @Input() set value(value: number) {
    if (this.type == "circle") {
      this._value = (value / 100) * 360;
      if (this.value >= 50) {
        this._value2 = true;
      }
    } else this._value = value;
  }

  get value() {
    return this._value;
  }

  get value2() {
    return this._value2;
  }
}
