import {
  Component,
  OnInit,
  Input,
  ViewEncapsulation,
  HostListener,
  ViewChild
} from "@angular/core";
import {
  animate,
  state,
  style,
  transition,
  trigger
} from "@angular/animations";
import { PerfectScrollbarConfigInterface } from "ngx-perfect-scrollbar";
import { Router } from "@angular/router";
import { ContextMenuComponent,ContextMenuService } from 'ngx-contextmenu';

declare var pg: any;

@Component({
  selector: "pg-menu-items",
  templateUrl: "./menu.component.html",
  styleUrls: ["./menu.component.scss"],
  animations: [
    trigger("toggleHeight", [
      state(
        "close",
        style({
          height: "0",
          overflow: "hidden",
          marginBottom: "0"
        })
      ),
      state(
        "open",
        style({
          height: "*",
          marginBottom: "10px",
          overflow: "hidden"
        })
      ),
      transition("close => open", animate("140ms ease-in")),
      transition("open => close", animate("140ms ease-out"))
    ])
  ],
  encapsulation: ViewEncapsulation.None
})
export class MenuComponent implements OnInit {
  @Input() openedSideBar: boolean = false;
  menuItems = [];
  currentItem = null;
  isPerfectScrollbarDisabled = false;
  pin: boolean = false;
  style: string;
  private sideBarWidthCondensed = 280 - 70;
  public config: PerfectScrollbarConfigInterface = {};
  profileOptions = {
    'placement': 'right',
    'show-delay': 500,
    'width': 220,
    'hideDelayAfterClick': 1000000000,
    'position': {
      'top': 108,
      'left': 176
    },
    'trigger': 'click',
    'pointerEvents': 'auto'
  }

  @ViewChild(ContextMenuComponent,{ static: false }) cloudMenu: ContextMenuComponent;

  constructor(private _contextMenuService: ContextMenuService,private router: Router) { }

  

  ngOnInit() { }

  ngAfterViewInit() {
    setTimeout(() => {
      this.togglePerfectScrollbar();
    });
  }

  @HostListener("window:resize", [])
  onResize() {
    this.togglePerfectScrollbar();
  }

  togglePerfectScrollbar() {
    this.isPerfectScrollbarDisabled = window.innerWidth < 1025;
  }

  @Input()
  set Items(value) {
      console.log("p1-Items" + JSON.stringify(value));
      console.log("p2-Items" + value[4].label);
      value[4].label = "ffffffffff";
      value[4].routerLink = "/sublease";
      console.log("p3-Items" + value[4].label);
      this.menuItems = value;
      //this.menuItems[4].label = "ddddd";
  }

    toggleNavigationSub(event, item) {
    event.preventDefault();
    if (this.currentItem && this.currentItem != item) {
      this.currentItem["toggle"] = "close";
    }
    this.currentItem = item;
    item.toggle = item.toggle === "close" ? "open" : "close";


  }

    toggleNavigation(item) {

    if (item.iconName == "row-right") {
      this.openSideBar();
      item.iconName = "row-left"
    }
    else {
      this.closeSideBar();
      item.iconName = "row-right"
    }
  }

      
  openSideBar(): boolean {
    if (pg.isVisibleSm() || pg.isVisibleXs()) {
      return false;
    }
    if (this.pin) {
      return false;
    }

    this.style = "transform:translate3d(" + this.sideBarWidthCondensed + "px, 0,0)";
    pg.addClass(document.body, "sidebar-visible");
    let pageSidebar = document.getElementsByClassName("page-sidebar")[0];
    pageSidebar.setAttribute("style", this.style);
    pageSidebar.removeAttribute("close");
    pageSidebar.setAttribute("open", "true");
    this.openedSideBar = true;
  }

  closeSideBar(): boolean {
    if (pg.isVisibleSm() || pg.isVisibleXs()) {
      return false;
    }
    if (this.pin) {
      return false;
    }

    this.style = "transform:translate3d(0,0,0)";
    pg.removeClass(document.body, "sidebar-visible");
    let pageSidebar = document.getElementsByClassName("page-sidebar")[0];
    pageSidebar.setAttribute("style", this.style);
    pageSidebar.removeAttribute("open");
    pageSidebar.setAttribute("close", "true");
    this.openedSideBar = false;
  }

    public onCloudMenu($event: KeyboardEvent, item: any): void {
      this._contextMenuService.show.next({
        anchorElement: $event.target,
        contextMenu: this.cloudMenu,
        event: <any>$event,
        item: item,
      });

    $event.preventDefault();
    $event.stopPropagation();
  }

    onCloudMenuClick($event) {
        console.log("p2Menu - " + $event);
    if($event.item && !$event.item.submenu){
      this.router.navigate([$event.item.routerLink]);
    }
  }

  getOpacity(value) {
    if (value == '#') {
      return '80%';
    }
  }

}
