import { HostListener, Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import {BreakpointObserver, Breakpoints, BreakpointState} from '@angular/cdk/layout';
@Injectable({
  providedIn: 'root'
})
export class ResizeWindowWatcherService {
  private mobileMode=new ReplaySubject <boolean>(1);
  mobileMode$=this.mobileMode.asObservable();

  //detect when window pass through the mid breakpoint
  constructor(private breakpointObserver:BreakpointObserver) { 
    breakpointObserver.observe([
      '(max-width: 992px)',
        ]).subscribe((state: BreakpointState) => {
          this.mobileMode.next(state.breakpoints['(max-width: 992px)']);
        });
  }
}
