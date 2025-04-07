export enum FilterType {
  default = 0,
  presets = 1,
  folders = 2,
  tags = 3,
  addedFilters = 4,
  period = 5,
  searchText = 6
}

export class GridFilter {
  filterType: FilterType;
  selectedAll: boolean;
  ids: Array<any>;
}
