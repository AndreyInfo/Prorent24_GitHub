import {
  Component,
  OnInit,
  ViewChild,
  Injector,
  AfterViewInit
} from "@angular/core";
import { NgForm } from "@angular/forms";
import { ModalDirective } from "ngx-bootstrap";
import { PagesToggleService } from "@shared/utils/toggler.service";
import { TreeGridAbstract } from "@abstractions/tree-grid.abstraction";
import { ActivatedRoute } from "@angular/router";
import { CustomDirectoryService } from "@services/custom-directory.service";
import { Entity } from "@shared/enums/entity.enum";
import { ProjectFunctionModel } from "@models/project/project-function.model";
import {
  ProjectConcreteFunctionsEndpoints,
  ProjectFunctionGroupsEndpoints,
  ProjectsEndpoints
} from "@endpoints";
import { ProjectFunctionGroupModel } from "@models/project/project-function-group.model";
import { ProjectFunctionType } from "@shared/enums/project-function-type.enum";
import { TimeUnit } from "@shared/enums/time-unit.enum";
import { StringExt } from "@shared/utils/string.extension";
import { HttpService } from "@core/http.service";
import { ProjectModel } from "@models/project/project.model";
import { ProjectTime } from "@models/project/project-time.model";

@Component({
  selector: "app-project-staff-and-vehicles",
  templateUrl: "./project-staff-and-vehicles.component.html"
})
export class ProjectStaffAndVehiclesComponent
  extends TreeGridAbstract<ProjectFunctionModel>
  implements OnInit, AfterViewInit {
  @ViewChild("groupModal", { static: true }) groupModal: ModalDirective;
  entityType: Entity = Entity.projects;
  vatTypes: Array<any>;
  timeUnit = TimeUnit;
  functionType: any = ProjectFunctionType;

  group: ProjectFunctionGroupModel = new ProjectFunctionGroupModel();

  defaultPeriodUseStart: Date;
  defaultPeriodUseEnd: Date;
  defaultPeriodPlanningStart: Date;
  defaultPeriodPlanningEnd: Date;

  trueIcon: string = `<i class="fa fa-check hint-text"></i>`;
  falseIcon: string = `<i class="fa fa-times hint-text"></i>`;

  constructor(
    public injector: Injector,
    private toggler: PagesToggleService,
    private http: HttpService,
    private customDirectory: CustomDirectoryService,
    private route: ActivatedRoute
  ) {
    super(injector, ProjectFunctionModel, ProjectConcreteFunctionsEndpoints);

    this.parentId = this.route.parent.snapshot.params.id;
  }

  ngOnInit(): void {

    this.loadSubData(this.parentId);

    this.http
      .get(StringExt.Format(ProjectsEndpoints.single, this.parentId))
      .subscribe((data: ProjectModel) => {
        let usePeriod: ProjectTime = data.times.find(f => f.defaultUsagePeriod);
        if (usePeriod) {
          this.defaultPeriodUseStart = usePeriod.from;
          this.defaultPeriodUseEnd = usePeriod.until;
        }

        let planningPeriod: ProjectTime = data.times.find(
          f => f.defaultPlanPeriod
        );
        if (planningPeriod) {
          this.defaultPeriodPlanningStart = planningPeriod.from;
          this.defaultPeriodPlanningEnd = planningPeriod.until;
        }
      });

    setTimeout(() => {
      this.toggler.setContent("full-height");
      this.toggler.setPageContainer("full-height");
      this.toggler.toggleFooter(false);
    });

    this.customDirectory
      .getVatClasses()
      .subscribe(data => (this.vatTypes = data));

    this.entitySubmitComplete.subscribe(data => {
      data.uid = this.selected_entity["uid"];
      this.setGridIcons(data);
    });
  }

  ngAfterViewInit(): void {
    this.treeGrid.onBindingComplete.subscribe(() => {
      this.treeGrid.setColumnProperty("groupName", "width", 150);
      this.treeGrid.setColumnProperty("periodOfUse", "width", 240);
      this.treeGrid.setColumnProperty("type", "cellsAlign", "center");
      this.treeGrid.setColumnProperty("type", "text", " ");
      this.treeGrid.setColumnProperty("includeInPrice", "cellsAlign", "center");
      this.treeGrid.setColumnProperty("showInPlaner", "cellsAlign", "center");

      this.treeGrid.getRows().forEach((row: any) => {
        this.treeGrid.expandRow(row.uid);
        row.records.forEach((r: any) => {
          this.setGridIcons(r);
        });
      });
    });
  }

  //#region group functions
  addGroup(): void {
    this.group = new ProjectFunctionGroupModel();
    this.group.useFrom = this.defaultPeriodUseStart;
    this.group.useUntil = this.defaultPeriodUseEnd;
    this.group.planFrom = this.defaultPeriodPlanningStart;
    this.group.planUntil = this.defaultPeriodPlanningEnd;

    this.groupModal.show();
  }

  submitGroup(form: NgForm): void {
    if (form.valid) {
      if (form.value.id) {
        this.http
          .post(
            StringExt.Format(
              ProjectFunctionGroupsEndpoints.single,
              this.parentId,
              form.value.id
            ),
            form.value
          )
          .subscribe(
            data => {
              this.treeGrid.updateRow(this.selected_entity["uid"], data);
            },
            null,
            () => {
              this.groupModal.hide();
            }
          );
      } else {
        this.http
          .post(
            StringExt.Format(
              ProjectFunctionGroupsEndpoints.root,
              this.parentId
            ),
            form.value
          )
          .subscribe(
            (data: any) => {
              data.records = [];
              this.treeGrid.addRow(null, data, "last");
            },
            null,
            () => {
              this.groupModal.hide();
            }
          );
      }
    }
  }
  //#endregion

  //#region functions
  addRow(data: any): void {
    debugger

    if (this.selected_entity) {
      let group: any =
        this.selected_entity["level"] === 0
          ? this.selected_entity
          : this.selected_entity["parent"];

      let groupRow: any = this.treeGrid.getRow(group.uid);

      data.groupId = group.id;
      data.parentFunctionId = data.id;
      data.planFrom = groupRow.planFrom;
      data.planUntil = groupRow.planUntil;
      data.useFrom = groupRow.useFrom;
      data.useUntil = groupRow.useUntil;

      let functionExistId: number;
      let functionExist: any = this.treeGrid
        .getRow(group.uid)
        .records.find(f => f.functionId === data.id);

      if (functionExist) {
        functionExistId = functionExist.id;
        data.quantity = functionExist.quantity + 1;
      }

      this.http
        .post(
          StringExt.Format(
            ProjectConcreteFunctionsEndpoints.single,
            this.parentId,
            functionExistId
          ),
          data
        )
        .subscribe(data => {
          if (functionExist) {
            this.treeGrid.updateRow(functionExist.uid, data);
          } else {
            data.records = data.childrens;
            this.treeGrid.addRow(null, data, "last", group.uid);
          }

          this.treeGrid.expandRow(group.uid);
          this.setGridIcons(data);
        });
    }
  }
  //#endregion

  onEditModal(id?: any): void {
    if (id) {
      if (this.selected_entity["level"] === 0) {
        Object.assign(this.group, this.selected_entity);
        this.groupModal.show();
      }
      if (this.selected_entity["level"] === 1) {
        this.http
          .get(
            StringExt.Format(
              ProjectConcreteFunctionsEndpoints.single,
              this.parentId,
              this.selected_entity.id
            )
          )
          .subscribe(
            data => {
              this.entity = data;
            },
            null,
            () => {
              this.rowModal.show();
            }
          );
      }
    }
  }

  deleteRows(): void {
    let rows: any[] = this.treeGrid.getCheckedRows();
    let groupIds: any[] = [];
    rows.forEach((item: any) => {
      if (item.level === 0) {
        groupIds.push({
          uid: item.uid,
          id: item.id
        });
      }

      if (item.level > 0) {
        this.grid.deleteRow(item.id, this.parentId).then(_ => {
          this.treeGrid.deleteRow(item.uid);
        });
      }
    });

    groupIds.forEach(g => {
      this.http
        .post(
          StringExt.Format(
            ProjectFunctionGroupsEndpoints.delete,
            this.parentId,
            g.id
          )
        )
        .subscribe(null, null, () => {
          this.treeGrid.deleteRow(g.uid);
        });
    });

    this.changeRemoveArrayMode();
  }
  
  private setGridIcons(row: any): void {
    let typeIcon: string;

    if (row.type === "crew") {
      typeIcon = `<i class="fa fa-user"></i>`;
    }
    if (row.type === "transport") {
      typeIcon = `<i class="fa fa-truck"></i>`;
    }
    this.treeGrid.setCellValue(row.uid, "type", typeIcon);

    this.treeGrid.setCellValue(
      row.uid,
      "includeInPrice",
      row.includeInPrice === true ? this.trueIcon : this.falseIcon
    );
    this.treeGrid.setCellValue(
      row.uid,
      "showInPlaner",
      row.showInPlaner === true ? this.trueIcon : this.falseIcon
    );
  }
}
