import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { share } from "rxjs/operators";

import { HttpService } from "@core/http.service";
import { DirectoryEndpoints } from "@endpoints";

@Injectable({
  providedIn: "root"
})
export class DirectoryService {
  constructor(private http: HttpService) {}

  getLanguage(): Observable<any> {
    return this.http.get(DirectoryEndpoints.language);
  }

  getCurrency(): Observable<any> {
    return this.http.get(DirectoryEndpoints.currency);
  }

  getTimeZone(): Observable<any> {
    return this.http.get(DirectoryEndpoints.timeZone).pipe(share());
  }

  getCountries(): Observable<any> {
    return this.http.get(DirectoryEndpoints.country).pipe(share());
  }

  getCompanyTypes(): Observable<any> {
    return this.http.get(DirectoryEndpoints.companyType).pipe(share());
  }

  getMeasuringUnits(): Observable<any> {
    return this.http.get(DirectoryEndpoints.measuringUnits).pipe(share());
  }
}
