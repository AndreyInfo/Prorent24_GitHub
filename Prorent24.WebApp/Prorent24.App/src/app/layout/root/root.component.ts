import {
  Component,
  OnDestroy,
  ViewChild,
  Input,
  HostListener,
  ChangeDetectorRef
} from "@angular/core";
import { Subscription, Observable } from "rxjs";
import { PagesToggleService } from "@shared/utils/toggler.service";
import { Router, Event, NavigationEnd, ActivatedRoute } from "@angular/router";

import { Store } from "@ngrx/store";
import { RootStoreState } from "@store";
import { User } from "@models/session/user.model";
import { RouteTabService } from "@services/route-tab.service";
import { ContextMenuService } from 'ngx-contextmenu';
declare var pg: any;

@Component({
  selector: "root-layout",
  template: ``
})
export class RootLayout implements OnDestroy {
  @ViewChild("root", { static: false }) root: any;
  user$: Observable<User>;
  logo$: Observable<string>;

  layoutState: string;
  extraLayoutClass: string;
  _boxed: boolean = false;
  _menuPin: boolean = false;
  _enableHorizontalContainer: boolean;
  _pageContainerClass = "";
  _contentClass = "t";
  _footer: boolean = true;
  _menuDrawerOpen: boolean = false;
  // mobile
  _secondarySideBar: boolean = false;
  // mobile
  _mobileSidebar: boolean = false;
  // mobile
  _mobileHorizontalMenu: boolean = false;
  _pageTitle: string;
  // sub layout - eg: email
  _layoutOption: string;
  _subscriptions: Array<Subscription> = [];
  _layout;
  @Input()
  public contentClass: string = "";

  @Input()
  public pageWrapperClass: string = "";

  @Input()
  public footer: boolean = true;

  constructor(
    public toggler: PagesToggleService,
    public store$: Store<RootStoreState.IState>,
    public router: Router,
    public route: ActivatedRoute,
    public tabService: RouteTabService,
    private cdRef: ChangeDetectorRef,
    public _contextMenuService: ContextMenuService
  ) {
    if (this.layoutState) {
      pg.addClass(document.body, this.layoutState);
    }
    
    router.events.subscribe((event: Event) => {
      if (event instanceof NavigationEnd) {
        var root: any = this.router.routerState.snapshot.root;

        while (root) {
          if (root.children && root.children.length) {
            root = root.children[0];
          } else if (root.data) {
            // custom Route Data here
            this._pageTitle = root.data["title"];
            this._layoutOption = root.data["layoutOption"];
            this._boxed = root.data["boxed"];
            break;
          } else {
            break;
          }
        }

        this.tabService.addTabObject({
          label: this._pageTitle,
          routerLink: this.router.routerState.snapshot.url
        });

        // reset Any Extra Layouts added from content pages
        pg.removeClass(document.body, this.extraLayoutClass);
        // close Sidebar and Horizonta Menu
        if (this._mobileSidebar) {
          this._mobileSidebar = false;
          pg.removeClass(document.body, "sidebar-open");
          this.toggler.toggleMobileSideBar(this._mobileSidebar);
        }

        setTimeout(() => {
          this.toggler.setContent("");
          this.toggler.setPageContainer("");
          this.toggler.toggleFooter(true);
        });

        this._mobileHorizontalMenu = false;
        this.toggler.toggleMobileHorizontalMenu(this._mobileHorizontalMenu);
        // srcoll Top
        this.scrollToTop();
      }

      // subscribition List
      this._subscriptions.push(
        this.toggler.pageContainerClass.subscribe(state => {
          this._pageContainerClass = state;
        })
      );

      this._subscriptions.push(
        this.toggler.contentClass.subscribe(state => {
          this._contentClass = state;
        })
      );

      this._subscriptions.push(
        this.toggler.bodyLayoutClass.subscribe(state => {
          if (state) {
            this.extraLayoutClass = state;
            pg.addClass(document.body, this.extraLayoutClass);
          }
        })
      );

      this._subscriptions.push(
        this.toggler.Applayout.subscribe(state => {
          this.changeLayout(state);
        })
      );

      this._subscriptions.push(
        this.toggler.Footer.subscribe(state => {
          this._footer = state;
        })
      );

      this._subscriptions.push(
        this.toggler.mobileHorizontaMenu.subscribe(state => {
          this._mobileHorizontalMenu = state;
        })
      );
    });
  }

