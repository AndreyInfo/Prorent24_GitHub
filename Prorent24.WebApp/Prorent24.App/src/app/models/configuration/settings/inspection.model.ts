import { BaseModel } from "@models/base.model";

export class InspectionModel extends BaseModel {
    name: string;
    description: string;
    frequencyInterval: number;
    frequencyUnitType: string;
}
