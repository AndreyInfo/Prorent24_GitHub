import { BaseModel } from "@models/base.model";
import { VatSchemeType } from '@shared/enums/vat-scheme-type.enum'
import { VatClassModel } from '@models/configuration/financial/vat-class.model'

export class VatClassSchemeRate extends BaseModel {
    type: VatSchemeType;
    vatClassId: number;
    vatClass: VatClassModel = new VatClassModel();
    vatSchemeId: number;
    rate: number;
    accountingCode: string;
    edifactCode: string;
}
