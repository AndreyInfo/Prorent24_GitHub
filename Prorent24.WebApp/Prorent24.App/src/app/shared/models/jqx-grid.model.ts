export class Column {
  text: string;
  datafield: string;
  hidden: boolean;
  columntype?: string;
  cellsformat?: string;
  editable?: boolean;
  cellsrenderer?: any;
  width?: any;
  format?: string;
  rendered?: any;
}

export class Source {
  constructor() {
    this.datafields = [];
  }
  localdata: any[];
  datatype: string;
  datafields: DataField[];
  id: string;
  hierarchy: Hierarchy;
  dataGroups:any[];
}

export class DataField {
  name: string;
  type: string;
  format?: string;
}

class Hierarchy {
  root: string;
}

export class JqxGridModel {
  constructor() {
    this.columns = [];
    this.source = new Source();
  }

  columns: Column[];
  source: Source;
}
