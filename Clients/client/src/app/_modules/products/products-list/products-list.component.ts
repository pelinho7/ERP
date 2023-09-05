import { Component, OnInit } from '@angular/core';
import { ProductsService } from '../services/products.service';
import { LoadingScreenService } from '../../utilities/services/loading-screen.service';
import { Pagination } from '../../utilities/models/pagination';
import { ResizeWindowWatcherService } from '../../navbars/services/resize-window-watcher.service';
import { ActivatedRoute, Router } from '@angular/router';
import { UrlParamsService } from '../../utilities/services/url-params.service';
import { Product } from '../product';
import { CreateProductsUrlParamsService } from '../services/create-products-url-params.service';
import { ConfirmService } from '../../message-dialogs/services/confirm.service';
import { Archive } from '../../utilities/models/archive';

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
    ,private confirmService:ConfirmService
    ) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params =>{
      this.pagination=new Pagination();

      console.log(this.pagination)
      if(params.name){
        this.nameFilterInput=params.name
      }
      else{
        this.nameFilterInput='';
      }

      this.pagination.castJsonToClass((params as Pagination))

      this.loadProducts();
    });
  }

  onEnterNameFilerInput(event:KeyboardEvent ){
    if(!this.loaded)return;

    if(event.key == 'Enter'){
      this.navigate(this.nameFilterInput,new Pagination());
    }
  }

  navigate(name:string,pagination:Pagination){
    var path = this.createProductsUrlParamsService.createProductsUrlParams(name,pagination)
    this.loaded=false;
    this.router.navigateByUrl('/products'+path)
  }

  onBlurNameFilerInput(){
    this.navigate(this.nameFilterInput,new Pagination());
  }

  loadProducts(){
    this.nameFilter=this.nameFilterInput;
      this.loadingScreenService.show();

      this.productsService.getList(this.nameFilterInput,this.pagination).subscribe(x=>{
        this.pagination=x.pagination;

        this.items=x.items;
        
        this.loaded=true;
      })
      .add(()=>{
        this.loadingScreenService.hide();
      })
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

  archive(){
    this.confirmService.confirm('Archiving of items', 'Are you sure you want to archive the selected items ('+this.selectedIds.length+') ?').subscribe(result=>{

      if(result){
        this.loadingScreenService.show();
        var productsToArchive = this.selectedIds.map(x => <Archive>{
          id: x
        });

        this.productsService.archive(productsToArchive).subscribe(x=>{
          // this.navigate(this.nameFilterInput,this.pagination);
          this.loadProducts();
        })
        .add(()=>{
          this.loadingScreenService.hide();
        })
      }
    })
  }

  pageChanged(event:any){
    if(!this.loaded || event.page == this.pagination.page)return;

    this.pagination.page=event.page;
    this.navigate(this.nameFilterInput,this.pagination);
  }
}
