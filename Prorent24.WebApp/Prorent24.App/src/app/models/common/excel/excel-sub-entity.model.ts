export class ExcelSubEntity{
    groupName: string;
    collection: ExcelSubEntityColumn[];
}

export class ExcelSubEntityColumn{
    id: number;
    text: string;
    value: string;
    type: string;
}