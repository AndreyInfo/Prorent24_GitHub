import {
  Component,
  Input,
  ViewChild,
  TemplateRef,
  OnChanges
} from "@angular/core";
import { Entity } from "@shared/enums/entity.enum";
import { WidgetTemplate } from "@shared/enums/widget-template.enum";
import * as moment from "moment";

enum WidgetType {
  detail = "DetailEntity",
  tags = "TagEntity",
  tasks = "TaskEntity",
  note = "NoteEntity",
  files = "FileEntity",
  viewTimeLine = "ViewTimeLine",
  detailLinkedItem = "DetailsOfLinkedItem",
  lastNotesLinkedItem = "LastNotesLinkedToThisItem",
  lastTasksLinkedItem = "LastTasksLinkedToThisItem"
}

@Component({
  selector: "rent-widget-info",
  templateUrl: "./widget-info.component.html"
})
export class WidgetInfoComponent implements OnChanges {
  @Input() entityId: any;
  @Input() entityType: Entity;
  @Input() data: Array<any>;

  templates: Array<WidgetTemplate> = new Array<WidgetTemplate>();

  // view values
  details: any[];
  tags: any;
  tasks: any;
  notes: Array<any>;
  viewTimeLine: any;
  detailLinkedItem: any;
  lastNotesLinkedItem: any;
  lastTasksLinkedItem: any;
  files: any[];

  // widget templates
  @ViewChild("detailsTemplate", { static: true }) detailsTemplate: TemplateRef<any>;
  @ViewChild("tagsTemplate", { static: true }) tagsTemplate: TemplateRef<any>;
  @ViewChild("tasksTemplate", { static: true }) tasksTemplate: TemplateRef<any>;
  @ViewChild("filesTemplate", { static: true }) filesTemplate: TemplateRef<any>;
  @ViewChild("notesTemplate", { static: true }) notesTemplate: TemplateRef<any>;
  @ViewChild("viewTimeLineTemplate", { static: true }) viewTimeLineTemplate: TemplateRef<any>;
  @ViewChild("detailLinkedItemTemplate", { static: true }) detailLinkedItemTemplate: TemplateRef<
    any
  >;
  @ViewChild("lastNotesLinkedItemTemplate", { static: true })
  lastNotesLinkedItemTemplate: TemplateRef<any>;
  @ViewChild("lastTasksLinkedItemTemplate", { static: true })
  lastTasksLinkedItemTemplate: TemplateRef<any>;

  ngOnChanges(): void {
    this.templates = new Array<WidgetTemplate>();

    if (this.data) {
      this.data.forEach(element => {
        switch (element.entity) {
          case WidgetType.detail:
            this.templates.push(WidgetTemplate.details);
            this.details = element.data;
            this.details.forEach((f: any) => {
              if(f.type === "date"){
                if (moment(f.value).isValid()) {
                  var stillUtc = moment.utc(f.value).toDate();
                  f.value = moment(stillUtc)
                    .local()
                    .format("DD.MM.YYYY HH:mm");
                }
              }
            });
            break;
          case WidgetType.tags:
            this.templates.push(WidgetTemplate.tags);
            this.tags = element.data;
            break;
          case WidgetType.viewTimeLine:
            this.templates.push(WidgetTemplate.viewTimeLine);
            this.viewTimeLine = element.data;
            break;
          case WidgetType.tasks:
            this.templates.push(WidgetTemplate.tasks);
            this.tasks = element.data;
            break;
          case WidgetType.note:
            this.templates.push(WidgetTemplate.notes);
            this.notes = element.data;
            break;
          case WidgetType.detailLinkedItem:
            this.templates.push(WidgetTemplate.detailLinkedItem);
            this.detailLinkedItem = element.data;
            break;
          case WidgetType.lastNotesLinkedItem:
            this.templates.push(WidgetTemplate.lastNotesLinkedItem);
            this.lastNotesLinkedItem = element.data;
            break;
          case WidgetType.lastTasksLinkedItem:
            this.templates.push(WidgetTemplate.lastTasksLinkedItem);
            this.lastTasksLinkedItem = element.data;
            break;
          case WidgetType.files:
            this.templates.push(WidgetTemplate.files);
            this.files = element.data;
            break;
          default:
            break;
        }
      });
    }
  }

  loadTemplate(type: WidgetTemplate): any {
    switch (type) {
      case WidgetTemplate.details:
        return this.detailsTemplate;
      case WidgetTemplate.tags:
        return this.tagsTemplate;
      case WidgetTemplate.tasks:
        return this.tasksTemplate;
      case WidgetTemplate.files:
        return this.filesTemplate;
      case WidgetTemplate.notes:
        return this.notesTemplate;
      case WidgetTemplate.viewTimeLine:
        return this.viewTimeLineTemplate;
      case WidgetTemplate.detailLinkedItem:
        return this.detailLinkedItemTemplate;
      case WidgetTemplate.lastNotesLinkedItem:
        return this.lastNotesLinkedItemTemplate;
      case WidgetTemplate.lastTasksLinkedItem:
        return this.lastTasksLinkedItemTemplate;
      default:
        return null;
    }
  }
}
