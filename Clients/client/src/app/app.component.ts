import { HttpClient } from '@angular/common/http';
import { Component, ElementRef, OnInit } from '@angular/core';
import { NavigationEnd, NavigationStart, Router } from '@angular/router';
import { OidcSecurityService } from 'angular-auth-oidc-client';
//import { AccountService } from './_modules/account/services/account.service';
import { ResizeWindowWatcherService } from './_services/resize-window-watcher.service';
import { RouteWatcherService } from './_services/route-watcher.service';
import { navbarItem } from './_modules/navbars/models/navbarItem';
import { AccountService } from './_modules/account/services/account.service';
import { filter, map } from 'rxjs/operators';
// import { TopNavbarComponent } from './_modules/navbars/topnavbar/topnavbar/topnavbar.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css','./shared/mobile-sidenav.css']
})
export class AppComponent implements OnInit {
  title = 'Shop';
  users:any;

  navbarItems:navbarItem[]=[];
  constructor(public accountService: AccountService, private router:Router
    ,private elem: ElementRef){
    this.createNavbarItems();
 
    
    // this.router.events.subscribe((event) => {
    //   if (event instanceof NavigationEnd) {
    //     //console.log('dasdas '+event.url)
    //     this.accountService.onAppInit();
    //   }
      
    // });
  }

  createNavbarItems(){
    var invoices=new navbarItem('Invoices','');
    invoices.subItems=[new navbarItem('Products','/products'),new navbarItem('Contractors','/contractors')]
    //ar aa=new navbarItem('test33','')
    this.navbarItems=[
      invoices,
      //new navbarItem('test',''){sub subItems=[aa]},
      new navbarItem('test2','')
    ]
  }

  ngOnInit() {
    // this.oidcSecurityService.checkAuth().subscribe((x) => {
    //   console.log("auth")
    //   console.log(x)
    //   // console.log(isAuthenticated)
    //   // console.log(userData)
    //   // console.log(accessToken)
    //   // console.log(idToken)
    // });
  }

  ngAfterViewInit(){
    //console.log(this.router.getCurrentNavigation())
    //setTimeout(()=>{this.accountService.onAppInit(); }, 1000)
    // this.accountService.onAppInit();
   
  }

  setCurrentActiveFormSection(){
    //get all form sections
    let sections = this.elem.nativeElement.querySelectorAll('.form-section');
    //get height of top nav bar to calculate offset
    let topNavBar = this.elem.nativeElement.querySelector("#top-navbar");
    var barHeight=topNavBar.getBoundingClientRect().height;
    console.log(barHeight)
    //if current view has form sections
    if(sections.length>0){
      //iterating through form sections to get first visible in view section to set active class
      for (let i = 0; i < sections.length; i++) {
        var elemHeight=sections[i].getBoundingClientRect().height;
        //section is visible if more than a half of it is in view
        if(elemHeight+sections[i].getBoundingClientRect().top-barHeight>elemHeight/2){
          let navItems = this.elem.nativeElement.querySelectorAll('.form-navigation-item');
          navItems.forEach((item:any) => {
            item.classList.remove('active');
          });
          if(navItems.length>i)
            navItems[i].classList.add('active');
          return;
        }
      }


      // var r=sections[0].getBoundingClientRect().bottom-sections[0].getBoundingClientRect().top-2*barHeight
      // //console.log(sections[0].getBoundingClientRect().top-barHeight+' '+(sections[0].getBoundingClientRect().bottom-barHeight))
      // console.log(sections[0].getAttribute('data-menu-item-id'))

      //console.log(sections[0].getBoundingClientRect())
//top-navbar
      // sections.forEach((element:any) => {
      //   console.log(element.getBoundingClientRect().top )
      // });
    }
    //console.log(sections.length)
  }
}
