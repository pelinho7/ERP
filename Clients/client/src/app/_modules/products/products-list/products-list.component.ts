import { Component, OnInit } from '@angular/core';
import { ProductsService } from '../services/products.service';
import { LoadingScreenService } from '../../utilities/services/loading-screen.service';
import { Pagination } from '../../utilities/models/pagination';
import { ResizeWindowWatcherService } from '../../navbars/services/resize-window-watcher.service';
import { ActivatedRoute, Router } from '@angular/router';
import { UrlParamsService } from '../../utilities/services/url-params.service';
import { Product } from '../product';
import { CreateProductsUrlParamsService } from '../services/create-products-url-params.service';

@Component({
  selector: 'app-products-list',
  templateUrl: './products-list.component.html',
  styleUrls: ['./products-list.component.css','../../../shared css/list-item.css']
})
export class ProductsListComponent implements OnInit {

  pagination: Pagination;
  items:Product[];
  paginationPanelStyles = new Map<string, string>();
  nameFilterInput:string=''
  nameFilter:string=''
  loaded:boolean=false;
  selectMode:boolean=false;
  selectedIds:number[]=[];

  constructor(private productsService:ProductsService
    ,private loadingScreenService:LoadingScreenService
    ,public resizeWindowWatcherService:ResizeWindowWatcherService
    ,private router: Router
    ,private route: ActivatedRoute
    ,private urlParamsService:UrlParamsService
    ,private createProductsUrlParamsService:CreateProductsUrlParamsService
    ) { }

  ngOnInit(): void {
    //this.router.routeReuseStrategy.shouldReuseRoute = () => false;
    this.route.queryParams.subscribe(params =>{
      this.pagination=new Pagination();
      if(params.name){
        this.nameFilterInput=params.name
      }
      else{
        this.nameFilterInput='';
      }
      //this.urlParamsService.urlParams2Object(params)
      // this.pagination.page=
      this.pagination.castJsonToClass((params as Pagination))

      console.log(this.pagination)
      this.loadProducts();
    });


    // var urlParam = '';
    // var urlWithoutParams = '';
    // this.pagination=new Pagination();
    // if (this.router.url.includes('?')) {
    //   var paramIndexStart = this.router.url.indexOf('?');
    //   urlParam = this.router.url.substring(paramIndexStart + 1);
    //   urlWithoutParams = this.router.url.substring(0, paramIndexStart);
    //   urlParam.split('&')
      
    //   var urlObject = this.urlParamsService.urlParams2Object(urlParam)
    //   this.pagination.castJsonToClass(urlObject)
    //   if(urlObject.name){
    //     this.nameFilterInput=urlObject.name;
    //   }
    // }
    // else {
    //   urlWithoutParams = this.router.url;
    // }
    
    

    
   // this.loadProducts();

   

  }

  onEnterNameFilerInput(event:KeyboardEvent ){
    if(!this.loaded)return;

    if(event.key == 'Enter'){
      this.loadProducts();
    }
  }

  navigate(){
    var path = this.createProductsUrlParamsService.createProductsUrlParams(this.nameFilterInput,this.pagination)
    this.router.navigateByUrl('/products'+path)
  }

  onBlurNameFilerInput(){
    //this.loadProducts();
    this.navigate();
  }

  loadProducts(){
    if(/*this.nameFilterInput!=this.nameFilter || !this.loaded*/true){
      this.nameFilter=this.nameFilterInput;
      this.loadingScreenService.show();
      this.productsService.getList(this.nameFilterInput,this.pagination).subscribe(x=>{
        this.pagination=x.pagination;


        this.items=x.items;
        
        this.loaded=true;
        console.log(this.pagination)
      })
      .add(()=>{
        this.loadingScreenService.hide();
      })
    }
  }

  selectChanged(productId:number){
    if(this.selectedIds.includes(productId)){
      this.selectedIds.splice(this.selectedIds.indexOf(productId),1)
    }
    else{
      this.selectedIds.push(productId)
    }
    this.selectMode=this.selectedIds.length>0;
  }

  clearSelections(){
    this.selectedIds=[];
    this.selectMode=false;
  }

  delete(){

  }

  pageChanged(event:any){
    this.navigate();
  }
}
