import { Component, HostListener, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ResizeWindowWatcherService } from '../services/resize-window-watcher.service';
import { navbarItem } from '../models/navbarItem';

@Component({
  selector: 'app-side-navbar',
  templateUrl: './side-navbar.component.html',
  styleUrls: ['./side-navbar.component.css']
})
export class SideNavbarComponent implements OnInit {

  @HostListener('window:resize', ['$event'])onResize($event:any) {
    //this.closeNav();
  }
  @Input() navbarItems:navbarItem[]=[];

  public loaded:boolean=false;
  constructor(public resizeWindowWatcherService:ResizeWindowWatcherService
    ,private router:Router) { }

  ngOnInit(): void {
    this.resizeWindowWatcherService.mobileMode$.subscribe(x=>{
      if(x == false){
        (document.querySelector('#side-navbar') as HTMLElement).style.width = '';
      }
      else{
        this.closeNav();
      }
    })
  }

  expandReduce(event:any){
    var subMenu=event.srcElement.parentElement.querySelectorAll('ul')[0];
    
    var icon=event.srcElement.parentElement.querySelectorAll('a>i')[0];
    if(subMenu!=null){
      if(subMenu.classList.contains('submenu-closed')){
        subMenu.classList.remove("submenu-closed");
        icon.classList.add("fa-chevron-up");
        icon.classList.remove("fa-chevron-down");
      }
      else{
        subMenu.classList.add("submenu-closed");
        icon.classList.remove("fa-chevron-up");
        icon.classList.add("fa-chevron-down");
      }
    }
  }

  selectedUrl(){
    this.closeNav();
  }

  closeNav(){
    (document.querySelector('#side-navbar') as HTMLElement).style.width = '0%';
    var topNav=(document.querySelector('#top-navbar') as HTMLElement);
    topNav.style.zIndex='';
  }

  navigate(url:string){
    this.router.navigateByUrl(url);
  }
}
