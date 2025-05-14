import { Component, OnInit } from '@angular/core';
import { Pagination } from '../../utilities/models/pagination';
import { Contractor } from '../contractor';
import { ActivatedRoute, Router } from '@angular/router';
import { UrlParamsService } from '../../utilities/services/url-params.service';
import { ConfirmService } from '../../message-dialogs/services/confirm.service';
import { CreateProductsUrlParamsService } from '../../products/services/create-products-url-params.service';
import { ResizeWindowWatcherService } from '../../navbars/services/resize-window-watcher.service';
import { LoadingScreenService } from '../../utilities/services/loading-screen.service';
import { ContractorsService } from '../services/contractors.service';
import { Archive } from '../../utilities/models/archive';
import { CreateContractorsUrlParamsService } from '../services/create-contractors-url-params.service';
import { CompaniesService } from '../../companies/services/companies.service';
import { switchMap, take, tap } from 'rxjs/operators';
import { Company } from '../../companies/models/company';

@Component({
  selector: 'app-contractors-list',
  templateUrl: './contractors-list.component.html',
  styleUrls: ['./contractors-list.component.css','../../../shared css/list-item.css']
})
export class ContractorsListComponent implements OnInit {

  pagination: Pagination;
  items:Contractor[];
  paginationPanelStyles = new Map<string, string>();
  nameFilterInput:string=''
  nameFilter:string=''
  loaded:boolean=false;
  selectMode:boolean=false;
  selectedIds:number[]=[];

  constructor(private contractorsService:ContractorsService
    ,private loadingScreenService:LoadingScreenService
    ,public resizeWindowWatcherService:ResizeWindowWatcherService
    ,private router: Router
    ,private route: ActivatedRoute
    ,private urlParamsService:UrlParamsService
    ,private createContractorsUrlParamsService:CreateContractorsUrlParamsService
    ,private confirmService:ConfirmService,
    private companiesService:CompaniesService
    ) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params =>{
      this.pagination=new Pagination();

      console.log(this.pagination)
      if(params.filter){
        this.nameFilterInput=params.filter
      }
      else{
        this.nameFilterInput='';
      }

      this.pagination.castJsonToClass((params as Pagination))

      this.loadContractors();
    });
  }

  onEnterNameFilerInput(event:KeyboardEvent ){
    if(!this.loaded)return;

    if(event.key == 'Enter'){
      this.navigate(this.nameFilterInput,new Pagination());
    }
  }

  navigate(name:string,pagination:Pagination){
    var path = this.createContractorsUrlParamsService.createContractorsUrlParams(null,name,pagination)
    this.loaded=false;
    this.router.navigateByUrl('/contractors'+path)
  }

  onBlurNameFilerInput(){
    this.navigate(this.nameFilterInput,new Pagination());
  }

  loadContractors(){
    // var companyId:number;
    // this.companiesService.currentCompany$.pipe(
    //   take(1)
    // ).subscribe(x=>
    // {
    //     companyId=x.id
    // })  

    // this.nameFilter=this.nameFilterInput;
    //   this.loadingScreenService.show();

    //   this.contractorsService.getList(companyId,this.nameFilterInput,this.pagination).subscribe(x=>{
    //     this.pagination=x.pagination;

    //     this.items=x.items;
        
    //     this.loaded=true;
    //   })
    //   .add(()=>{
    //     this.loadingScreenService.hide();
    //   })


    this.companiesService.currentCompany$.pipe(
      tap(company => {
        this.nameFilter=this.nameFilterInput;
      this.loadingScreenService.show();

      this.contractorsService.getList(company.id,this.nameFilterInput,this.pagination).subscribe(x=>{
        this.pagination=x.pagination;

        this.items=x.items;
        
        this.loaded=true;
      })
      .add(()=>{
        this.loadingScreenService.hide();
      })
      })
    ).subscribe();
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

        this.contractorsService.archive(productsToArchive).subscribe(x=>{
          this.loadContractors();
        })
        .add(()=>{
          this.clearSelections();
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
