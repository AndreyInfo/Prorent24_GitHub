import { Component, Output, EventEmitter, Input } from "@angular/core";
import { MovementOptions } from "./movement-option";

@Component({
  selector: "rent-movement-state",
  templateUrl: "./movement-state.component.html"
})
export class MovementStateComponent {
  @Input() title: string;
  @Input() side: string; // left or right
  @Input() collection: Array<any> = new Array<any>();
  @Input() currentCollectionState: string; // collection name
  @Input() nextCollectionState: string; // collection name
  @Input() newState: string;

  @Output() move = new EventEmitter<any>();
  moveEvent(data: MovementOptions): void {
    this.move.emit(data);
  }

  onlyDigits(event: any, max: number): boolean {
    const charCode: number = event.which ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;
  }
}
