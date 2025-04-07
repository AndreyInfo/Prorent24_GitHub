import {
  Component,
  OnInit,
  ElementRef,
  ViewEncapsulation,
  TemplateRef,
  ContentChild,
  HostListener,
  HostBinding
} from "@angular/core";
import { Subscription } from "rxjs/Subscription";
import { PagesToggleService } from "@shared/utils/toggler.service";
declare var pg: any;

@Component({
  selector: "pg-sidebar",
  templateUrl: "./sidebar.component.html",
  styleUrls: ["./sidebar.component.scss"],
  host: {
    class: "page-sidebar"
  },
  encapsulation: ViewEncapsulation.None
})
export class SidebarComponent implements OnInit {
  subscriptions: Array<Subscription> = [];
  pin: boolean = false;
  drawer: boolean = false;
  sidebar;
  timer;
  @HostBinding("style.transform")
  style: string;

  private sideBarWidth = 280;
  private sideBarWidthCondensed = 280 - 70;

  @ContentChild("sideBarOverlay", { static: true }) sideBarOverlay: TemplateRef<void>;
  @ContentChild("sideBarHeader", { static: true }) sideBarHeader: TemplateRef<void>;
  @ContentChild("menuItems", { static: true }) menuItems: TemplateRef<void>;

  constructor(
    private appSidebar: ElementRef,
    private toggler: PagesToggleService
  ) {
    this.subscriptions.push(
      this.toggler.sideBarToggle.subscribe(toggle => {
        this.toggleMobile(toggle);
      })
    );
    this.subscriptions.push(
      this.toggler.pageContainerHover.subscribe(message => {
        this.closeSideBar();
      })
    );
    this.subscriptions.push(
      this.toggler.menuDrawer.subscribe(message => {
        this.toggleDrawer();
      })
    );
    //this.mobileSidebar = false;
  }

  ngOnInit(): void {}

  ngOnDestroy(): void {
    for (const subs of this.subscriptions) {
      subs.unsubscribe();
    }
    clearTimeout(this.timer);
  }


  // openSideBar(): boolean {
  //   debugger
  //   if (pg.isVisibleSm() || pg.isVisibleXs()) {
  //     return false;
  //   }
  //   if (this.pin) {
  //     return false;
  //   }

  //   this.style = "translate3d(" + this.sideBarWidthCondensed + "px, 0,0)";
  //   pg.addClass(document.body, "sidebar-visible");
  // }

  // @HostListener("click", ["$event"])
  closeSideBar(): boolean {
    if (pg.isVisibleSm() || pg.isVisibleXs()) {
      return false;
    }
    if (this.pin) {
      return false;
    }

    this.style = "translate3d(0,0,0)";
    pg.removeClass(document.body, "sidebar-visible");
    let pageSidebar = document.getElementsByClassName("page-sidebar")[0];
    pageSidebar.removeAttribute("open");
    pageSidebar.setAttribute("close","true");
  }

  toggleMenuPin(): void {
    if (this.pin) {
      this.pin = false;
    } else {
      this.pin = true;
    }
  }

  toggleDrawer(): void {
    if (this.drawer) {
      this.drawer = false;
    } else {
      this.drawer = true;
    }
  }

  toggleMobile(toggle: boolean): void {
    clearTimeout(this.timer);
    if (toggle) {
    //  this.mobileSidebar = toggle;
    } else {
      this.timer = setTimeout(() => {
      //  this.mobileSidebar = toggle;
      }, 400);
    }
  }
}
