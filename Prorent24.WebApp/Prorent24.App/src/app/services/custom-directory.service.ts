import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map, share } from "rxjs/operators";

import { HttpService } from "@core/http.service";
import {
	ConfigFinancialVatClassesEndpoints,
	ConfigFinancialVatSchemesEndpoints,
	ConfigFinancialDiscountGroupsEndpoints,
	ConfigFinancialPaymentMethodsEndpoints,
	ConfigFinancialFactorGroupsEndpoints,
	ConfigFinancialInvoiceMomentsEndpoints,
	ConfigFinancialPaymentConditionsEndpoints,
	ConfigAccountUserRolesEndpoints,
	ConfigCommunicationDocTemplatesEndpoints,
	ConfigCommunicationBlanksEndpoints,
	ConfigFinancialConditionsEndpoints,
	ConfigCommunicationSalutationsEndpoints,
	ConfigSettingsInspectionEndpoints,
	StaffRatesEndpoints,
	ContactsEndpoints,
	FoldersEndpoints,
	ContactPersonsEndpoints,
	StaffEndpoints,
	ConfigSettingsProjectTypesEndpoints,
	ConfigFinancialLedgersEndpoints,
	ConfigFinancialSettingsEndpoints
} from "@endpoints";
import { StringExt } from "@shared/utils/string.extension";
import { Entity } from "@shared/enums/entity.enum";
import { DocumentTemplateTypeEnum } from "@shared/enums/document-template-type.enum";

@Injectable({
	providedIn: "root"
})
export class CustomDirectoryService {
	constructor(private http: HttpService) {}

	getVatClasses(): Observable<any> {
		return this.http.get(ConfigFinancialVatClassesEndpoints.root).pipe(
			map(data => {
				return data.grid.data;
			}, share())
		);
	}

	getVatSchemes(): Observable<any> {
		return this.http.get(ConfigFinancialVatSchemesEndpoints.root).pipe(
			map(data => {
				return data.grid.data;
			})
		);
	}

	getLedgers(): Observable<any> {
		return this.http.get(ConfigFinancialLedgersEndpoints.root).pipe(
			map(data => {
				return data.grid.data;
			})
		);
	}
	getDiscountGroups(): Observable<any> {
		return this.http.get(ConfigFinancialDiscountGroupsEndpoints.root).pipe(
			map(data => {
				return data.grid.data;
			})
		);
	}

	getFactorGroups(): Observable<any> {
		return this.http.get(ConfigFinancialFactorGroupsEndpoints.root).pipe(
			map(data => {
				return data.grid.data;
			})
		);
	}

	getInspectionType(): Observable<any> {
		return this.http.get(ConfigSettingsInspectionEndpoints.root).pipe(
			map(data => {
				return data.grid.data;
			})
		);
	}

	getPaymentMethods(): Observable<any> {
		return this.http.get(ConfigFinancialPaymentMethodsEndpoints.root).pipe(
			map(data => {
				return data.grid.data;
			})
		);
	}

	getPaymentConditions(): Observable<any> {
		return this.http.get(ConfigFinancialPaymentConditionsEndpoints.root).pipe(
			map(data => {
				return data.grid.data;
			})
		);
	}

	getInvoiceMoments(): Observable<any> {
		return this.http.get(ConfigFinancialInvoiceMomentsEndpoints.root).pipe(
			map(data => {
				return data.grid.data;
			})
		);
	}

	getProjectTypes(): Observable<any> {
		return this.http.getT<any>(ConfigSettingsProjectTypesEndpoints.root).pipe(
			map(data => {
				return data.grid.data;
			})
		);
	}

	getDocumentTemplates(): Observable<any> {
		return this.http
			.getT<any>(ConfigCommunicationDocTemplatesEndpoints.root)
			.pipe(
				map(data => {
					return data.grid.data;
				})
			);
	}

	getDocumentTemplatesByType(
		documentTemplateType: DocumentTemplateTypeEnum
	): Observable<any> {
		return this.http
			.getT<any>(
				StringExt.Format(
					ConfigCommunicationDocTemplatesEndpoints.list,
					documentTemplateType
				)
			)
			.pipe(
				map(data => {
					return data;
				})
			);
	}

	getLetterheads(): Observable<any> {
		return this.http.getT<any>(ConfigCommunicationBlanksEndpoints.root).pipe(
			map(data => {
				return data.grid.data;
			})
		);
	}

	getSalutations(): Observable<any> {
		return this.http
			.getT<any>(ConfigCommunicationSalutationsEndpoints.root)
			.pipe(
				map(data => {
					return data.grid.data;
				})
			);
	}

	getAdditionalConditions(): Observable<any> {
		return this.http.getT<any>(ConfigFinancialConditionsEndpoints.root).pipe(
			map(data => {
				return data.grid.data;
			})
		);
	}

	getCrewMembers(): Observable<any> {
		return this.http.get(StringExt.Format(StaffEndpoints.root)).pipe(
			map(data => {
				return data.grid.data;
			})
		);
	}

	getCrewMemberRates(crewMemberId: string): Observable<any> {
		return this.http
			.get(StringExt.Format(StaffRatesEndpoints.root, crewMemberId))
			.pipe(
				map(data => {
					return data.grid.data;
				})
			);
	}

	getUserRoles(): Observable<any> {
		return this.http.get(ConfigAccountUserRolesEndpoints.root).pipe(
			map(data => {
				return data.grid.data;
			})
		);
	}

	getFolders(entityType: Entity,search:string=''): Observable<any> {
		return this.http.get(StringExt.Format(FoldersEndpoints.part, entityType,search));
	}

	getContacts(): Observable<any> {
		return this.http.get(ContactsEndpoints.root).pipe(
			map(data => {
				return data.grid.data;
			})
		);
	}

	getContactPersons(contactId: any): Observable<any> {
		return this.http
			.get(StringExt.Format(ContactPersonsEndpoints.root, contactId))
			.pipe(
				map(data => {
					return data.grid.data;
				})
			);
	}

	getFinancialSetting(): Observable<any> {
		return this.http.get(ConfigFinancialSettingsEndpoints.root).pipe(
			map(data => {
				return data;
			})
		);
	}
}
