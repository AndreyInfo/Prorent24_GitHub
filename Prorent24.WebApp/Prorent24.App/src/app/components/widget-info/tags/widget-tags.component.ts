import { Component, OnInit, Input } from "@angular/core";
import { NgForm } from "@angular/forms";
import { Entity } from "@shared/enums/entity.enum";
import { HttpService } from "@core/http.service";
import { SearchService } from "@services/search.service";
import { StringExt } from "@shared/utils/string.extension";
import { GeneralTagsEndpoints } from "@endpoints";
import { TagModel } from "@models/common/tag.model";
import { TagService } from "@services/tag.service";

@Component({
  selector: "rent-widget-tags",
  templateUrl: "./widget-tags.component.html"
})
export class WidgetTagsComponent implements OnInit {
  @Input() entityId: any;
  @Input() entityType: Entity;
  @Input() data: Array<any>;

  tag: TagModel = new TagModel();

  constructor(
    private http: HttpService,
    private search: SearchService,
    private tagService: TagService
  ) {}

  ngOnInit(): void {}

  submitTag(form: NgForm): void {
    if (form.valid) {
      this.http
        .post(
          StringExt.Format(
            GeneralTagsEndpoints.root,
            this.entityType,
            this.entityId
          ),
          form.value
        )
        .subscribe(
          data => {
            let isExistTag = this.data.find((f: any) => f.name === data.name);
            if (!isExistTag) {
              this.tagService.push(data);
              this.data.push(data);
            }
          },
          null,
          () => (this.tag = new TagModel())
        );
    }
  }

  removeTag(id: any): void {
    this.http
      .post(StringExt.Format(GeneralTagsEndpoints.delete, id))
      .subscribe();
  }
}
