import { Component, OnInit } from '@angular/core';
import { Pagination } from '../../utilities/models/pagination';
import { LoadingScreenService } from '../../utilities/services/loading-screen.service';
import { ResizeWindowWatcherService } from '../../navbars/services/resize-window-watcher.service';
import { ActivatedRoute, Router } from '@angular/router';
import { UrlParamsService } from '../../utilities/services/url-params.service';
import { ConfirmService } from '../../message-dialogs/services/confirm.service';
import { Archive } from '../../utilities/models/archive';
import { DocumentsService } from '../services/documents.service';
import { CreateDocumentsUrlParamsService } from '../services/create-documents-url-params.service';
import { Document } from '../document';

@Component({
  selector: 'app-documents-list',
  templateUrl: './documents-list.component.html',
  styleUrls: ['./documents-list.component.css','../../../shared css/list-item.css']
})
export class DocumentsListComponent implements OnInit {

  pagination: Pagination;
  items:Document[];
  paginationPanelStyles = new Map<string, string>();
  nameFilterInput:string=''
  nameFilter:string=''
  loaded:boolean=false;
  selectMode:boolean=false;
  selectedIds:number[]=[];

  constructor(private documentsService:DocumentsService
    ,private loadingScreenService:LoadingScreenService
    ,public resizeWindowWatcherService:ResizeWindowWatcherService
    ,private router: Router
    ,private route: ActivatedRoute
    ,private urlParamsService:UrlParamsService
    ,private createDocumentsUrlParamsService:CreateDocumentsUrlParamsService
    ,private confirmService:ConfirmService
    ) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params =>{
      this.pagination=new Pagination();

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
    var path = this.createDocumentsUrlParamsService.createDocumentsUrlParams(name,pagination)
    this.loaded=false;
    this.router.navigateByUrl('/contractors'+path)
  }

  onBlurNameFilerInput(){
    this.navigate(this.nameFilterInput,new Pagination());
  }

  loadContractors(){
    this.nameFilter=this.nameFilterInput;
      this.loadingScreenService.show();

      this.documentsService.getList(this.nameFilterInput,this.pagination).subscribe(x=>{
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

        this.documentsService.archive(productsToArchive).subscribe(x=>{
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
