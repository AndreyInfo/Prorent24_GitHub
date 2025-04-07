import { Injectable, Inject, PLATFORM_ID } from "@angular/core";
import { isPlatformBrowser } from "@angular/common";

@Injectable({
  providedIn: "root"
})
export class LocalStorage {
  constructor(@Inject(PLATFORM_ID) private platform: Object) {}

  read(key: string): string {
    if (isPlatformBrowser(this.platform)) {
      let value: string = localStorage.getItem(key);
      return value;
    }
    return;
  }

  write(key: string, value: string): void {
    if (isPlatformBrowser(this.platform)) {
      localStorage.setItem(key, value);
    }
  }

  remove(key: string): void {
    if (isPlatformBrowser(this.platform)) {
      localStorage.removeItem(key);
    }
  }
}
