import {
  Component,
  Input,
  ViewChild,
  Output,
  EventEmitter
} from "@angular/core";
import { NgForm } from "@angular/forms";
import { ModalDirective } from "ngx-bootstrap";
import { jqxTreeComponent } from "jqwidgets-ng/jqxtree";
import { Entity } from "@shared/enums/entity.enum";
import { CustomDirectoryService } from "@services/custom-directory.service";
import { FolderModel } from "@models/common/folder.model";
import { FoldersService } from "@services/folders.service";
import { threadId } from 'worker_threads';
import { X } from '@angular/cdk/keycodes';

@Component({
  selector: "rent-tree-folders",
  templateUrl: "./tree-folders.component.html"
})
export class TreeFoldersComponent {
  @ViewChild("foldersModal", { static: true }) foldersModal: ModalDirective;
  @ViewChild("foldersTree", { static: true }) foldersTree: jqxTreeComponent;

  @Input() entityType: Entity;
  @Input() showCheckBox: boolean = false;
  @Input() position: string;
  @Output() onSelect = new EventEmitter<any>();
  select(folder: any): void {
    this.onSelect.emit(folder);
  }

  dataFolders: Array<any>;
  folders: Array<any>;
  folder: FolderModel = new FolderModel();
  selectedFolder: any = {};
  groupId: number = 0;

  editFolderMode: boolean = false;

  constructor(
    private customDirectory: CustomDirectoryService,
    private foldersService: FoldersService
  ) {
    // this.foldersTree.onItemClick[0];
  }

  showFolders(id: number = null): void {
    if (!this.folders || id > 0) {
      this.customDirectory
        .getFolders(this.entityType)
        .subscribe(data => (this.folders = data), null, () => {
          if (id > 0) {
            this.groupId = id;
            this.folders = this.folders.filter(x => x.id == id);
            this.dataFolders = this.folders;
            this.folders = this.foldersService.buildFolders(this.folders);
          }
          else {
            this.dataFolders = this.folders;
            this.folders = this.foldersService.buildFolders(this.folders);
          }
        });
    }

    this.foldersModal.show();
  }

  // submit folder
  submitFolder(form: NgForm): void {
    let response_folder: any;
    if (form.valid) {
      let selectedItem: any = this.foldersTree.getSelectedItem();
      if (selectedItem != null) {
        if (selectedItem.level > 0) {
          form.value.parentId = selectedItem.parentId;
        }
      }

      this.foldersService
        .editFolder(form.value)
        .subscribe(data => (response_folder = data), null, () => {
          let folder = {};
          if (selectedItem != null) {
            folder = { id: response_folder.id, text: response_folder.name, label: response_folder.name };
            this.folders.push(folder);
            this.editFolderMode = false;
            // this.foldersTree.addTo(
            //   folder,
            //   selectedItem.element
            // );
          } else {
            folder = { id: response_folder.id, text: response_folder.name, label: response_folder.name };
            this.folders.push(folder);
            this.editFolderMode = false;
            // this.foldersTree.addAfter(
            //   folder,
            //   null
            // );
          }



          // this.foldersTree.render();

          //Select folder after render
          // this.folder.id = folder["id"];
          // this.folder.name = folder["label"];
          // this.folder.parentId = folder["parentId"];
          // 
          // this.select(this.folder);

        });

      this.folder = new FolderModel();
    }
  }

  onSubmitChildFolder(item: FolderModel, current: any, parent: any) {
    if (item.id > 0) {
      item.id = current.id;
      item.parentId = current.parentid == -1 ? null : current.parentid;
      this.foldersService
        .editFolder(item, item.id)
        .subscribe(data => {
          if (current.parentid > 0) {
            let index = parent.items.findIndex(x => x.parentid == item.parentId);
            parent.items[index].text = data.name;
            parent.items[index].label = data.name;
            parent.items[index].edit = false;
          }
          else {
            current.text = data.name;
            current.label = data.name;
            current.edit = false;
          }
        });
    }
    else {
      let data = { name: item.name, moduleType: this.entityType, parentId: parent.id };
      this.foldersService
        .editFolder(data)
        .subscribe(data => {
          let folder = { id: data.id, text: data.name, label: data.name, parentId: parent.id };
          if (!parent.items) {
            parent.items = [];
          }
          parent.items.push(folder);
          current.childAdd = false;
        });
    }
  }

