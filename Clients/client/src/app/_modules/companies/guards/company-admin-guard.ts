import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { CompaniesService } from '../services/companies.service';
import { ToastrService } from 'ngx-toastr';
import { map } from 'rxjs/operators';
import { Company } from '../models/company';

@Injectable({
  providedIn: 'root'
})
export class CompanyAdminGuard implements CanActivate {

    constructor(private companiesService:CompaniesService,private toastr: ToastrService
      ,private router:Router){}

    canActivate(): Observable<boolean>  {
      return this.companiesService.currentCompany$.pipe(
        map((company:Company)=>{

          if(company == null || company.companyCurrentUser == null || company.companyCurrentUser.administrator == false) {
            this.toastr.info('No permissions.');
            this.router.navigateByUrl('/')

            return false;
          }
          return true;
        })
        )
    }
  
}
