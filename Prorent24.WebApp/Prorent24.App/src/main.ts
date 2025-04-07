import { enableProdMode } from "@angular/core";
import { platformBrowserDynamic } from "@angular/platform-browser-dynamic";
import "hammerjs";

import { AppModule } from "./app/app.module";
import { environment } from "./environments/environment";

if (environment.production) {
  enableProdMode();
}

export function getBaseUrl(): string {
  return document.getElementsByTagName("base")[0].href;
}

const providers: Array<any> = [
  { provide: "BASE_URL", useFactory: getBaseUrl, deps: [] }
];

platformBrowserDynamic(providers)
  .bootstrapModule(AppModule)
  .catch(err => console.error(err));
