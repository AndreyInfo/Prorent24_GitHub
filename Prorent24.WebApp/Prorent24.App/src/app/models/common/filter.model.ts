import { FilterType } from "@shared/enums/filter-type.enum";

export class FilterModel {
  filterType: FilterType;
  values: Array<any>;
  data: Array<any>;
}

