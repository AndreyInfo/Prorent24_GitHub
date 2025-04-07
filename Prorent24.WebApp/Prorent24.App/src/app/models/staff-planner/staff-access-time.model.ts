import { ShedulerEventAction } from "@shared/enums/sheduler-event-action.enum";
import { ProjectFunctionType } from "@shared/enums/project-function-type.enum";

export class StaffAccessTimeModel {
  constructor() {
    this.functionIds = []
  }
  
  id: number;
  type: ProjectFunctionType;
  functionIds: string[];
  action: ShedulerEventAction = ShedulerEventAction.appointment;
  from: Date;
  until: Date;
  description: string;
  comment: string;
}
