import { Injector, Output, EventEmitter, Input } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { NgForm } from "@angular/forms";
import { BaseModel } from "@models/base.model";
import { IBaseEndPoints } from "@endpoints";
import { HttpService } from "@core/http.service";
import { Entity } from "@shared/enums/entity.enum";
import { StringExt } from "@shared/utils/string.extension";
import { MessageService } from "@ui-components/notification/notification.service";

export abstract class FormAbstract<T extends BaseModel> {
  protected http: HttpService;
  protected activateRoute: ActivatedRoute;
  protected router: Router;
  protected notification: MessageService;

  @Input() parentId: any;

  @Output() onChangeModel = new EventEmitter<any>();
  changeModelEvent(data: any): void {
    this.onChangeModel.emit(data);
  }

  @Output() onDataLoadComplete = new EventEmitter<any>();
  dataLoadComplete(data: any): void {
    this.onDataLoadComplete.emit(data);
  }

  onSaveComplete = new EventEmitter<any>();

  id: any;
  entity: T;

  constructor(
    private injectorObj: Injector,
    public entityType: Entity,
    private type: new () => T,
    private endPoint: IBaseEndPoints,
    private isChild?: boolean
  ) {
    this.http = this.injectorObj.get(HttpService);
    this.activateRoute = this.injectorObj.get(ActivatedRoute);
    this.router = this.injectorObj.get(Router);
    this.notification = this.injectorObj.get(MessageService);

    this.id = isChild
      ? this.activateRoute.parent.snapshot.params.id
      : this.activateRoute.snapshot.params.id;

    this.entity = new this.type();
  }

  ngOnInit(): void {
    if (this.id) {
      this.http
        .get(
          this.parentId
            ? StringExt.Format(this.endPoint.single, this.parentId, this.id)
            : StringExt.Format(this.endPoint.single, this.id)
        )
        .subscribe(data => {
          Object.assign(this.entity, data);
          this.changeModelEvent(data);
          this.dataLoadComplete(data);
        });
    }
  }

  submitData(
    form: NgForm,
    redirectToParent?: boolean,
    goToEditAfter?: boolean,
    levelNodeBack?: number
  ): void {
      debugger

      if (form.value.birthDate != null) {
          form.value.birthDate = new Date(Date.parse(form.value.birthDate)).toDateString();
      }
      if (form.value.purchaseDate != null) {
          form.value.purchaseDate = new Date(Date.parse(form.value.purchaseDate)).toDateString();
      }
      

      console.log("p1-submitData 222 " + form.value.purchaseDate);

    if (form.valid) {
      if (this.entity.id) {
        this.http
          .post(
            this.parentId
              ? StringExt.Format(
                  this.endPoint.single,
                  this.parentId,
                  this.entity.id
                )
              : StringExt.Format(this.endPoint.single, this.entity.id),
            form.value
          )
          .subscribe(
            data => {
              this.changeModelEvent(data);
              this.entity = data;
            },
            null,
            () => {
              if (redirectToParent) {
                this.router.navigate(["."], {
                  relativeTo: this.activateRoute.parent
                });
              }

              this.notification.create("success", "Данные сохранены", {
                Position: "top-right",
                Style: "flip",
                Duration: 3000
              });

              this.onSaveComplete.emit(this.entity);
            }
          );
      } else {
        this.http
          .post(
            this.parentId
              ? StringExt.Format(this.endPoint.root, this.parentId)
              : this.endPoint.root,
            form.value
          )
          .subscribe(
            (data: T) => {
              this.entity = data;
              this.changeModelEvent(data);
            },
            null,
            () => {
              if (redirectToParent) {
                if (goToEditAfter) {
                  if (levelNodeBack) {
                    let levelBack: string = this.goBackLevelPoints(
                      levelNodeBack
                    );
                    this.router.navigate(
                      [`${levelBack}edit/${this.entity.id}`],
                      {
                        relativeTo: this.activateRoute.parent
                      }
                    );
                  }
                } else {
                  this.router.navigate(["."], {
                    relativeTo: this.activateRoute.parent
                  });
                }
              }

              this.notification.create("success", "Данные сохранены", {
                Position: "top-right",
                Style: "flip",
                Duration: 3000
              });

              this.onSaveComplete.emit(this.entity);
            }
          );
      }
    }
  }

  private goBackLevelPoints(level: number = 1): string {
    let commands: string = "../";
    return commands.repeat(level);
  }
}
