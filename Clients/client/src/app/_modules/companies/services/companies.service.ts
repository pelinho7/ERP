import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Company } from '../models/company';
import { map } from 'rxjs/operators';
import { ReplaySubject } from 'rxjs';
import { isNumber } from 'ngx-bootstrap/chronos/utils/type-checks';

@Injectable({
  providedIn: 'root'
})
export class CompaniesService {

    private currentCompanySource=new ReplaySubject<Company>(1);
    currentCompany$=this.currentCompanySource.asObservable();
  baseUrl=environment.apiUrl;
    constructor(private http:HttpClient
      ) { 
        this.loadUserFromLocalStorage();
      }

    loadUserFromLocalStorage(){
        var companyIdInLocalStorage=localStorage.getItem('companyId');
        if(companyIdInLocalStorage!=null && companyIdInLocalStorage.length>0 && !isNaN(Number(companyIdInLocalStorage))){
          this.getById(Number(companyIdInLocalStorage)).subscribe(x=>{
            this.setCurrentCompany(x);
          })
        }
    }

    setCurrentCompany(company:Company){
      if(company!=null){
        localStorage.setItem('companyId',company.id.toString());
      }
      else{
        localStorage.removeItem('companyId');
      }
      this.currentCompanySource.next(company);
    }
  
    add(company:Company){
      return this.http.post<any>(this.baseUrl+'companies',company).pipe(
        map((company:Company)=>{
  
        }))
    }
  
    update(company:Company){
      return this.http.put<any>(this.baseUrl+'companies',company).pipe(
        map((company:Company)=>{
  
        }))
    }

    getById(id:number){
        return this.http.get<Company>(this.baseUrl+'companies/'+id).pipe(
          map((company:Company)=>{
            return company;
          }))
      }
}
