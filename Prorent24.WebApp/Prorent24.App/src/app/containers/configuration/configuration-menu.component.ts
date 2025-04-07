import { Component, OnInit } from "@angular/core";
import { PagesToggleService } from "@shared/utils/toggler.service";

@Component({
  selector: "app-configuration-menu",
  templateUrl: "./configuration-menu.component.html"
})
export class ConfigurationMenuComponent implements OnInit {
  constructor(private toggler: PagesToggleService) {}

  ngOnInit(): void {
    setTimeout(() => {
      this.toggler.toggleFooter(false);
    });
  }
}
