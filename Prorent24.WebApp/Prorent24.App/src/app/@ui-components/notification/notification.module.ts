import { OverlayModule } from "@angular/cdk/overlay";
import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { _MESSAGE_DEFAULT_CONFIG_PROVIDER } from "./notification.config";
import { MessageContainerComponent } from "./notification-container.component";
import { MessageComponent } from "./notification.component";
import { MessageService } from "./notification.service";
import { TranslateModule } from "@ngx-translate/core";

const providers: any = [_MESSAGE_DEFAULT_CONFIG_PROVIDER];

@NgModule({
  imports: [CommonModule, OverlayModule, TranslateModule],
  declarations: [MessageContainerComponent, MessageComponent],
  providers: [MessageService],
  entryComponents: [MessageContainerComponent]
})
export class NotificationModule {}
