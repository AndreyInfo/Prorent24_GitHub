import { Component, OnInit, Output, EventEmitter, Input } from "@angular/core";
import { MatTabChangeEvent } from "@angular/material";

@Component({
  selector: "rent-crew-and-vehicles",
  templateUrl: "./crew-and-vehicles.component.html"
})
export class CrewAndVehiclesComponent implements OnInit {
  @Input() selectMode: string;
  @Input() checkedObserve: boolean;
  
  @Output() changeTab = new EventEmitter<any>();
  changeTabEvent(event: MatTabChangeEvent): void {
    this.changeTab.emit(event.index);
  }

  @Output() doubleClickRow = new EventEmitter<any>();
  doubleClickRowEvent(data: any): void {
    this.doubleClickRow.emit(data);
  }

  @Output() checkedRows = new EventEmitter<any>();
  checkedRowsEvent(data: any): void {
    this.checkedRows.emit(data);
  }

  ngOnInit(): void {}
}
