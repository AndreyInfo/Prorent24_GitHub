import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { EffectsModule } from "@ngrx/effects";
import { StoreModule } from "@ngrx/store";
import { NotificationsStoreEffects } from "./effects";
import { notificationsReducer } from "./reducer";

@NgModule({
  imports: [
    CommonModule,
    StoreModule.forFeature("notifications", notificationsReducer),
    EffectsModule.forFeature([NotificationsStoreEffects])
  ],
  providers: [NotificationsStoreEffects]
})
export class NotificationsStoreModule {}
