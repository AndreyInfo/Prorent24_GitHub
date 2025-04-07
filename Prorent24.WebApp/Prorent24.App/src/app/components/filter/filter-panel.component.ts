import {
  Component,
  OnInit,
  Input,
  ViewChild,
  TemplateRef,
  EventEmitter,
  Output
} from "@angular/core";
import { NgForm } from "@angular/forms";
import { HttpService } from "@core/http.service";
import { GeneralFiltersEndpoints, GeneralPresetsEndpoints } from "@endpoints";
import { StringExt } from "@shared/utils/string.extension";
import { Entity } from "@shared/enums/entity.enum";
import { FilterType } from "@shared/enums/filter-type.enum";
import { jqxTreeComponent } from "jqwidgets-ng/jqxtree";
import { FoldersService } from "@services/folders.service";
import { FilterModel } from "@models/common/filter.model";
import { PresetModel } from "@models/common/preset.model";
import { FilterPeriod } from "@models/common/filter-period.model";
import { TagService } from "@services/tag.service";
import { take } from "rxjs/operators";
import { TreeFoldersComponent } from "@components/tree-folders/tree-folders.component";

@Component({
  selector: "rent-filter-panel",
  templateUrl: "./filter-panel.component.html"
})
export class FilterPanelComponent implements OnInit {
  @Input() entityType: Entity;
  @Input() subEntity: string = "";
  @Input() styleTag: string = "";

  @Output() onChanged = new EventEmitter<Array<any>>();
  changeFilter(filter: Array<any>): void {
    this.onChanged.emit(filter);
  }

  @ViewChild("filterFoldersTree", { static: false })
  filterFoldersTree: jqxTreeComponent;
  @ViewChild("presetsTemplate", { static: true }) presetsTemplate: TemplateRef<
    any
  >;
  @ViewChild("foldersTemplate", { static: true }) foldersTemplate: TemplateRef<
    any
  >;
  @ViewChild(TreeFoldersComponent, { static: true }) foldersModal: TreeFoldersComponent;
  @ViewChild("tagsTemplate", { static: true }) tagsTemplate: TemplateRef<any>;
  @ViewChild("periodTemplate", { static: true }) periodTemplate: TemplateRef<
    any
  >;
  @ViewChild("filtersTemplate", { static: true }) filtersTemplate: TemplateRef<
    any
  >;

  defaultFilter: any;
  searchText: string;
  filters: Array<FilterModel>;
  folders: any;
  periods: Array<any>;
  selectedFilter: Array<FilterModel> = new Array<FilterModel>();
  additionalFolderGroups: any[] = [];

  preset: PresetModel = new PresetModel();
  formPresetMode: boolean = false;
  editPresetMode: boolean = false;

  constructor(
    private http: HttpService,
    private foldersService: FoldersService,
    private tagService: TagService
  ) { }

  ngOnInit(): void {
    this.http
      .get(
        StringExt.Format(GeneralFiltersEndpoints.single, this.entityType) +
        this.subEntity
      )
      .subscribe(
        data => {
          this.filters = data;
          this.defaultFilter = data;
        },
        null,
        () => {
          this.buildFilters(this.filters);
        }
      );

    this.tagService.onPush.pipe(take(1)).subscribe((data: any) => {
      let _tagFilter: any = this.filters.find(
        (f: any) => f.filterType === "Tags"
      );

      if (_tagFilter) {
        let isExistTag = _tagFilter.data.find(
          (f: any) => f.id === data.directoryId
        );
        if (!isExistTag) {
          _tagFilter.data.push(data);
        }
      }
    });
  }

  buildFilters(filters: any): void {
    this.initFilter(filters);
    if (filters.some((e: any) => e.filterType === FilterType.folders)) {
      this.folders = this.foldersService.buildFolders(
        filters.find((f: any) => f.filterType === FilterType.folders).data
      );
    }
  }

  loadTemplate(filterType: any): any {
    switch (filterType) {
      case FilterType.presets:
        return this.presetsTemplate;
      case FilterType.folders:
        return this.foldersTemplate;
      case FilterType.tags:
        return this.tagsTemplate;
      case FilterType.period:
        return this.periodTemplate;
      case FilterType.addedFilters:
        return this.filtersTemplate;
      default:
        return null;
    }
  }

