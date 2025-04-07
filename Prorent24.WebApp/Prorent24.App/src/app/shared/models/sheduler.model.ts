export class ShedulerModel {
  resources: Array<Resource> = new Array<Resource>();
  events: Array<EventModel> = new Array<EventModel>();
}

export class Availability {
  start: Date;
  end: Date;
  type: string;
  color: string;
}

export class Resource {
  id: string;
  name: string;
  expanded: boolean = true;
  children: Array<ResourceChild> = new Array<ResourceChild>();
  availabilities: Array<Availability>;
}

export class ResourceChild {
  id: string;
  name: string;
  unavailable: boolean;
  availabilities: Array<Availability>;
}

export class EventModel {
  id: string;
  resource: string;
  start: Date;
  end: Date;
  text: string;
  temp_text: string;
  barColor: string;
  backColor: string;
  fontColor: string;

  plannedQuantity: number;
  isAvailability: boolean;
}
