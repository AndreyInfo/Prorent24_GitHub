import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { PgCardModule } from "@ui-components/card/card.module";
import { SecondarySidebarModule } from "@ui-components/secondary-sidebar/secondary-sidebar.module";
import { PgSelectModule } from "@ui-components/select/select.module";
import { jqxTreeModule } from "jqwidgets-ng/jqxtree";
import { jqxMenuModule } from "jqwidgets-ng/jqxmenu";
import { TagModule } from "@ui-components/tag/tag.module";
import { ButtonsModule } from "ngx-bootstrap/buttons";
import { pgDatePickerModule } from "@ui-components/datepicker/datepicker.module";
import { TranslateModule } from "@ngx-translate/core";
import { EnumPipeArrayModule } from "@shared/pipes/enum-array.module";
import { FoldersService } from "@services/folders.service";
import { FilterPanelComponent } from "./filter-panel.component";
import { FilterPeriodComponent } from "./filter-period/filter-period.component";
import { NumberUtilModule } from '@shared/utils/number-util.module';
import { TreeFoldersModule } from "@components/tree-folders/tree-folders.module";

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    TranslateModule,
    PgCardModule,
    TagModule,
    jqxTreeModule,
    jqxMenuModule,
    SecondarySidebarModule,
    PgSelectModule,
    ButtonsModule,
    pgDatePickerModule,
    EnumPipeArrayModule,
    NumberUtilModule,
    TreeFoldersModule
  ],
  declarations: [FilterPanelComponent, FilterPeriodComponent],
  exports: [FilterPanelComponent],
  providers: [FoldersService]
})
export class FilterPanelModule {}
