import { BaseModel } from "@models/base.model";

export class BlankModel extends BaseModel {
  name: string;
  pageSize: string;
  pageWidth: number;
  pageHeight: number;
  topMargin: number;
  rightMargin: number;
  bottomMargin: number;
  leftMargin: number;
  pageNumbers: boolean;
  showAtInvoices: boolean;
  showAtQuotations: boolean;
  displayAtContracts: boolean;
  showAtSubhireSlips: boolean;
  showAtReminders: boolean;
  showAtCrewMemberSlips: boolean;
  showAtTransportSlips: boolean;
  showOnEquipmentSlips: boolean;
  showAtRepairSlips: boolean;
  showAtOtherSlips: boolean;
}
