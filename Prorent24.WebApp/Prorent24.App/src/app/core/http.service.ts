import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import "rxjs/add/operator/catch";

@Injectable({
  providedIn: "root"
})
export class HttpService {
  public host: string;

  constructor(private http: HttpClient, @Inject("BASE_URL") baseUrl: string) {
    this.host = baseUrl + "api/";
  }

  getPath(path: string): string {
    return this.host + path;
  }

  get(
    path: string,
    params?: HttpParams,
    headers: HttpHeaders = new HttpHeaders()
  ): Observable<any> {
    return this.http.get(this.host + path, {
      params: params,
      headers: headers,
      withCredentials: true
    });
  }

  getT<T>(
    path: string,
    params?: HttpParams,
    headers: HttpHeaders = new HttpHeaders()
  ): Observable<T> {
    return this.http.get<T>(this.host + path, {
      params: params,
      headers: headers,
      withCredentials: true
    });
  }

  post(
    path: string,
    body?: any,
    params?: HttpParams,
    headers: HttpHeaders = new HttpHeaders()
  ): Observable<any> {
    return this.http.post(this.host + path, body, {
      params: params,
      headers: headers,
      withCredentials: true
    });
  }

  postT<T>(
    path: string,
    body?: any,
    params?: HttpParams,
    headers: HttpHeaders = new HttpHeaders()
  ): Observable<T> {
    return this.http.post<T>(this.host + path, body, {
      params: params,
      headers: headers,
      withCredentials: true
    });
  }

  uploadFile(path: string): Observable<any> {
    return this.http.get(this.host + path, {
      responseType: "arraybuffer",
      withCredentials: true
    });
  }

  uploadFileByUrl(path: string): Observable<any> {
    return this.http.get(path, {
      responseType: "arraybuffer",
      withCredentials: true
    });
  }

  postArrayBuffer(
    path: string,
    body?: any,
    params?: HttpParams,
    headers: HttpHeaders = new HttpHeaders()
  ): Observable<any> {
    return this.http.post(this.host + path, body, {
      params: params,
      headers: headers,
      responseType: "arraybuffer",
      withCredentials: true
    });
  }
}
