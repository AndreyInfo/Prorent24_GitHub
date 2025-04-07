import { BaseModel } from "@models/base.model";

export class ProjectEquipmentModel extends BaseModel {
  groupName: string;
  groupId: number;
  equipmentId: number;
  equipmentName: string;
  name: string;
  quantity: number = 1;
  price: number = 0;
  discount: number = 0;
  factor: number = 0;
  vatClassId: number;
  remark: string;
  totalPrice: number;
}
