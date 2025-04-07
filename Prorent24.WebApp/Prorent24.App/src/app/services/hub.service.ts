import { Injectable } from "@angular/core";
import * as signalR from "@aspnet/signalr";
import { MessageService } from "@ui-components/notification/notification.service";
import { Store } from "@ngrx/store";
import { RootStoreState } from "@store";
import { NotificationsStoreActions } from "@store";

@Injectable()
export class HubService {
  private hubConnection: signalR.HubConnection;

  constructor(
    private notify: MessageService,
    public store$: Store<RootStoreState.IState>
  ) {}

  startConnection = () => {
    Object.defineProperty(WebSocket, "OPEN", { value: 1 });

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl("/hub")
      .build();

    this.hubConnection
      .start()
      .then()
      .catch(err => console.log("Error while starting connection: " + err));
  };

  public addTransferDataListener = () => {
    let msgData;
    this.hubConnection.on("notification", event => {
      msgData = event.data;
      this.notify.create(event.eventType, event.data.message, {
        Position: "top-right",
        Style: "circle",
        Duration: 30000,
        Title: event.data.title,
        imgURL: "../assets/images/icons/info_icon.png"
      });

      this.store$.dispatch(
        new NotificationsStoreActions.AddNotification({
          id: null,
          entityId: null,
          theme: msgData.title,
          message: msgData.message,
          creationDate: new Date(Date.now()),
          isRead: false,
          moduleType: null
        })
      );
    });
  };
}
