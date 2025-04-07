import { BaseModel } from "@models/base.model";
import { BooleanEnum } from "@shared/enums/boolean.enum";

export class ContactPaymentModel extends BaseModel {
  invoiceMomentId: number;
  paymentConditionId: number;
  vatSchemeId: number;
  contactInsurancePercentage: BooleanEnum;
  insurancePercentage: number;
  rental: number;
  sales: number;
  discountRentalEquipment: number;
  discountSaleEquipment: number;
  crewDiscount: number;
  transportDiscount: number;
  totalDiscount: number;
  subhireDiscount: number;
}
