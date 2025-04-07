import { Injectable, Inject, PLATFORM_ID } from "@angular/core";
import { isPlatformBrowser } from "@angular/common";

export class CookieOption {
  expires: number;
  path: string;
}

@Injectable({
  providedIn: "root"
})
export class CookieStorage {
  options: CookieOption = new CookieOption();

  constructor(@Inject(PLATFORM_ID) private platform: Object) {
    this.options.expires = 1296000;
    this.options.path = "/";
  }

  public read(key: string): any {
    if (isPlatformBrowser(this.platform)) {
      let value: string = this.getCookie(key);
      return value;
    }

    return;
  }

  public write(key: string, value: any): void {
    if (isPlatformBrowser(this.platform)) {
      this.setCookie(key, value, this.options);
    }
  }

  public removeAll(): void {
    var cookies: string[] = document.cookie.split(";");
    for (var i: number = 0; i < cookies.length; i++) {
      var cookie: string = cookies[i];
      var eqPos: number = cookie.indexOf("=");
      var name: string = eqPos > -1 ? cookie.substr(0, eqPos) : cookie;
      document.cookie =
        name +
        "=;" +
        "expires=Thu, 01-Jan-1970 00:00:01 GMT;" +
        "path=" +
        "/;" +
        "domain=" +
        window.location.host +
        ";" +
        "secure=;";
    }
  }

  private getCookie(name: string): any {
    var matches: any = document.cookie.match(
      new RegExp(
        "(?:^|; )" +
          name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, "\\$1") +
          "=([^;]*)"
      )
    );
    return matches ? decodeURIComponent(matches[1]) : undefined;
  }

  private setCookie(name: string, value: any, options: any): void {
    options = options || {};

    var expires: any = options.expires;

    if (typeof expires === "number" && expires) {
      var d: Date = new Date();
      d.setTime(d.getTime() + expires * 1000);
      expires = options.expires = d;
    }
    if (expires && expires.toUTCString) {
      options.expires = expires.toUTCString();
    }

    value = encodeURIComponent(value);

    var updatedCookie: string = name + "=" + value;

    for (var propName in options) {
      updatedCookie += "; " + propName;
      var propValue: any = options[propName];
      if (propValue !== true) {
        updatedCookie += "=" + propValue;
      }
    }

    document.cookie = updatedCookie;
  }
}
