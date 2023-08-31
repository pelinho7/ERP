import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable, of } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { AccountService } from '../_modules/account/services/account.service';
import { User } from '../_modules/account/models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationGuard implements CanActivate {

  constructor(private accountService:AccountService,private toastr: ToastrService
    ,private router:Router){}


  //     async canActivate(route: ActivatedRouteSnapshot,
  //     state: RouterStateSnapshot): Promise<boolean>  {
  //       console.log('gggggggg')
        
  //       var user:User=null
  //       this.accountService.currentUser$.pipe(map((us:User)=>user=us));
  //       console.log('gggggggg11 '+user)
  //       if(user == null){
  //         this.accountService.login();
  //           return false;
  //       }
  //       // else{
  //       //   if(this.accountService.isTokenExpired(user.token)){
  //       //     //this.toastr.info('Token expired.');
  //       //     // var newToken:string='';
  //       //     // this.accountService.getAccessToken()..pipe(map((us:User)=>user=us));
  //       //     var newToken = await this.accountService.getAccessToken();
  //       //     if(newToken){
  //       //       return true;
  //       //     }
  //       //     else{
  //       //       return false;
  //       //     }
  //       //   }
  //       // }
  //       return true
  //   }
  // }

  canActivate(route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean>  {
    // this.accountService.login();
    // var user = await this.accountService.currentUser$.toPromise()
    // this.accountService.onAppInit();
    // console.log(state.url)
    // console.log(route)
    // return of(false);

    return this.accountService.currentUser$.pipe(
      map((user:User)=>{
        console.log('AuthenticationGuard')
        if(user) {
          if(this.accountService.isTokenExpired(user.token)){
            //this.toastr.info('Token expired.');
            var newToken='';
            this.accountService.getAccessToken().pipe(map((t:string)=>newToken=t));
            if(this.accountService.isTokenExpired(user.token)){
              this.toastr.info('Token expired.');
              return false;
            }
            return true;
          }
          else
            return true;
        }
        else{
          this.accountService.login(state.url);
          return false;
        }
        // this.toastr.error('You shall not pass!');
        // return false;
      })
      )
  }
}
