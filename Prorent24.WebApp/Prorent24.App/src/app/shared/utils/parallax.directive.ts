import { Directive, ElementRef, HostListener, Renderer2 } from "@angular/core";
declare var pg: any;

@Directive({
  selector: "[pg-parallax]"
})
export class ParallaxDirective {
  scrollElement: any = "window";
  scrollPos = 0;
  nativeElement;
  coverPhotoSpeend = 0.3;
  contentSpeed = 0.17;
  windowSize;
  constructor(private parallaxEl: ElementRef, private renderer: Renderer2) {
    this.windowSize = window.innerWidth;
  }

  ngOnInit(): void {
    this.nativeElement = this.parallaxEl.nativeElement;
    this.registerPageContainerScroller();
  }

  registerPageContainerScroller(): void {
    if (!pg.isHorizontalLayout) {
      return;
    }
    let pageContainer: any = document.querySelector(".page-container");
    if (pageContainer) {
      this.scrollElement = pageContainer;
      this.renderer.listen(pageContainer, "scroll", event => {
        this.animate();
      });
    }
  }

  @HostListener("window:resize", [])
  onResize(): void {
    this.windowSize = window.innerWidth;
  }

  @HostListener("window:scroll", [])
  onWindowScroll(): void {
    this.animate();
  }

  animate(): void {
    if ((this.windowSize = window.innerWidth < 1025)) {
      return;
    }
    let rect: any = this.nativeElement.getBoundingClientRect();
    let opacityKeyFrame: number = (rect.width * 50) / 100;
    let direction: string = "translateX";

    if (this.scrollElement === "window") {
      this.scrollPos = window.pageYOffset || document.documentElement.scrollTop;
    } else {
      this.scrollPos = this.scrollElement.scrollTop;
    }

    direction = "translateY";
    let styleString: string =
      direction + "(" + this.scrollPos * this.coverPhotoSpeend + "px)";

    this.nativeElement.style.transform = styleString;

    this.nativeElement.style.webkitTransform = styleString;
    this.nativeElement.style.mozTransform = styleString;
    this.nativeElement.style.msTransform = styleString;

    if (this.scrollPos > opacityKeyFrame) {
      this.nativeElement.style.opacity = 1 - this.scrollPos / 1200;
    } else {
      this.nativeElement.style.opacity = 1;
    }
  }
}
