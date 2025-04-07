import { Component, OnInit, Input } from "@angular/core";
declare var pg: any;

@Component({
  selector: "pg-container",
  templateUrl: "./container.component.html"
})
export class ContainerComponent implements OnInit {
  _enableHorizontalContainer: boolean = false;
  _extraClass: string = "";
  _extraHorizontalClass: string = "";

  constructor() {}

  @Input()
  set extraClass(value: string) {
    this._extraClass = value;
  }

  @Input()
  set extraHorizontalClass(value: string) {
    this._extraHorizontalClass = value;
  }

  ngOnInit(): void {
    this._enableHorizontalContainer = pg.isHorizontalLayout;
  }
}
