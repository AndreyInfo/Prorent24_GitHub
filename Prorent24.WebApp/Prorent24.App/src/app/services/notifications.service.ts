import { Injectable } from "@angular/core";
import { HttpService } from "@core/http.service";
import { Observable } from "rxjs";
import { NotificationsEndpoints } from "@endpoints";
import { StringExt } from '@shared/utils/string.extension';

@Injectable()
export class NotificationsService {
  constructor(private http: HttpService) {}

  getLastNotifications(): Observable<any[]> {
    return this.http.get(NotificationsEndpoints.list);
  }

  setAsRead(id: any): Observable<any[]> {
    return this.http.post(StringExt.Format(NotificationsEndpoints.single, id));
  }
}
