import { Injectable, Output, EventEmitter } from "@angular/core";

@Injectable()
export class TagService {
  @Output() onPush: EventEmitter<any> = new EventEmitter();;

  push(tag: any): void {
    this.onPush.emit(tag);
  }
}
