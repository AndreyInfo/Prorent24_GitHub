import { SetType } from "@shared/enums/set-type.enum";

export class BookingItemModel {
  groupId: number;
  groupName: string;
  equipments: Array<BookingEquipment> = new Array<BookingEquipment>();
  movementStatus: string;
}

export class KitCaseEquipments {
  id: number;
  groupId: number;
  equipmentId: number;
  equipmentType: SetType;
  equipmentName: string;
  movementStatus: string;
  selectedQuantity: number;
  totalQuantity: number;
  limitQuantity: number;
}

export class BookingEquipment {
  id: number;
  groupId: number;
  equipmentId: number;
  equipmentType: SetType;
  equipmentName: string;
  movementStatus: string;
  selectedQuantity: number;
  totalQuantity: number;
  limitQuantity: number;
  kitCaseEquipments: Array<KitCaseEquipments> = new Array<KitCaseEquipments>();
  package: string;
  serialNumbers:string[]=[];
}

export class BookingPartModel {
  constructor() {
    this.packed = new Array<BookingItemModel>();
  }
  pack: Array<BookingItemModel> = new Array<BookingItemModel>();
  packed: Array<BookingItemModel> = new Array<BookingItemModel>();
  transportation: Array<BookingItemModel> = new Array<BookingItemModel>();
  mounting: Array<BookingItemModel> = new Array<BookingItemModel>();
  inUse: Array<BookingItemModel> = new Array<BookingItemModel>();
  dismantling: Array<BookingItemModel> = new Array<BookingItemModel>();
  returned: Array<BookingItemModel> = new Array<BookingItemModel>();
  orderIsOver: Array<BookingItemModel> = new Array<BookingItemModel>();
}

export class BookingModel {
  data: BookingPartModel = new BookingPartModel();
  projectName: string;
}
