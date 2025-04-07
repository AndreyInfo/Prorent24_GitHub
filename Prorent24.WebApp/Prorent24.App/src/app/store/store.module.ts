import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { EffectsModule } from "@ngrx/effects";
import { StoreModule } from "@ngrx/store";
import { StoreDevtoolsModule } from "@ngrx/store-devtools";

import { IdentityStoreModule } from "@store/identity-store/identity-store.module";
import { NotificationsStoreModule } from '@store/notifications-store/notifications-store.module';

@NgModule({
  imports: [
    CommonModule,
    StoreModule.forRoot({}),
    EffectsModule.forRoot([]),
    StoreDevtoolsModule.instrument({
      maxAge: 20
    }),
    IdentityStoreModule,
    NotificationsStoreModule
  ],
  declarations: []
})
export class RootStoreModule {}
