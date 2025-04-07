import { Injectable } from "@angular/core";
import { Subject } from "rxjs/Subject";

@Injectable()
export class PagesToggleService {
  // search Toggle
  private _searchToggle = new Subject();
  searchToggle = this._searchToggle.asObservable();

  // quickview Toggle
  private _quickViewToggle = new Subject();
  quickViewToggle = this._quickViewToggle.asObservable();

  // sidebar Toggle - Mobile
  private _sideBarToggle = <Subject<boolean>>new Subject();
  sideBarToggle = this._sideBarToggle.asObservable();

  // secondary Sidebar Toggle - Mobile
  private _secondarySideBarToggle = <Subject<any>>new Subject();
  secondarySideBarToggle = this._secondarySideBarToggle.asObservable();

  // horizontal Menu Toggle - Mobile
  private _mobileHorizontaMenu = <Subject<boolean>>new Subject();
  mobileHorizontaMenu = this._mobileHorizontaMenu.asObservable();

  // menu Pin Toggle
  private _menuPinToggle = new Subject();
  menuPinToggle = this._menuPinToggle.asObservable();

  // menu Pin Toggle
  private _menuDrawer = <Subject<string>>new Subject();
  menuDrawer = this._menuDrawer.asObservable();

  // page Wrapper Class
  private _pageContainerClass = <Subject<string>>new Subject();
  pageContainerClass = this._pageContainerClass.asObservable();

  // page Content Class
  private _contentClass = <Subject<string>>new Subject();
  contentClass = this._contentClass.asObservable();

  // header Class
  private _headerClass = <Subject<string>>new Subject();
  headerClass = this._headerClass.asObservable();

  // body Layout Class
  private _bodyLayoutClass = <Subject<string>>new Subject();
  bodyLayoutClass = this._bodyLayoutClass.asObservable();

  // app Layout
  private _layout = <Subject<string>>new Subject();
  Applayout = this._layout.asObservable();

  // footer Visiblity
  private _footer = <Subject<boolean>>new Subject();
  Footer = this._footer.asObservable();

  // page Container Hover Event - Used for sidebar
  private _pageContainerHover = <Subject<boolean>>new Subject();
  pageContainerHover = this._pageContainerHover.asObservable();

    setContent(className: string): void {
    this._contentClass.next(className);
  }

  setPageContainer(className: string): void {
    this._pageContainerClass.next(className);
  }

  setHeaderClass(className: string): void {
    this._headerClass.next(className);
  }

  setBodyLayoutClass(className: string): void {
    this._bodyLayoutClass.next(className);
  }

  removeBodyLayoutClass(className: string): void {
    this._bodyLayoutClass.next(className);
  }

  changeLayout(className: string): void {
    this._layout.next(className);
  }

  toggleSearch(toggle: boolean): void {
    this._searchToggle.next({ text: toggle });
  }

  toggleMenuPin(toggle: boolean): void {
    this._menuPinToggle.next({ text: toggle });
  }

  toggleMenuDrawer(): void {
    this._menuDrawer.next();
  }

  toggleQuickView(): void {
    this._quickViewToggle.next();
  }

  toggleMobileSideBar(toggle: boolean): void {
    this._sideBarToggle.next(toggle);
  }

  toggleSecondarySideBar(toggle: boolean): void {
    this._secondarySideBarToggle.next(toggle);
  }

  toggleMobileHorizontalMenu(toggle: boolean): void {
    this._mobileHorizontaMenu.next(toggle);
  }

  toggleFooter(toggle: boolean): void {
    this._footer.next(toggle);
  }

  triggerPageContainerHover(toggle: boolean): void {
    this._pageContainerHover.next(toggle);
  }
}
