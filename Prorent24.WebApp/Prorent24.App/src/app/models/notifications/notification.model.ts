import { BaseModel } from "@models/base.model";

export class NotificationModel extends BaseModel {
  theme: string;
  message: string;
  isRead: boolean;
  entityId: number;
  moduleType: string;
  creationDate: Date;
}
