class Grid {
  constructor() {
    this.columns = new Array<Column>();
  }
  columns: Array<Column>;
  hierarchy: Hierarchy;
  data: any[];
  dataGroups:any[];
}

class Column {
  key: string;
  type: string;
  name: string;
  isDisplayName: boolean;
  order: number;
  isEntity: boolean;
  width?: any;
}

class Hierarchy {
  root: string;
}

export class DynamicTable {
  constructor() {
    this.grid = new Grid();
  }

  grid: Grid;
  entity: number;
}
