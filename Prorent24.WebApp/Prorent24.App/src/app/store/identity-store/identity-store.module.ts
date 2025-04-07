import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { EffectsModule } from "@ngrx/effects";
import { StoreModule } from "@ngrx/store";
import { IdentityStoreEffects } from "./effects";
import { identityReducer } from "./reducer";

@NgModule({
  imports: [
    CommonModule,
    StoreModule.forFeature("identity", identityReducer),
    EffectsModule.forFeature([IdentityStoreEffects])
  ],
  providers: [IdentityStoreEffects]
})
export class IdentityStoreModule {}
