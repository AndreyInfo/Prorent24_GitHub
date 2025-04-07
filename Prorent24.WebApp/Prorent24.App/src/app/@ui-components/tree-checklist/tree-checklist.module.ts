import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { TreeChecklistComponent } from "./tree-checklist.component";
import { TreeModule } from 'angular-tree-component';

@NgModule({
  imports: [CommonModule, FormsModule, TreeModule.forRoot()],
  declarations: [TreeChecklistComponent],
  exports: [TreeChecklistComponent]
})
export class TreeCheckListModule {}
