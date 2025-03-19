import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Company } from '../models/company';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CompaniesService {

  baseUrl=environment.apiUrl;
    constructor(private http:HttpClient
      ) { }
  
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
