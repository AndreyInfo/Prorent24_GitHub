import { Component, OnInit } from "@angular/core";
import { GridAbstract } from "@abstractions/grid.abstraction";
import { PagesToggleService } from "@shared/utils/toggler.service";
import { GridService } from "@services/grid.service";
import { InvoiceModel } from "@models/invoice/invoice.model";
import { Entity } from "@shared/enums/entity.enum";
import { InvoicesEndpoints, InvoiceDocumentsEndpoints } from "@endpoints";
import { TableEntity } from '@shared/enums/table-entity.enum';
import { HttpService } from '@core/http.service';
import { StringExt } from '@shared/utils/string.extension';

@Component({
  selector: "app-invoices",
  templateUrl: "./invoices.component.html"
})
export class InvoicesComponent extends GridAbstract<InvoiceModel>
  implements OnInit {
  entityType: Entity = Entity.invoices;
  users: Array<any> = new Array<any>();
  tableEntityType: TableEntity = TableEntity.invoices;

  constructor(
    private toggler: PagesToggleService,
    private http: HttpService,
    public gridService: GridService
  ) {
    super(gridService, InvoiceModel, InvoicesEndpoints);
  }

  ngOnInit(): void {
    super.ngOnInit();

    setTimeout(() => {
      this.toggler.setContent("full-height");
      this.toggler.setPageContainer("full-height");
      this.toggler.toggleFooter(false);
    });
  }

  downloadFile(): void {
    this.http
      .uploadFile(StringExt.Format(InvoiceDocumentsEndpoints.invoice, this.selected_entity.id))
      .toPromise()
      .then(res => {
        var blob = new Blob([res], { type: "application/pdf" });
        var link = document.createElement("a");
        link.href = window.URL.createObjectURL(blob);
        link.download = "invoice.pdf";
        link.click();
      });
    }

    setDefaultColumns(): void {
        let data: any = {};
        data.moduleEntity = this.tableEntityType;
        data.columns = null;

        this.gridService.setGridColumn(data).then(() => {
            this.loadData();
        });
    }

    clearFilterByPeriod() {
        this.filterPeriod.fromDate = null;
        this.filterPeriod.toDate = null;

        this.onChangedFilterByPeriod();
    }


}
