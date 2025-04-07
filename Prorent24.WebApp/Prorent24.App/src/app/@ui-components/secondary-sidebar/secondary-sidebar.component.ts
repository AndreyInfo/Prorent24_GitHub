import { Component, OnInit, OnDestroy, Input } from "@angular/core";
import { PagesToggleService } from "@shared/utils/toggler.service";

@Component({
  selector: "rent-secondary-sidebar",
  templateUrl: "./secondary-sidebar.component.html",
  styleUrls: ["./secondary-sidebar.component.scss"]
})
export class SecondarySidebarComponent implements OnInit, OnDestroy {
  _toggleMobileSidebar = false;
  _togglePosition;
  _extraClass;
  _service;

  @Input()
  set extraClass(value: string) {
    this._extraClass = value;
  }

  constructor(private toggler: PagesToggleService) {}

  ngOnInit(): void {
    this._service = this.toggler.secondarySideBarToggle.subscribe(state => {
      if (typeof state === "boolean") {
        this._toggleMobileSidebar = state;
      } else {
        this._toggleMobileSidebar = state.open;
        let rect: any = state.$event.target.getBoundingClientRect();
        this._togglePosition = {
          position: "",
          top: rect.top + rect.height + "px",
          left: rect.left + "px",
          transform: "translateX(-50%)"
        };
      }
    });
  }

  ngOnDestroy(): void {
    this._service.unsubscribe();
  }
}
