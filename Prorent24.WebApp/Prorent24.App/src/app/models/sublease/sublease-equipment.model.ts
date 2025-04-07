import { BaseModel } from "@models/base.model";

export class SubleaseEquipmentModel extends BaseModel {
  groupId: number;
  groupName: string;
  equipmentId: number;
  name: string;
  equipmentName: string;
  quantity: number = 1;
  price: number = 0;
  discount: number;
  factor: number;
  remark: string;
  totalPrice: number;
}
