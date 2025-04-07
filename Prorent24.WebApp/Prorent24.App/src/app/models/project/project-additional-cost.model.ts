import { BaseModel } from "@models/base.model";

export class ProjectAdditionaCost extends BaseModel {
    projectId: number;
    name: string;
    purchasePrice: number = 0;
    salePrice: number = 0;
    vatClassId: number;
    details: string;
}
