import { Injectable } from "@angular/core";
import { DayPilot } from "daypilot-pro-angular";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable()
export class ShedulerDataService {

  constructor(private http: HttpClient) {}

  getEvents(from: DayPilot.Date, to: DayPilot.Date): Observable<any[]> {
    // simulating an HTTP request
    return new Observable(observer => {
      setTimeout(() => {
        observer.next(this.events);
      }, 1);
    });

    // return this.http.get("/api/events?from=" + from.toString() + "&to=" + to.toString());
  }

  getResources(): Observable<any[]> {
    // simulating an HTTP request
    return new Observable(observer => {
      setTimeout(() => {
        observer.next(this.resources);
      }, 1);
    });

    // return this.http.get("/api/resources");
  }

  resources: any[] = [
    {
      name: "Live Performance",
      id: "GA",
      expanded: true,
      children: [
        { name: "Planning", id: "R1" },
        { name: "Invitations", id: "R2" }
      ]
    },
    {
      name: "EMF Light Show",
      id: "GB",
      expanded: true,
      children: [
        { name: "Planning", id: "R3", unavailable: true },
        { name: "Invitations", id: "R4" }
      ]
    }
  ];

  events: any[] = [
    {
      id: "1",
      resource: "R1",
      start: "2018-05-03",
      end: "2018-05-08",
      text: "Scheduler Event 1",
      color: "#e69138"
    },
    {
      id: "2",
      resource: "R2",
      start: "2019-06-03",
      end: "2019-06-04",
      text: "Scheduler Event 2",
      color: "#6aa84f"
    },
    {
      id: "3",
      resource: "R2",
      start: "2019-06-01T10:00:00",
      end: "2019-06-02T12:00:00",
      text: "Scheduler Event 3",
      color: "#fd7400"
    },
    {
      id: "4",
      resource: "R2",
      start: "2019-06-01T12:00:00",
      end: "2019-07-02T12:00:00",
      text: "Builder",
      color: "#6aa84f"
    },
    {
      id: "6",
      resource: "R2",
      start: "2019-06-01",
      end: "2019-06-02",
      text: "Setup",
      color: "#6d5cae"
    }
  ];
}
