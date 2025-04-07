import { Component, ViewChild, ViewEncapsulation } from "@angular/core";

@Component({
  selector: "blank-layouts",
  template: `
    <router-outlet></router-outlet>
  `,
  styleUrls: ["./blank.component.scss"],
  encapsulation: ViewEncapsulation.None
})
export class BlankComponent {
  @ViewChild("root", { static: false }) root: any;
}