  // on filters change
  filterChange(): void {
    this.selectedFilter.forEach(element => {
      switch (element.filterType) {
        case FilterType.searchText:
          element.values = [this.searchText];
          break;
        case FilterType.tags:
          let tags_values: any = this.filters
            .find((f: any) => f.filterType === FilterType.tags)
            .data.reduce((ids: any, obj: any) => {
              if (obj.selected === String(true)) {
                ids.push(obj.id);
              }
              return ids;
            }, []);
          element.values = tags_values;
          break;
        case FilterType.folders:
          element.values = this.folders.filter(x => x.checked).map(v => {
            return v["id"];
          });

          if (this.additionalFolderGroups && this.additionalFolderGroups.length > 0) {
            for (let g = 0; g < this.additionalFolderGroups.length; g++) {
              for (let i = 0; i < this.additionalFolderGroups[g].ids.length; i++) {
                element.values.push(this.additionalFolderGroups[g].ids[i]);
              }
            }
          }
          break;
        case FilterType.period:
          element.values = this.periods;
        default:
          break;
      }
    });

    this.changeFilter(this.selectedFilter);
  }

  changePeriod(periods: FilterPeriod): void {
    this.periods = new Array<any>();
    this.periods.push(periods);
    this.filterChange();
  }

  submitPreset(form: NgForm): void {
    if (form.valid) {
      if (form.value.id) {
        this.http
          .post(
            StringExt.Format(GeneralPresetsEndpoints.single, form.value.id),
            form.value
          )
          .subscribe();
      } else {
        this.preset.moduleType = this.entityType;
        this.preset.filters = this.selectedFilter;

        let new_preset: any;
        this.http
          .post(GeneralPresetsEndpoints.root, this.preset)
          .subscribe(data => (new_preset = data), null, () => {
            this.filters
              .find((f: FilterModel) => f.filterType === FilterType.presets)
              .data.push(new_preset);

            this.preset = new PresetModel();
            this.formPresetMode = false;
          });
      }
    }
  }

  deletePreset(id: any): void {
    this.http
      .post(StringExt.Format(GeneralPresetsEndpoints.delete, id))
      .subscribe(null, null, () => {
        let array: any = this.filters.find(
          (f: FilterModel) => f.filterType === FilterType.presets
        ).data;

        array.splice(array.findIndex(f => f.id === id), 1);
      });
  }

  changePreset(preset: PresetModel): void {
    if (preset.isDefault) {
      this.buildFilters(this.defaultFilter);
      let emptyFilter: Array<FilterModel> = new Array<FilterModel>();
      this.changeFilter(emptyFilter);
    } else {
      this.http
        .get(StringExt.Format(GeneralPresetsEndpoints.single, preset.id))
        .subscribe(data => (this.filters = data), null, () => {
          this.filters
            .find((f: FilterModel) => f.filterType === FilterType.tags)
            .data.forEach(i => {
              i.selected = String(i.selected);
            });

          this.buildFilters(this.filters);
          this.filterChange();
        });
    }
  }

  showFoldersModal(id): void {
    this.foldersModal.showFolders(id);
  }

  existsGroup(item){
    for(let i =0; i < this.additionalFolderGroups.length; i++){
      if(this.additionalFolderGroups[i].group == item.id){
        item.checked = true;
      }
      else{
        item.checked = false;
      }
    }
   
    return true;
  }

  setIdsFolders(data) {
    if(data.click){
      let index = this.additionalFolderGroups.find(x => x.group == data.group);
      if (index > -1) {
        this.additionalFolderGroups[index].ids = data.ids;
      }
      else {
        this.additionalFolderGroups.push(data);
      }
    }
    else{
      let index = this.additionalFolderGroups.findIndex(x => x.group == data.group);
      this.additionalFolderGroups.splice(index,1);
      let indexFolder = this.folders.findIndex(x => x.id == data.group);
      this.folders[indexFolder].checked = false;
    }

    this.filterChange();
  }

  // init empty filter
  private initFilter(data: any): void {
    this.selectedFilter = new Array<FilterModel>();
    data.forEach((element: any) => {
      let f: FilterModel = new FilterModel();
      f.filterType = element.filterType;
      f.values = new Array<any>();
      this.selectedFilter.push(f);
    });
  }
}