  treeFolderOnSelect(item: any, patentItem: any): void {
    item.expanded = item.expanded ? item.expanded = false : item.expanded = true;
    this.selectedFolder = item;

    if (item.parent && item.parent.parentid > -1 && item.expanded) {
      item.parent.expanded = true;
    }
  }

  onCheckLevel0(item: any, $event) {
    item.checked = item.checked ? item.checked = false : item.checked = true;
    this.selectedFolder = item;
    this.checkIncludeItems(item, item.checked);
  }

  checkIncludeItems(item: any, check: boolean) {
    if (item.items) {
      for (let i = 0; i < item.items.length; i++) {
        item.items[i].checked = check;
        this.checkIncludeItems(item.items[i], check);
      }
    }
  }

  getSelectedIds(items: any, ids: any) {
    if (items) {
      for (let i = 0; i < items.length; i++) {
        if (items[i].checked) {
          ids.push(items[i].id);
        }
        this.getSelectedIds(items[i].items, ids);
      }
    }
  }

  selectFolder(cancell): void {
    if (this.showCheckBox) {
      let ids = [];
      this.getSelectedIds(this.folders, ids);
      this.select({ group:this.groupId, ids: ids, click:cancell });
    }
    else {
      let folder = { id: this.selectedFolder.id, name: this.selectedFolder.text };
      this.select(folder);
    }

    this.foldersModal.hide();
  }


  onAddFolder(item: any) {
    item.childAdd = true;
    item.expanded = true;
    this.folder = new FolderModel();
  }

  onEditFolder(item: FolderModel, child: any) {
    child.edit = true
    this.folder.id = child ? child.id : 0;
    this.folder.name = child ? child.text : '';
    this.folder.moduleType = this.entityType;
    this.folder.parentId = child.parentid > 0 ? item.id : null;
  }

  onCancellFolder(item: any) {
    item.edit = false;
    this.folder = new FolderModel();
  }

  deleteFolder(item: any, parrentItem: any): void {
    this.foldersService.deleteFolder(item.id).subscribe(data => { }, null, () => {
      if (item.parentId > 0) {
        let index = parrentItem.items.findIndex(x => x.id == item.id);
        if (index > -1) {
          parrentItem.items.splice(index, 1);
        }
      }
      else {
        let index = this.folders.findIndex(x => x.id == item.id);
        if (index > -1) {
          this.folders.splice(index, 1);
        }
      }

      this.folder = new FolderModel();
    });
  }

  dragFolder(): void {
    let response_folder: any;

    let selectedItem: any = this.foldersTree.getSelectedItem();
    if (selectedItem != null) {
      let draggedFolder: FolderModel = new FolderModel();
      draggedFolder.id = selectedItem.id;
      draggedFolder.moduleType = this.entityType;
      draggedFolder.name = selectedItem.text
        ? selectedItem.text
        : selectedItem.label;
      if (selectedItem.level > 0) {
        draggedFolder.parentId = selectedItem.parentId;
      }

      this.foldersService
        .editFolder(draggedFolder, draggedFolder.id)
        .subscribe(data => (response_folder = data));
    }
  }

  searchFolders(dataSerch) {
    this.customDirectory
      .getFolders(this.entityType, dataSerch.target.value)
      .subscribe(data => (this.folders = data), null, () => {
        this.dataFolders = this.folders;
        this.folders = this.foldersService.buildFolders(this.folders);
      });
  }
}
