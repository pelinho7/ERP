import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { User } from '../models/user';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { NavigationEnd, Router } from '@angular/router';
import { map } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private currentUserSource=new ReplaySubject<User>(1);
  currentUser$=this.currentUserSource.asObservable();
  constructor(private oidcSecurityService:OidcSecurityService, private router:Router) { 
    this.currentUserSource.next(null);
    console.log('AccountService')
  }

  loadUserFromStorage(){
    let userJson=localStorage.getItem('user');
    if(userJson){
      let user:User = JSON.parse(userJson);
      this.setCurrentUser(user);
    }
  }

  setLoginRedirectUrl(redirectUrl:string=null){
    if(redirectUrl == null || redirectUrl?.length == 0)
      redirectUrl=this.router.url;
    
    sessionStorage.setItem('loginRedirectUrl',redirectUrl)
  }

  getLoginRedirectUrl():string{
    return sessionStorage.getItem('loginRedirectUrl')
  }

  login(url:string=null){
    sessionStorage.setItem('authorized','true');
    this.setLoginRedirectUrl(url);
    this.oidcSecurityService.authorize();
  }

  logout(){
    this.oidcSecurityService.logoff().subscribe((result) => {
      localStorage.removeItem('user');
      this.currentUserSource.next(null);
    });
  }

  checkAuth(){
    var redirectUrl = this.getLoginRedirectUrl();

    if(redirectUrl!=null){
      this.oidcSecurityService.checkAuth().subscribe((x) => {

        if(x.isAuthenticated){
          this.accessTokenToUser(x.accessToken);
          if(redirectUrl == null || redirectUrl == 'null'){
            redirectUrl='';
          }

          sessionStorage.setItem('loginRedirectUrl',redirectUrl)

          this.router.navigateByUrl(redirectUrl)
        }
      });
    }
  }

  async accessTokenToUser(token:string){
    var decodedToken=this.getDecodedToken(token);
    let tokenJsonValue = JSON.stringify(decodedToken);
    let user:User = (JSON.parse(tokenJsonValue) as User);
    user.token=token;
    this.setCurrentUser(user);
  }

  onAppInit(){
    if(document.cookie.indexOf('idsrv.session=')>=0){
      console.log('onAppInit 1')
      if(!sessionStorage.getItem('authorized')){
        console.log('onAppInit 2')
        // sessionStorage.setItem('authorized','true');
        this.login();
      }
      else{
        sessionStorage.removeItem('loginRedirectUrl')
      }
    }
  }

  async isAuthenticated(){
    var auth = await this.oidcSecurityService.isAuthenticated().toPromise();
    console.log('isAuthenticated '+auth)
    return auth;
  }

  getAccessToken(){
    return this.oidcSecurityService.getAccessToken().pipe(map((accessToken:string)=>{
      this.accessTokenToUser(accessToken);
      return accessToken;
    }))
    // var accessToken = await this.oidcSecurityService.getAccessToken().toPromise();
    // this.accessTokenToUser(accessToken);
    // console.log(accessToken)
    // return accessToken;
  }

  async get(){
    if(document.cookie.indexOf('idsrv.session=')>=0){
      if(!sessionStorage.getItem('authorized')){
        sessionStorage.setItem('authorized','true');
        this.login();
      }
    }
  }

  setCurrentUser(user:User){
    localStorage.setItem('user',JSON.stringify(user));
    this.currentUserSource.next(user);
  }

  getDecodedToken(token:string){
    //atob()decode data in Base64 
    return JSON.parse(atob(token.split('.')[1]))
  }

  isTokenExpired(token:string){
    const expiry = (this.getDecodedToken(token)).exp;
    var ex=(Math.floor((new Date).getTime() / 1000)) >= expiry;
    return ex;
  }
}
