import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { NgModule, ErrorHandler, LOCALE_ID } from "@angular/core";
import {
  HttpClient,
  HttpClientModule,
  HTTP_INTERCEPTORS
} from "@angular/common/http";

// import ngx-translate and the http loader
import { TranslateLoader, TranslateModule } from "@ngx-translate/core";
import { TranslateHttpLoader } from "@ngx-translate/http-loader";

// modules
import { AppRoutingModule } from "./app-routing.module";
import { PerfectScrollbarModule } from "ngx-perfect-scrollbar";
import { SharedModule } from "@shared/shared.module";
import { RootStoreModule } from "./store";

import { NotificationModule } from "@ui-components/notification/notification.module";

// basic bootstrap modules
import {
  BsDropdownModule,
  AccordionModule,
  AlertModule,
  ButtonsModule,
  CollapseModule,
  ModalModule,
  ProgressbarModule,
  TabsModule,
  TypeaheadModule
} from "ngx-bootstrap";

import { ContextMenuModule } from 'ngx-contextmenu';
import { MatTabsModule } from "@angular/material/tabs";
import { MatTooltipModule } from "@angular/material/tooltip";
import { TooltipModule } from 'ng2-tooltip-directive';

// middleware
import { AuthenticatedGuard } from "@core/authentication.guard";
import { SystemEmptyGuard } from "@core/system.empty.guard";

// components
import { AppComponent } from "./app.component";

import {
  RootLayout,
  BlankComponent,
  CondensedComponent,
  HeaderComponent,
  MenuComponent,
  MenuAltComponent,
  MenuIconComponent,
  SidebarComponent
} from "./layout";

// services
import { HttpService } from "@core/http.service";
import { HubService } from "@services/hub.service";
import { PagesToggleService } from "@shared/utils/toggler.service";

import {
  PERFECT_SCROLLBAR_CONFIG,
  PerfectScrollbarConfigInterface
} from "ngx-perfect-scrollbar";

import { HttpConfigInterceptor } from "@core/httpconfig.interceptor";
import { TagService } from "@services/tag.service";
import { RouteTabService } from "@services/route-tab.service";
import { NotificationsService } from '@services/notifications.service';
import { MAT_DATE_LOCALE } from '@angular/material';

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
  suppressScrollX: true
};

// required for AOT compilation
export function HttpLoaderFactory(http: HttpClient): any {
  return new TranslateHttpLoader(http);
}

@NgModule({
  declarations: [
    AppComponent,
    RootLayout,
    BlankComponent,
    CondensedComponent,
    HeaderComponent,
    MenuComponent,
    MenuAltComponent,
    MenuIconComponent,
        SidebarComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    RootStoreModule,
    HttpClientModule,
    PerfectScrollbarModule,
    SharedModule,
    NotificationModule,
    BsDropdownModule.forRoot(),
    AccordionModule.forRoot(),
    AlertModule.forRoot(),
    ButtonsModule.forRoot(),
    CollapseModule.forRoot(),
    ModalModule.forRoot(),
    ProgressbarModule.forRoot(),
    TabsModule.forRoot(),
    MatTooltipModule,
    TooltipModule,
    MatTabsModule,
    ContextMenuModule.forRoot(),
    TypeaheadModule.forRoot(),
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    })
  ],
  providers: [
    HttpService,
    HubService,
    AuthenticatedGuard,
    SystemEmptyGuard,
    PagesToggleService,
    NotificationsService,
    TagService,
    RouteTabService,
    {
      provide: PERFECT_SCROLLBAR_CONFIG,
      useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG
    },
      { provide: HTTP_INTERCEPTORS, useClass: HttpConfigInterceptor, multi: true }
      //{ provide: LOCALE_ID, useValue: 'ru-RU' },
      //{ provide: MAT_DATE_LOCALE, useValue: 'ru-RU' }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
