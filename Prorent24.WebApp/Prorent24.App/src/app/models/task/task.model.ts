import { BaseModel } from "@models/base.model";

export class TaskModel extends BaseModel {
  name: string;
  author: string;
  CompletedBy: string;
  deadLine: Date = null;
  isPublic: boolean = true;
  executors: Array<any>;
  crewMembers: Array<any>;
  description: string;
  belongsTo: string;
  tags: Array<any>;
  completedIn: Date = null;
  expiredTime: boolean;
  crewMemberId: string;
  contactId: number;
  vehicleId: number;
}
