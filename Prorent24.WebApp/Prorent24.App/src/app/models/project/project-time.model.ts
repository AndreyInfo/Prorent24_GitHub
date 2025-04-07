export class ProjectTime {
  id: number;
  projectId: number;
  title: string;
  name: string;
  from: Date = null;
  until: Date = null;
  displayQuotation: boolean;
  displayContract: boolean;
  displayPackSlip: boolean;
  defaultUsagePeriod: boolean;
  defaultPlanPeriod: boolean;
}
