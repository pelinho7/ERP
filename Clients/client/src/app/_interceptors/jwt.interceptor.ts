import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { Router } from '@angular/router';
import { AccountService } from '../_modules/account/services/account.service';
import { User } from '../_modules/account/models/user';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private accountService: AccountService) {console.log('request')}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    // request=request.clone({
    //   setHeaders:{
    //     Authorization:`Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6IkIzQ0ZCMkE5Qjg2QzBDQjJDMDRDQjc2N0U0RUUyMUQ2IiwidHlwIjoiYXQrand0In0.eyJuYmYiOjE2Nzk1MTEzOTEsImV4cCI6MTY3OTUxNDk5MSwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NTAwMSIsImNsaWVudF9pZCI6Im1vdmllc19tdmNfY2xpZW50Iiwic3ViIjoiMSIsImF1dGhfdGltZSI6MTY3OTUwNzE0OCwiaWRwIjoibG9jYWwiLCJlbWFpbCI6InBhdHJ5ay5wZWxlQG9wLnBsIiwianRpIjoiNDg5MTM2QzJGNjNCQTkwOEQ4OUNFMzJCRTFCQkU4MjUiLCJzaWQiOiIzRkRDQzg2Rjc0MDJGNzUxNzZENTc1MjU3OUIzNTc1MyIsImlhdCI6MTY3OTUxMTM5MSwic2NvcGUiOlsib3BlbmlkIiwicHJvZmlsZSIsIm1vdmllQVBJIl0sImFtciI6WyJwd2QiXX0.hEQiyEMP1UjjQ_ThGMYfhWY5mYdZTVLdBmm6NYdAm3KEM71gE_owlmibyCQEEXqnugdnJDoF7FpWVQlHU8CVqWznRmDCrL3g0bstPONYwefZYgw9JgUWnh7mO5iisFiHaXyeKC3IvMBbkTfpyHbQxhQ8Xyz8pB1kPAQXVHO6WQMNHCu7k2u-UYFcjdZ-Hvk_s5ex1iT_tJPaOkmOle0_2m4_gr5l0bs4uVVxk2YnOakHIpNvCHn3wif3c0rfK21xjSQY45f3c-UJHG57nVcIW4udBsQG893Qxo7qMihfkSc5v749pFGexTN62C3C2Wv5p_A7JthL7oxsb2pqsKla_g`
    //   }
    // })

    let currentUser:User;
    this.accountService.currentUser$.subscribe(user=>{
      currentUser=user
    });

    if(currentUser){
      console.log('currentUser.token')
      console.log(currentUser.token)
      request=request.clone({
        setHeaders:{
          Authorization:`Bearer ${currentUser.token}`
        }
      })
    }
    // console.log(request)
    return next.handle(request);
  }
}
