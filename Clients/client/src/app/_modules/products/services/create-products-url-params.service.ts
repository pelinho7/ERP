import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Pagination } from '../../utilities/models/pagination';

@Injectable({
  providedIn: 'root'
})
export class CreateProductsUrlParamsService {

  constructor() { }

  createProductsUrlParams(name:string,pagination:Pagination){
    let path:string='';
    let params=new HttpParams();
    if(name.length>0){
      path = '?name='+name
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
