import { Injectable } from "@angular/core";
import { Store } from "@ngrx/store";
import { RootStoreState } from "@store";
import { IdentityStoreSelectors } from "@store";
import { take, map } from "rxjs/operators";
import { Entity } from "@shared/enums/entity.enum";
import { Observable } from "rxjs";

const capitalize: any = (str: string) => {
  return str.charAt(0).toUpperCase() + str.slice(1);
};

@Injectable()
export class PermissionService {
  constructor(public store$: Store<RootStoreState.IState>) {}

  getPermissions(entity: Entity): Observable<any> {
    return this.store$
      .select(IdentityStoreSelectors.selectPermissions)
      .pipe(take(1))
      .pipe(
        map((data: Array<any>) => {
          return data.find(f => f.module === capitalize(entity));
        })
      );
  }
}
