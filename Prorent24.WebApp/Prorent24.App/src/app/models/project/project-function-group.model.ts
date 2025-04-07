import { BaseModel } from "@models/base.model";

export class ProjectFunctionGroupModel extends BaseModel {
    name: string;
    groupName: string;
    planFrom: Date = null;
    planUntil: Date = null;
    useFrom: Date = null;
    useUntil: Date = null;
}
