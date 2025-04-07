import { Component, OnInit, ViewChild, Input } from "@angular/core";
import { NgForm } from "@angular/forms";
import { HttpService } from "@core/http.service";
import { StringExt } from "@shared/utils/string.extension";
import { GeneralNotesEndpoints } from "@endpoints";
import { ModalDirective } from "ngx-bootstrap";
import { NoteModel } from "@models/common/note.model";
import { Entity } from "@shared/enums/entity.enum";
import { ConfidentialType } from "@shared/enums/confidential-type.enum";

@Component({
  selector: "rent-widget-notes",
  templateUrl: "./widget-notes.component.html"
})
export class WidgetNotesComponent implements OnInit {
  @ViewChild("noteModal", { static: true }) noteModal: ModalDirective;

  @Input() entityId: any;
  @Input() entityType: Entity;
  @Input() data: Array<any>;

  note: NoteModel = new NoteModel();
  confidentialType = ConfidentialType;

  constructor(
    private http: HttpService,
  ) {}

  ngOnInit(): void {}

  submitNote(form: NgForm): void {
    if (form.valid) {
      if (form.value.id) {
        let updateNote: any;
        this.http
          .post(
            StringExt.Format(GeneralNotesEndpoints.single, form.value.id),
            form.value
          )
          .subscribe(data => (updateNote = data), null, () => {
            let noteIndex: number = this.data.findIndex(
              f => f.id === form.value.id
            );
            this.data[noteIndex] = updateNote;
            this.noteModal.hide();
          });
      } else {
        this.http
          .post(
            StringExt.Format(
              GeneralNotesEndpoints.root,
              this.entityType,
              this.entityId
            ),
            form.value
          )
          .subscribe(data => this.data.push(data), null, () => {
            this.noteModal.hide();
          });
      }
    }
  }

  editNote(note?: any): void {
    if (note) {
      this.note = Object.assign({}, note);
    } else {
      this.note = new NoteModel();
    }

    this.noteModal.show();
  }

  deleteNote(id: any): void {
    this.http
      .post(StringExt.Format(GeneralNotesEndpoints.delete, id))
      .subscribe(null, null, () => {
        let index: number = this.data.findIndex(f => f.id === id);
        this.data.splice(index, 1);
        this.noteModal.hide();
      });
  }
}
