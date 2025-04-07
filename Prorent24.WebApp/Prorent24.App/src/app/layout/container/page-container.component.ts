import { Component, HostListener, ViewEncapsulation } from "@angular/core";
import { PagesToggleService } from "@shared/utils/toggler.service";

@Component({
  selector: "page-container",
  template: "<ng-content></ng-content>",
  styleUrls: ["./page-container.scss"],
  encapsulation: ViewEncapsulation.None,
  host: {
    class: "page-container"
  }
})
export class PageContainer {
  constructor(private toggler: PagesToggleService) {}

  // @HostListener("mouseenter", ["$event"])
  // @HostListener("tap", ["$event"])
  triggerMouseOverCall(): void {
    this.toggler.triggerPageContainerHover(true);
  }
}
