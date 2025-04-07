import { InjectionToken } from "@angular/core";

export interface IMessageConfig {
  // for all messages as default config (can override when dynamically created)
  Duration?: number;
  PauseOnHover?: boolean;
  Animate?: boolean;
  Position?: string;
  Style?: string;
  // for message container only
  MaxStack?: number;
  /* tslint:disable-next-line:no-any */
  [index: string]: any;
}

export const _MESSAGE_DEFAULT_CONFIG: any = new InjectionToken<IMessageConfig>(
  "_MESSAGE_DEFAULT_CONFIG"
);

export const _MESSAGE_CONFIG: any = new InjectionToken<IMessageConfig>(
  "_MESSAGE_CONFIG"
);

export const _MESSAGE_DEFAULT_CONFIG_PROVIDER: any = {
  provide: _MESSAGE_DEFAULT_CONFIG,
  useValue: {
    Position: "top-right",
    Style: "simple",
    Duration: 1500,
    Animate: true,
    PauseOnHover: true,
    MaxStack: 7
  }
};
