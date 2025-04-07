import { Injectable } from "@angular/core";
import { HttpService } from "@core/http.service";
import { StringExt } from "@shared/utils/string.extension";
import { SearchEndpoints } from "@endpoints";
import { Observable } from "rxjs";
import { share } from "rxjs/operators";

@Injectable()
export class SearchService {
  constructor(private http: HttpService) {}

  users(str: string): Observable<Array<any>> {
    return this.http.get(StringExt.Format(SearchEndpoints.users, str));
  }

  contacts(str: string): Observable<Array<any>> {
    return this.http.get(StringExt.Format(SearchEndpoints.contacts, str));
  }

  contact_persons(str: string): Observable<Array<any>> {
    return this.http.get(StringExt.Format(SearchEndpoints.contact_persons, str));
  }

  equipments(str: string): Observable<Array<any>> {
    return this.http.get(StringExt.Format(SearchEndpoints.equipments, str));
  }

  tags(entityType: any, str: string): Observable<Array<any>> {
    return this.http
      .get(StringExt.Format(SearchEndpoints.tags, entityType, str))
      .pipe(share());
  }
}
