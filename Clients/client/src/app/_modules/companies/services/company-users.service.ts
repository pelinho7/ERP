import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, switchMap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { CompanyUser } from '../models/companyUser';
import { forkJoin, ReplaySubject } from 'rxjs';
import { CompaniesService } from './companies.service';
import { UserBase } from '../../account/models/userBase';
import { Archive } from '../../utilities/models/archive';

@Injectable({
  providedIn: 'root'
})
export class CompanyUsersService {

  private currentCompanyUsersSource=new ReplaySubject<CompanyUser[]>(1);
  currentCompanyUsers$=this.currentCompanyUsersSource.asObservable();
  expirationDataDate:Date=null;
  baseUrl=environment.apiUrl;
  constructor(private http:HttpClient,private companiesService:CompaniesService) { 
    //clear company users cached after company changed
    companiesService.currentCompany$.subscribe(c=>{
      this.currentCompanyUsersSource.next(null);
      this.expirationDataDate=null;
    })
  }

  cachedCompanyUsers(companyUsers: CompanyUser[]){
    this.currentCompanyUsersSource.next(companyUsers);
    //data is valif for 30 min
    this.expirationDataDate = new Date((new Date()).getTime() + 30 * 60 * 1000);
  }

  updateCachedCompanyUsers(companyUsers: CompanyUser[]){
    this.currentCompanyUsersSource.next(companyUsers);
  }
 
 getCompanyUsers(companyId:number,forecRequest:boolean=false) {
  //if cached data is actual return its
  if(!forecRequest && this.expirationDataDate!=null && this.expirationDataDate>(new Date())){
    return this.currentCompanyUsers$;
  }
  else{
    //get all company users and for all company users get email adress
    return this.http.get<CompanyUser[]>(this.baseUrl+'company-users?companyId='+companyId).pipe(
      switchMap((companyUsers: CompanyUser[]) => {
        let params = new HttpParams();
        companyUsers.forEach((cu) => {
          params = params.append('UserIds', cu.userId.toString());
        });
        return this.http.get<UserBase[]>(this.baseUrl+'users',{params}).pipe(
          map((users: UserBase[]) => {
            companyUsers.forEach(companyUser => {
              var user = users.find(x=>x.userId == companyUser.userId);
              if(user!=null){
                companyUser.userEmail=user.email;
              }
            });
            this.cachedCompanyUsers(companyUsers);
            return companyUsers;
          })
        );
      })
    );
  }
}

add(companyUser:CompanyUser){
  return this.http.post<any>(this.baseUrl+'company-users',companyUser).pipe(
    map((company:CompanyUser)=>{
      return companyUser;
    }))
}

update(companyUser:CompanyUser){
  return this.http.put<any>(this.baseUrl+'company-users',companyUser).pipe(
    map((companyUser:CompanyUser)=>{
      return companyUser;
    }))
}

archive(companyUserToArchive:Archive){
  return this.http.patch<any>(this.baseUrl+'company-users',companyUserToArchive).pipe(
    map((companyUser:any)=>{
      return companyUser;
    }))
}

}
