import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { CreateDocumentsUrlParamsService } from './create-documents-url-params.service';
import { Archive } from '../../utilities/models/archive';
import { Pagination } from '../../utilities/models/pagination';
import { PagedList } from '../../utilities/models/pagedList';
import { Document } from '../document';

@Injectable({
  providedIn: 'root'
})
export class DocumentsService {

  baseUrl=environment.apiUrl;
  constructor(private http:HttpClient
    ,private createDocumentsUrlParamsService:CreateDocumentsUrlParamsService) { }

  add(document:Document){
    return this.http.post<any>(this.baseUrl+'documents',document).pipe(
      map((document:Document)=>{

      }))
  }

  update(document:Document){
    return this.http.put<any>(this.baseUrl+'documents',document).pipe(
      map((document:Document)=>{

      }))
  }

  archive(documentsToArchive:Archive[]){
    return this.http.patch<any>(this.baseUrl+'documents',documentsToArchive).pipe(
      map((document:any)=>{

      }))
  }

  
  getList(name:string,pagination:Pagination){
    var path = this.createDocumentsUrlParamsService.createDocumentsUrlParams(name,pagination)
    return this.http.get<any>(this.baseUrl+'documents'+path).pipe(
      map((documents:PagedList<Document>)=>{
        return documents;
      }))
  }

  getById(id:number){
    return this.http.get<Document>(this.baseUrl+'documents/'+id).pipe(
      map((document:Document)=>{
        return document;
      }))
  }
}
