import { Component, OnInit } from "@angular/core";
import { PagesToggleService } from "@shared/utils/toggler.service";
import { Entity } from "@shared/enums/entity.enum";
import { GridService } from "@services/grid.service";
import { GridAbstract } from "@abstractions/grid.abstraction";
import { ContactModel } from "@models/contacts/contact.model";
import { ContactsEndpoints } from "@endpoints";
import { TableEntity } from '@shared/enums/table-entity.enum';

@Component({
  selector: "app-contacts",
  templateUrl: "./contacts.component.html"
})
export class ContactsComponent extends GridAbstract<ContactModel>
  implements OnInit {
  entityType: Entity = Entity.contacts;
  tableEntityType: TableEntity = TableEntity.contacts;
  
  constructor(
    private toggler: PagesToggleService,
    public gridService: GridService
  ) {
    super(gridService, ContactModel, ContactsEndpoints);
  }

  ngOnInit(): void {
    this.loadData(this.filter);

    setTimeout(() => {
      this.toggler.setContent("full-height");
      this.toggler.setPageContainer("full-height");
      this.toggler.toggleFooter(false);
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

}
