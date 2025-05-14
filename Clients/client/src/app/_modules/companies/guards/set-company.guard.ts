import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable, of } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { catchError, filter, map, take, tap } from 'rxjs/operators';
import { CompaniesService } from '../services/companies.service';
import { Company } from '../models/company';


@Injectable({
  providedIn: 'root'
})
export class SetCompanyGuard implements CanActivate {

    constructor(private companiesService:CompaniesService,private toastr: ToastrService
      ,private router:Router){}

    canActivate(): Observable<boolean>  {
      return this.companiesService.currentCompany$.pipe(
        take(1),
        map((company:Company)=>{
          if(company == null || company.companyCurrentUser == null) {
            this.toastr.info('Not set company.');
            this.router.navigateByUrl('/')

            return false;
          }
          return true;
        })
        )
    }
}
