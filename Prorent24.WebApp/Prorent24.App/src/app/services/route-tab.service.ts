import { Injectable } from "@angular/core";

export interface ITab {
  label: string;
  routerLink: string;
}

@Injectable({
  providedIn: "root"
})
export class RouteTabService {
  tabs: ITab[] = [];
  tabOptions: ITab[] = [];

  constructor() {}

  addTab(url: string) {
    const tab = this.getTabOptionByUrl(url);
    if (!this.tabs.includes(tab)) {
      this.tabs.push(tab);
    }
  }

  addTabObject(tab: ITab) {
    const _tab = this.tabs.find((t: any) => t.routerLink === tab.routerLink );;
    if (!this.tabs.includes(_tab) && tab.label) {
      this.tabs.push(tab);
    }
  }

  getTabOptionByUrl(url: string): ITab {
    return this.tabOptions.find((tab: any) => tab.routerLink === url);
  }

  deleteTab(index: number) {
    this.tabs.splice(index, 1);
  }
}
