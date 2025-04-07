import { ShedulerEventAction } from '@shared/enums/sheduler-event-action.enum';

export class CalendarModel {
  id: number;
  action: string;
  start: Date;
  end: Date;
  subject: string;
  description: string;
  comment: string;

  isAvailable: boolean;
  color: string;
  background: string;
  borderColor: string;

  resizable: boolean = false;
  draggable: boolean = false;
}
