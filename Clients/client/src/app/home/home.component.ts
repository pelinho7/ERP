import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { MediaChange, MediaObserver } from '@angular/flex-layout';
import { Subscription } from 'rxjs/internal/Subscription';
import { distinctUntilChanged, filter, map, switchMap } from 'rxjs/operators';
import { ResizeWindowWatcherService } from '../_services/resize-window-watcher.service';
import { EventTypes, OidcSecurityService, OpenIdConfiguration, PublicEventsService } from 'angular-auth-oidc-client';
import { HttpClient } from '@angular/common/http';
import * as Oidc from 'oidc-client';
import { of } from 'rxjs';
import { AccountService } from '../_modules/account/services/account.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css','./../shared/mobile-sidenav.css']
})
export class HomeComponent implements OnInit {
  mobileScreen=false;
  constructor(private http:HttpClient
    ,public accountService:AccountService) { 
      
    }

  // @HostListener('window:resize', ['$event'])onResize($event:any) {
  //   if(992>$event.currentTarget.innerWidth){
  //     this.mobileScreen=true;
  //   }
  //   else{
  //     this.mobileScreen=false;
  //   }
  // }

  ngOnInit(): void {

  }
}
