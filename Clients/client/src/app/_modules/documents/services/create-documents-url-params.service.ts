import { Injectable } from '@angular/core';
import { Pagination } from '../../utilities/models/pagination';
import { HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CreateDocumentsUrlParamsService {

  constructor() { }

  createDocumentsUrlParams(filter:string,pagination:Pagination){
    let path:string='';
    let params=new HttpParams();
    if(filter.length>0){
      path = '?filter='+filter
    }
    if(path.length>0){
      path = '?' + path.substring(1);
    }
    if(pagination && pagination.page>1){
      let page='page='+pagination.page;
      if(path.length==0){
        path += '?';
      }
      else{
        path += '&';
      }
      path+=page;
    }
    return path;
  }
}
