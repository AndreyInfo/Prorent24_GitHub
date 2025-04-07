import { Overlay } from "@angular/cdk/overlay";
import { ComponentPortal } from "@angular/cdk/portal";
import { Injectable, Type } from "@angular/core";
import { MessageContainerComponent } from "./notification-container.component";
import {
  IMessageData,
  IMessageDataFilled,
  IMessageDataOptions
} from "./notification.definitions";

export class MessageBaseService<
  ContainerClass extends MessageContainerComponent,
  MessageData
> {
  protected _counter = 0; // id counter for messages
  protected _container: ContainerClass;

  constructor(
    overlay: Overlay,
    containerClass: Type<ContainerClass>,
    private _idPrefix: string = ""
  ) {
    this._container = overlay
      .create()
      .attach(new ComponentPortal(containerClass)).instance;
  }

  remove(messageId?: string): void {
    if (messageId) {
      this._container.removeMessage(messageId);
    } else {
      this._container.removeMessageAll();
    }
  }

  createMessage(
    message: object,
    options?: IMessageDataOptions
  ): IMessageDataFilled {
    const resultMessage: IMessageDataFilled = {
      ...message,
      ...{
        messageId: this._generateMessageId(),
        options,
        createdAt: new Date()
      }
    };
    this._container.createMessage(resultMessage);

    return resultMessage;
  }

  protected _generateMessageId(): string {
    return this._idPrefix + this._counter++;
  }
}

@Injectable()
export class MessageService extends MessageBaseService<
  MessageContainerComponent,
  IMessageData
> {
  constructor(overlay: Overlay) {
    super(overlay, MessageContainerComponent, "message-");
  }

  // shortcut methods
  success(content: string, options?: IMessageDataOptions): IMessageDataFilled {
    return this.createMessage({ type: "success", content }, options);
  }

  error(content: string, options?: IMessageDataOptions): IMessageDataFilled {
    return this.createMessage({ type: "error", content }, options);
  }

  info(content: string, options?: IMessageDataOptions): IMessageDataFilled {
    return this.createMessage({ type: "info", content }, options);
  }

  warning(content: string, options?: IMessageDataOptions): IMessageDataFilled {
    return this.createMessage({ type: "warning", content }, options);
  }

  create(
    type: string,
    content: string,
    options?: IMessageDataOptions
  ): IMessageDataFilled {
    return this.createMessage({ type, content }, options);
  }
}
