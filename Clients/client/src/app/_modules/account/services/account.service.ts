import { Injectable } from '@angular/core';
import { Observable, of, ReplaySubject } from 'rxjs';
import { User } from '../models/user';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { NavigationEnd, Router } from '@angular/router';
import { catchError, map, take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { UrlService } from 'src/app/_services/url.service';
import { AccountData } from '../models/accountData';


@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private currentUserSource=new ReplaySubject<User>(1);
  currentUser$=this.currentUserSource.asObservable();
  baseUrl=environment.apiUrl;

      constructor(private router:Router
        ,private http:HttpClient, private toastr:ToastrService
        ,private urlService:UrlService
      ) {   
        this.loadUserFromLocalStorage()
      }
  
      logIn(model:any){
          return this.http.post<User>(this.baseUrl+'account/login',model).pipe(
            map((user:User)=>{
              if(user !== null){
                // console.log(user)
                this.setCurrentUser(user);
              }
            })
          )
        }

        logout(){
          localStorage.removeItem('user');
          this.currentUserSource.next(null);
        }

        setCurrentUser(user:User){
          // const roles =this.getDecodedToken(user.token).role;
          localStorage.setItem('user',JSON.stringify(user));
          this.currentUserSource.next(user);
        }

        loadUserFromLocalStorage(){
          var userInLocalStorage=localStorage.getItem('user');
          if(userInLocalStorage!=null){
            var user=JSON.parse(userInLocalStorage);
            if(this.isTokenExpired(user.token)){
              this.logout();
            }
            else{
              this.setCurrentUser(JSON.parse(userInLocalStorage))
            }
          }
        }

        isTokenExpired(token:string){
          const expiry = (this.getDecodedToken(token)).exp;
          var ex=(Math.floor((new Date).getTime() / 1000)) >= expiry;
          return ex;
        }

        getDecodedToken(token:string){
          //atob()decode data in Base64 
          return JSON.parse(atob(token.split('.')[1]))
        }

        redirectIfTokenExpired(){
          let cUser:User=null;
          this.currentUser$.pipe(take(1)).subscribe(x=>cUser=x);
          if(cUser) {
            if(this.isTokenExpired(cUser.token)){
              this.toastr.info('Token expired.');
              this.urlService.setPreviousUrl(this.router.url);
              this.logout();
              this.router.navigateByUrl('account/login')
              return true;
            }
            else{
              return false;
            }
          }
          this.router.navigateByUrl('account/login')
          return true;
        }
      
        register(model:any){
          return this.http.post<any>(this.baseUrl+'account/register',model).pipe(
            map((user:any)=>{
              console.log(user);
              // if(user !== null){
              //   this.setCurrentUser(user);
              // }
            })
          )
        }
      
        checkEmailNotTaken(email:string,id:number){
          if(email.length==0){
            return of(false)
          }
          var idParam='';
          if(id!=null){
            idParam='&id='+id;
          }
          return this.http.get<boolean>(this.baseUrl+'account/check-email-not-taken?email='+email+idParam).pipe(
            map((result:boolean)=>{
              return result;
            })
          )
        }
      
        checkLoginNotTaken(login:string){
          return this.http.get<boolean>(this.baseUrl+'account/check-login-not-taken?login='+login).pipe(
            map((result:boolean)=>{
              return result;
            })
          )
        }
      
        verifyEmail(data: string) { 
          return this.http.get<User>(this.baseUrl + data).pipe(
            map((user:User)=>{
              this.setCurrentUser(user);
              return user;
            })
          )
        }
      
        resendVerificationEmail(user:any) { 
          return this.http.post(this.baseUrl+'account/resend-verification-email',user);
        }
      
        resetPassword(login:string) { 
          return this.http.get(this.baseUrl+'account/reset-password?email='+login).pipe(
            map(_ => {
              return true;
            }),
            catchError(err => {
              this.toastr.error(err);
              return of(false);
            })
          )
        }
      
        newPassword(model:any){
          return this.http.post(this.baseUrl+'account/new-password',model);
        }
      
        getAccountData(){
          return this.http.get<AccountData>(this.baseUrl+'account/data').pipe(
            map((data:AccountData)=>{
              return data;
            })
          )
        }
      
        updateAccountData(model:any){
          return this.http.post<User>(this.baseUrl+'account/data',model).pipe(
            map((user:User)=>{
              //reload username after update
                this.currentUser$.pipe(take(1)).subscribe(currentU=>{
                  currentU.email=user.email;
                  this.setCurrentUser(currentU);
                })
                return user;
              })
          )
        }
      
        changePassword(model:any){
          return this.http.post(this.baseUrl+'account/change-password',model);
        }

        getUsers(filter:string){
          return this.http.get<User>(this.baseUrl+'users?filter='+filter).pipe(
            map((result:User)=>{
              return result;
            })
          )
        }
}