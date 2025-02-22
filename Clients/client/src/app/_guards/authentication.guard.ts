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

  canActivate(): Observable<boolean>  {
    return this.accountService.currentUser$.pipe(
      map((user:User)=>{
        if(user) {
          if(this.accountService.isTokenExpired(user.token)){
            this.toastr.info('Token expired.');
            this.accountService.logout();
            this.router.navigateByUrl('account/login')
            return false;
          }
          return true;
        }
        this.toastr.error('You shall not pass!');
        return false;
      })
      )
  }
}
