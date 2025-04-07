import { Component, OnInit, Input } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { CrewMemberRateModel } from "@models/crew/crew-member-rate.model";
import { GridService } from "@services/grid.service";
import { GridAbstract } from "@abstractions/grid.abstraction";
import { StaffRatesEndpoints } from "@endpoints";
import { TimeUnit } from "@shared/enums/time-unit.enum";

@Component({
  selector: "rent-staff-rates",
  templateUrl: "./staff-rates.component.html"
})
export class StaffRatesComponent extends GridAbstract<CrewMemberRateModel>
  implements OnInit {
  timeUnit = TimeUnit;

  constructor(public gridService: GridService, private route: ActivatedRoute) {
    super(gridService, CrewMemberRateModel, StaffRatesEndpoints);

    this.parentId = this.route.parent.snapshot.params.id;
  }

  ngOnInit(): void {
    this.loadSubData(this.parentId);
  }
}
