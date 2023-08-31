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
  constructor(public oidcSecurityService: OidcSecurityService,private http:HttpClient
    ,private eventService: PublicEventsService,public accountService:AccountService) { 
      this.eventService
    .registerForEvents()
    .pipe(filter((notification) => notification.type === EventTypes.TokenExpired))
    .subscribe((value) =>{
    });
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

  

  

  async get(){
    //var auth = await this.oidcSecurityService.isAuthenticated$.toPromise()
    this.oidcSecurityService.isAuthenticated$.subscribe(x=>console.log(x.isAuthenticated))
    var accessToken = await this.oidcSecurityService.getAccessToken().toPromise();
    //console.log(auth.isAuthenticated)
    console.log(accessToken)
    // this.oidcSecurityService.forceRefreshSession().subscribe(x=>{
    //   console.log(x)
    // })
  // this.http.get('https://localhost:5000/Contractors/1')
  // .subscribe(x=>console.log(x))

  // this.oidcSecurityService.getConfiguration().subscribe(x=>{console.log('qqqqqqqq');console.log(x)});  console.log('111');

  // this.oidcSecurityService.getConfiguration()
  //     .pipe(
  //       switchMap((config: OpenIdConfiguration) => {
  //         const userManagerSettings = {
  //           authority: config.authority,
  //           client_id: config.clientId,
  //           redirect_uri: config.redirectUrl,
  //           post_logout_redirect_uri: config.postLogoutRedirectUri,
  //           response_type: config.responseType,
  //           scope: config.scope
  //         };
  //         const userManager = new Oidc.UserManager(userManagerSettings);
  //         // tutaj możesz wykonać operacje na userManager
  //         return of(userManager);
  //       })
  //     )
  //     .subscribe((userManager: Oidc.UserManager) => {
  //       userManager.events.addAccessTokenExpiring((error) => {
  //         console.log('Token expired. User needs to login again.');
  //         // Tutaj możesz wywołać swoją funkcję do ponownej autoryzacji użytkownika
  //       });
  //       // tutaj możesz wykonać dodatkowe operacje na userManager
  //     }
  //     );
}

private startRefreshTokenTimer() {
  //const refreshTokenInterval = this.oidcSecurityService.getConfig().refreshTokenExpiryInSeconds * 1000 / 2;

  // this.refreshTokenSubscription = interval(refreshTokenInterval)
  //   .pipe(
  //     takeWhile(() => this.oidcSecurityService.getIsAuthorized())
  //   )
  //   .subscribe(() => {
  //     this.oidcSecurityService.forceRefreshSession();
  //   });
}
}
