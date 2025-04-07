export interface IMessageDataOptions {
  Position?: string;
  Style?: string;
  Duration?: number;
  Animate?: boolean;
  Title?: string;
  imgURL?: string;
  PauseOnHover?: boolean;
}

// message data for terminal users
export interface IMessageData {
  // for html
  html?: string;

  // for string content
  type?: "success" | "info" | "warning" | "error" | "loading" | string;
  style?: "simple" | "bar" | "flip" | "circle" | string;
  position?: "top";
  content?: string;
}

// filled version of MessageData (includes more private properties)
export interface IMessageDataFilled extends IMessageData {
  messageId: string; // service-wide unique id, auto generated
  state?: "enter" | "leave";
  options?: IMessageDataOptions;
  createdAt: Date; // auto created
}
