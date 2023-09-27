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

  // canActivate(route: ActivatedRouteSnapshot,
  //   state: RouterStateSnapshot): Observable<boolean>  {
  //   return this.accountService.currentUser$.pipe(
  //     map((user:User)=>{
  //       console.log('AuthenticationGuard')
  //       if(user) {
  //         if(this.accountService.isTokenExpired(user.token)){

  //           var newToken='';
  //           console.log(user.token)
  //           this.accountService.getAccessToken().pipe(map((t:string)=>newToken=t));
  //           this.accountService.getAccessToken().subscribe(x=>{
  //             console.log('xxxxxxxxxx')
  //             console.log(x)
  //           }
  //           ,
  //           ()=>{}
  //           ,
  //           ()=>{
  //             if(this.accountService.isTokenExpired(user.token)){
  //               this.toastr.info('Token expired.');
  //               return false;
  //             }
  //             return true;
  //           })

  //           return true
           
  //         }
  //         else
  //           return true;
  //       }
  //       else{
  //         this.accountService.login(state.url);
  //         return false;
  //       }
  //     })
  //     )
  // }

  canActivate(route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> {
    var u:User;

    const sub = this.accountService.currentUser$.pipe(take(1)).subscribe(value => u = value);
    sub.unsubscribe();
    if(u) {
      if(this.accountService.isTokenExpired(u.token)){
        //this.toastr.info('Token expired.');
        var newToken='';
        console.log(u.token)
        this.accountService.getAccessToken().subscribe(x=>{
        }
        ,
        ()=>{}
        ,
        ()=>{
          if(this.accountService.isTokenExpired(u.token)){
            this.toastr.info('Token expired.');
            return false;
          }
          return true;
        })

        return of(true)
       
      }
      else
        return of(true);
    }
    else{
      console.log('fffffffffffffffffffffffff')
      this.accountService.login(state.url);
      return of(false);
    }
    
  }
}
