export class FinancialModel {
    currencySign: string;
    defaultExpirationPeriodQuotationContract: number;
    defaultDueTermInvoiceReminders: number;
    defaultVatSchemeId: number;
    defaultVatClassId: number;
    defaultTaxClassCrewFunctionId: number;
    defaultTaxClassTransportFunctionId: number;
    vatClassInsuranceId: number;

    bankName: string;
    bankAccountNumber: string;
    bankNumberBic: string;

    printOnLetterhead: boolean;
    sendTermsConditionsWithQuotations: boolean;
    sendTermsConditionsWithContracts: boolean;
    sendTermsConditionsWithInvoices: boolean;

    invoiceLineTotal: number = 0;
    companyTypeId: number;
}
