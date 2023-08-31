import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Product } from '../product';
import { Pagination } from '../../utilities/models/pagination';
import { PagedList } from '../../utilities/models/pagedList';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {
  baseUrl=environment.apiUrl;
  constructor(private http:HttpClient) { }

  add(product:Product){
    console.log('user');
    return this.http.post<any>(this.baseUrl+'products',product).pipe(
      map((product:Product)=>{

      }))
  }

  update(product:Product){
    return this.http.put<any>(this.baseUrl+'products',product).pipe(
      map((product:Product)=>{

      }))
  }

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
    return path;//[path,params]
  }

  getList(name:string,pagination:Pagination){
    var path = this.createProductsUrlParams(name,pagination)
    return this.http.get<any>(this.baseUrl+'products'+path).pipe(
      map((products:PagedList<Product>)=>{
        return products;
      }))
  }

  getById(id:number){
    return this.http.get<Product>(this.baseUrl+'products/'+id).pipe(
      map((product:Product)=>{
        return product;
      }))
  }

  checkCodeNotTaken(code:string){
    return this.http.get<boolean>(this.baseUrl+'products/check-code-not-taken/'+code).pipe(
      map((result:boolean)=>{
        return result;
      })
    )
  }
}
