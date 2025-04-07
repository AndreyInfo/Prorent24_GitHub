import { BaseModel } from "@models/base.model";
import { ProjectStatus } from "@shared/enums/project-status.enum";
import { ProjectTime } from "./project-time.model";
import { ContactModel } from "@models/contacts/contact.model";

export class ProjectModel extends BaseModel {
	name: string;
	number: string;
	status: ProjectStatus = ProjectStatus.Option;
	typeId: number;
	color: string;
	reference: string;
	accountManagerId: string;
	clientContactId: number;
	clientContactPersonId: number;
	locationContactId?: number;
	locationContactPersonId?: number;
	
	times: ProjectTime[] = [];

	clientContact: ContactModel = new ContactModel();
	locationContact: ContactModel = new ContactModel();
	clientContactPerson: ContactModel = new ContactModel();
	locationContactPerson: ContactModel = new ContactModel();

	totalPrice?: number;

	description: string;
	isPublic: boolean;
	crewMembers: Array<any>;
}
