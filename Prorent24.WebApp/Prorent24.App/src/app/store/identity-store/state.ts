import { createEntityAdapter, EntityAdapter, EntityState } from "@ngrx/entity";
import { User } from "@models/session/user.model";

export const adapter: EntityAdapter<User> = createEntityAdapter<User>();

export interface IState extends EntityState<User> {
  isAuthenticated: boolean;
  isEmpty: boolean;
  user: User | null;
  error: string | null;
  permissions: Array<any>;
  modules: Array<any>;
  logo: string;
}

export const initialState: IState = adapter.getInitialState({
  isAuthenticated: false,
  isEmpty: false,
  user: null,
  error: null,
  permissions: [],
  modules: [],
  logo: "../../../assets/img/logo-default.png"
});
