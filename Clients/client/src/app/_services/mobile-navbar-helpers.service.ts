import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class MobileNavbarHelpersService {

  constructor() { }

  openMobileNavbar(navbarPanelId : string){
    var nav=(document.querySelector('#'+navbarPanelId) as HTMLElement);
    nav.style.width = '100%';
    nav.style.transition= '0.5s'

    var topNav=(document.querySelector('#top-navbar') as HTMLElement);
    topNav.style.zIndex='-1';
  }
}
