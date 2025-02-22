import { Injectable } from '@angular/core';
import { CreateContractorsUrlParamsService } from './create-contractors-url-params.service';
import { Contractor } from '../contractor';
import { Pagination } from '../../utilities/models/pagination';
import { PagedList } from '../../utilities/models/pagedList';
import { map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Archive } from '../../utilities/models/archive';

@Injectable({
  providedIn: 'root'
})
export class ContractorsService {

  baseUrl=environment.apiUrl;
  constructor(private http:HttpClient
    ,private createContractorsUrlParamsService:CreateContractorsUrlParamsService) { }

  add(contractor:Contractor){
    return this.http.post<any>(this.baseUrl+'contractors',contractor).pipe(
      map((contractor:Contractor)=>{

      }))
  }

  update(contractor:Contractor){
    return this.http.put<any>(this.baseUrl+'contractors',contractor).pipe(
      map((contractor:Contractor)=>{

      }))
  }

  archive(contractorsToArchive:Archive[]){
    return this.http.patch<any>(this.baseUrl+'contractors',contractorsToArchive).pipe(
      map((contractor:any)=>{

      }))
  }

  
  getList(name:string,pagination:Pagination){
    var path = this.createContractorsUrlParamsService.createContractorsUrlParams(name,pagination)
    console.log(path)
    console.log(name)
    return this.http.get<any>(this.baseUrl+'contractors'+path).pipe(
      map((contractors:PagedList<Contractor>)=>{
        console.log(contractors)

        return contractors;
      }))
  }

  getAll(){
    return this.http.get<any>(this.baseUrl+'contractors/all').pipe(
      map((contractors:Contractor[])=>{
        return contractors;
      }))
  }

  getById(id:number){
    return this.http.get<Contractor>(this.baseUrl+'contractors/'+id).pipe(
      map((contractor:Contractor)=>{
        return contractor;
      }))
  }

  checkCodeNotTaken(code:string,id:number){
    var idParam='';
    if(id!=null){
      idParam='?id='+id;
    }
    return this.http.get<boolean>(this.baseUrl+'contractors/check-code-not-taken/'+code+idParam).pipe(
      map((result:boolean)=>{
        return result;
      })
    )
  }
}