  /** @function changeLayout
   *   @description Add Document Layout Class
   */
  changeLayout(type: string): void {
    this.layoutState = type;
    if (type) {
      pg.addClass(document.body, type);
    }
  }

  /** @function removeLayout
   *   @description Remove Document Layout Class
   */
  removeLayout(type: string): void {
    pg.removeClass(document.body, type);
  }

  ngOnDestroy(): void {
    for (const sub of this._subscriptions) {
      sub.unsubscribe();
    }

    this.cdRef.detach();
  }

  /** @function scrollToTop
   *   @description Force to scroll to top of page. Used on Route
   */
  scrollToTop(): void {
    let top: number = window.pageYOffset;
    if (top === 0) {
      let scroller: any = document.querySelector(".page-container");
      if (scroller) {
        scroller.scrollTo(0, 0);
      }
    } else {
      window.scrollTo(0, 0);
    }
  }

  /** @function openQuickView
   *   @description Show Quick View Component / Right Sidebar - Service
   */
  openQuickView($e: any): void {
    $e.preventDefault();
    this.toggler.toggleQuickView();
  }

  /** @function openSearch
   *   @description Show Quick Search Component - Service
   */

  openSearch($e: any): void {
    $e.preventDefault();
    this.toggler.toggleSearch(true);
  }

  /** @function toggleMenuPin
   *   @description Permanently Open / Close Main Sidebar
   */
  toggleMenuPin($e: any): void {
    if (pg.isVisibleSm()) {
      this._menuPin = false;
      return;
    }
    if (this._menuPin) {
      pg.removeClass(document.body, "menu-pin");
      this._menuPin = false;
    } else {
      pg.addClass(document.body, "menu-pin");
      this._menuPin = true;
    }
  }

  /** @function toggleMenuDrawer
   *   @description Open Main Sidebar Menu Drawer - Service
   */
  toggleMenuDrawer(): void {
    this._menuDrawerOpen = this._menuDrawerOpen === true ? false : true;
    this.toggler.toggleMenuDrawer();
  }

  /** @function toggleMobileSidebar
   *   @description Open Main Sidebar on Mobile - Service
   */
  toggleMobileSidebar(): void {
    if (this._mobileSidebar) {
      this._mobileSidebar = false;
      pg.removeClass(document.body, "sidebar-open");
    } else {
      this._mobileSidebar = true;
      pg.addClass(document.body, "sidebar-open");
    }
    this.toggler.toggleMobileSideBar(this._mobileSidebar);
  }

  /** @function toggleHorizontalMenuMobile
   *   @description Open Secondary Sidebar on Mobile - Service
   */
  toggleSecondarySideBar(): void {
    this._secondarySideBar = this._secondarySideBar === true ? false : true;
    this.toggler.toggleSecondarySideBar(this._secondarySideBar);
  }

  /** @function toggleHorizontalMenuMobile
   *   @description Call Horizontal Menu Toggle Service for mobile
   */
  toggleHorizontalMenuMobile(): void {
    this._mobileHorizontalMenu =
      this._mobileHorizontalMenu === true ? false : true;
    this.toggler.toggleMobileHorizontalMenu(this._mobileHorizontalMenu);
  }

  @HostListener("window:resize", [])
  onResize(): void {
    this.autoHideMenuPin();
  }

  // utils
  autoHideMenuPin(): void {
    if (window.innerWidth < 1025) {
      if (pg.hasClass(document.body, "menu-pin")) {
        pg.addClass(document.body, "menu-unpinned");
        pg.removeClass(document.body, "menu-pin");
      }
    } else {
      if (pg.hasClass(document.body, "menu-unpinned")) {
        pg.addClass(document.body, "menu-pin");
      }
    }
  }

  // tabs
  openTab(url: string) {
    this.tabService.addTab(url);
    this.router.navigateByUrl(url);
  }

  closeTab(index: number, event: any) {
    this.tabService.deleteTab(index);
    event.preventDefault();
  }

}
