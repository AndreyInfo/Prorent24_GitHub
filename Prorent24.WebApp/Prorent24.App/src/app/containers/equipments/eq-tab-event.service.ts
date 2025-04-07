import { Injectable, EventEmitter } from "@angular/core";
import { Subject } from 'rxjs';

@Injectable()
export class EqTabEventService {
    private emitChangeSource = new Subject<any>();

    changeEmitted$ = this.emitChangeSource.asObservable();

  emitChange(data: any) {
    this.emitChangeSource.next(data);
  }
}
