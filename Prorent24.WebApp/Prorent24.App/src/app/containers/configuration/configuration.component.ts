import { Component, OnInit } from "@angular/core";
import { PagesToggleService } from "@shared/utils/toggler.service";

@Component({
  selector: "app-configuration",
  templateUrl: "./configuration.component.html"
})
export class ConfigurationComponent implements OnInit {
  constructor(private toggler: PagesToggleService) {}

  ngOnInit(): void {
    setTimeout(() => {
      this.toggler.toggleFooter(false);
    });
  }
}
