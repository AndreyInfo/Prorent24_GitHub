import { Component, OnInit, ViewChild } from "@angular/core";
import { NgForm } from "@angular/forms";
import { ActivatedRoute } from "@angular/router";
import { GridAbstract } from "@abstractions/grid.abstraction";
import { GridService } from "@services/grid.service";
import { EquipmentsStockEndpoints } from "@endpoints";
import { StockModel } from "@models/equipments/stock.model";
import { HttpService } from "@core/http.service";
import { StringExt } from "@shared/utils/string.extension";
import { ModalDirective } from "ngx-bootstrap";

@Component({
  selector: "app-equipment-stock",
  templateUrl: "./equipment-stock.component.html"
})
export class EquipmentStockComponent extends GridAbstract<StockModel>
  implements OnInit {
  @ViewChild("adjustStockModal", { static: true }) adjustStockModal: ModalDirective;

  parentId: any;

  constructor(
    public gridService: GridService,
    private http: HttpService,
    private route: ActivatedRoute
  ) {
    super(gridService, StockModel, EquipmentsStockEndpoints);

    this.parentId = this.route.parent.snapshot.params.id;
  }

  ngOnInit(): void {
    this.loadSubData(this.parentId);
  }

    onSubmitAdjustStock(form: NgForm): void {

        console.log("p2 - ");

      if (form.valid) {

          if (form.value.deliveryDate != null) {
              form.value.deliveryDate = new Date(Date.parse(form.value.deliveryDate)).toDateString();
              console.log("p1 - " + form.value.deliveryDate);
          }

      this.http
        .post(
          StringExt.Format(`${EquipmentsStockEndpoints.root}/correct`, this.parentId),
          form.value
        )
        .subscribe((data: any) => {
          this.adjustStockModal.hide();
        });
      }

      

  }
}
