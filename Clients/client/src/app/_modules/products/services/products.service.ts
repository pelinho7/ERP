import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Product } from '../product';
import { Pagination } from '../../utilities/models/pagination';
import { PagedList } from '../../utilities/models/pagedList';
import { CreateProductsUrlParamsService } from './create-products-url-params.service';
import { Archive } from '../../utilities/models/archive';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {
  baseUrl=environment.apiUrl;
  constructor(private http:HttpClient
    ,private createProductsUrlParamsService:CreateProductsUrlParamsService) { }

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

  archive(productsToArchive:Archive[]){
    return this.http.patch<any>(this.baseUrl+'products',productsToArchive).pipe(
      map((product:any)=>{

      }))
  }

  
  getList(name:string,pagination:Pagination){
    var path = this.createProductsUrlParamsService.createProductsUrlParams(name,pagination)
    return this.http.get<any>(this.baseUrl+'products'+path).pipe(
      map((products:PagedList<Product>)=>{
        return products;
      }))
  }

  getAll(){
    return this.http.get<any>(this.baseUrl+'products/all').pipe(
      map((products:Product[])=>{
        return products;
      }))
  }

  getById(id:number){
    return this.http.get<Product>(this.baseUrl+'products/'+id).pipe(
      map((product:Product)=>{
        return product;
      }))
  }

  checkCodeNotTaken(code:string,id:number){
    var idParam='';
    if(id!=null){
      idParam='?id='+id;
    }
    return this.http.get<boolean>(this.baseUrl+'products/check-code-not-taken/'+code+idParam).pipe(
      map((result:boolean)=>{
        return result;
      })
    )
  }
}
