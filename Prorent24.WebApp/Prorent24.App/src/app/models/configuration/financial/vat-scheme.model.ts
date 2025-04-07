import { BaseModel } from "@models/base.model";
import { VatSchemeType } from '@shared/enums/vat-scheme-type.enum'
import { VatClassSchemeRate } from '@models/configuration/financial/vat-class-scheme-rate'


export class VatSchemeModel extends BaseModel {
  name: string;
  type: VatSchemeType=VatSchemeType.rates;
  vatClassSchemeRates: VatClassSchemeRate[]=[];
}
