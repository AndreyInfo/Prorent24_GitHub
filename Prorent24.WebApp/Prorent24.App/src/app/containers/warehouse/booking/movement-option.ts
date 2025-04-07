import { BookingEquipment } from "@models/warehouse/booking.model";

export class MovementOptions {
  index: number;
  from: string;
  to: string;
  newState: any;
  equipment: BookingEquipment;
  kit?: BookingEquipment;
  kitIndex?: number;
}
