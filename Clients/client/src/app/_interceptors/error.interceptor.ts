import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router,private toastr:ToastrService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError(error=>{
        if(error){
          switch(error.status){
            case 400:
              //jeżeli jest to błąd z walidacji to posiada on słownik klucz błąd z którego wyciągane są jedynie 
              //wartości (komunikaty)
              if(error.error.errors){
                const modalStateErrors=[];
                for(const key in error.error.errors){
                  if(error.error.errors[key]){
                    modalStateErrors.push(error.error.errors[key]);
                  }
                }
                throw modalStateErrors.flat();
              }
              else if(typeof(error.error) === 'object'){ 
                this.toastr.error('Bad request',error.status);
              }
              else{
                this.toastr.error(error.error,error.status);
              }
            break;

            case 401:
              console.log(error.error)
              let message:string='';
              if(error.error.title === undefined){
                message=error.error;
              }
              else{
                message=error.error.title;
              }
             
              this.toastr.error(message,error.status);
              break;

            case 404:
              this.router.navigateByUrl('/not-found');
              break;

            case 500:
              const navigationExtras: NavigationExtras = {state: {error: error.error}}
              this.router.navigateByUrl('/server-error',navigationExtras);
              break;
            default:
              this.toastr.error('Somthing unexpected went wrong.');
              console.log(error);
              break;
          }
        }
        return throwError(error);
      })
    );
  }
}
