import { Injectable } from "@angular/core";
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpErrorResponse
} from "@angular/common/http";

import { Observable, throwError } from "rxjs";
import { map, catchError } from "rxjs/operators";
import { MessageService } from "@ui-components/notification/notification.service";

@Injectable()
export class HttpConfigInterceptor implements HttpInterceptor {
  constructor(private _notification: MessageService) { }
  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      map((event: HttpEvent<any>) => {
        return event;
      }),
      catchError((error: HttpErrorResponse) => {
        let data: any = {};
        data = {
          // reason: error && error.error.reason ? error.error.reason : "",
          status: error.status
        };

        let msg: string;

        switch (data.status) {
          case 400:
            msg = "Что-то пошло не так, проверьте данные и повторите это действие еще раз";
            break;
          case 403:
          case 401:
            msg = "Отказано в доступе";
            break;
          case 428:
            if (error.error.unauthorized) {
              msg = "Неверный логин или пароль";
            }
            else {
              msg = error.error;
            }
            break;
          default:
            msg = null; //"Неизвестная ошибка";
            break;
        }

        if (msg != null) {
          this._notification.create("danger", msg, {
            Position: "top-right",
            Style: "flip",
            Duration: 5000
          });

          return throwError(error);
        }
      })
    );
  }
}
