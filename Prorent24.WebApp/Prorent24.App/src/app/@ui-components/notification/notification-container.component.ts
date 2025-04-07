import {
  Component,
  Inject,
  OnInit,
  Optional,
  ViewEncapsulation
} from "@angular/core";
import {
  IMessageConfig,
  _MESSAGE_CONFIG,
  _MESSAGE_DEFAULT_CONFIG
} from "./notification.config";
import {
  IMessageDataFilled,
  IMessageDataOptions
} from "./notification.definitions";

@Component({
  selector: "pg-message-container",
  encapsulation: ViewEncapsulation.None,
  template: `
    <div
      class="pgn-wrapper"
      [class.hide]="messages.length == 0"
      *ngIf="currentMessage"
      [attr.data-position]="currentMessage.options.Position"
      [ngStyle]="style"
    >
      <pg-message
        *ngFor="let message of messages; let i = index"
        [Message]="message"
        [Index]="i"
      ></pg-message>
    </div>
  `,
  styleUrls: []
})
export class MessageContainerComponent {
  messages: IMessageDataFilled[] = [];
  currentMessage = null;
  style;
  config: IMessageConfig;

  constructor(
    @Optional() @Inject(_MESSAGE_DEFAULT_CONFIG) defaultConfig: IMessageConfig,
    @Optional() @Inject(_MESSAGE_CONFIG) config: IMessageConfig
  ) {
    this.config = { ...defaultConfig, ...config };
  }

  // create a new message
  createMessage(message: IMessageDataFilled): void {
    let el: any = window.document.querySelector(".header ");
    if (el) {
      let rect: any = el.getBoundingClientRect();
      this.style = {
        marginTop: rect.height + "px"
      };
    }
    this.currentMessage = message;
    if (this.messages.length >= this.config.MaxStack) {
      this.messages.splice(0, 1);
    }
    message.options = this._mergeMessageOptions(message.options);
    this.messages.push(message);
  }

  // remove a message by messageId
  removeMessage(messageId: string): void {
    this.messages.some((message, index) => {
      if (message.messageId === messageId) {
        this.messages.splice(index, 1);
        return true;
      }
    });
  }

  // remove all messages
  removeMessageAll(): void {
    this.messages = [];
  }

  // merge default options and cutom message options
  protected _mergeMessageOptions(
    options: IMessageDataOptions
  ): IMessageDataOptions {
    const defaultOptions: IMessageDataOptions = {
      Duration: this.config.Duration,
      Animate: this.config.Animate,
      PauseOnHover: this.config.PauseOnHover
    };
    return { ...defaultOptions, ...options };
  }
}
