import { Component, OnDestroy, OnInit, Renderer2 } from '@angular/core';
import { CompanyUsersService } from '../services/company-users.service';
import { ConfirmService } from '../../message-dialogs/services/confirm.service';
import { LoadingScreenService } from '../../utilities/services/loading-screen.service';
import { ToastrService } from 'ngx-toastr';
import { CompaniesService } from '../services/companies.service';
import { finalize, map, take, takeUntil, tap } from 'rxjs/operators';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { CompanyUser } from '../models/companyUser';
import { Subject } from 'rxjs';
import { AccountService } from '../../account/services/account.service';
import { Archive } from '../../utilities/models/archive';

@Component({
  selector: 'app-company-users',
  templateUrl: './company-users.component.html',
  styleUrls: ['./company-users.component.css','../../../shared css/list-item.css']
})
export class CompanyUsersComponent implements OnInit,OnDestroy  {

  loaded:boolean=false;
  companyUsers: CompanyUser[];
  private destroy$ = new Subject<void>();

  constructor(private companyUsersService:CompanyUsersService
    ,private companiesService:CompaniesService
    ,public accountService: AccountService
  ,private confirmService:ConfirmService
,private loadingScreenService:LoadingScreenService
,private toastr: ToastrService
,private router:Router
,private location: Location
, private renderer: Renderer2) { }

    ngOnInit(): void {
      this.getCompanyUsers();
    }

  getCompanyUsers(){
    var companyId:number;
    this.companiesService.currentCompany$.pipe(
      take(1)
    ).subscribe(x=>{companyId=x.id})

    this.loadingScreenService.show('company-users-spinner');
    this.companyUsersService.getCompanyUsers(companyId).pipe(
      takeUntil(this.destroy$),
      //finalize fire only when request posted
      finalize(() => {
        this.loadingScreenService.hide('company-users-spinner');
      }),
      //tap for asObservable object (not request)
      tap(() => {
        this.loadingScreenService.hide('company-users-spinner');
      })
    )
    .subscribe(x=>{
      this.companyUsers=x;
      this.loaded=true;
    })
  }

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
}

  addCompanyUser(){
    this.router.navigateByUrl('/company/company-users/add')
  }

  close(){
    this.location.back();
  }

  toggleUser(event:any){
    const buttonElement = event.currentTarget as HTMLElement;
    const cardHeaderElement = buttonElement.parentElement.parentElement;

    if(cardHeaderElement.nextElementSibling.classList.contains('colapsed-user-modules')){
      this.renderer.removeClass(cardHeaderElement.nextSibling,'colapsed-user-modules')
      this.renderer.removeClass(cardHeaderElement,'colapsed-user-header')
      this.renderer.addClass(buttonElement.firstChild,'fa-arrow-up')
      this.renderer.removeClass(buttonElement.firstChild,'fa-arrow-down')
    }
    else{
      this.renderer.addClass(cardHeaderElement.nextSibling,'colapsed-user-modules')
      this.renderer.addClass(cardHeaderElement,'colapsed-user-header')
      this.renderer.removeClass(buttonElement.firstChild,'fa-arrow-up')
      this.renderer.addClass(buttonElement.firstChild,'fa-arrow-down')
    }
  }

  archive(id:number){
this.confirmService.confirm('Archiving company user', 'Are you sure you want to archive the selected company user?').subscribe(result=>{
        
        if(result){
          var companyUserToArchive: Archive=new Archive();
          companyUserToArchive.archive=true;
          companyUserToArchive.id=id;

          this.loadingScreenService.show('company-users-spinner');
  
          
          this.companyUsersService.archive(companyUserToArchive).pipe(
            takeUntil(this.destroy$),
            //finalize fire only when request posted
            finalize(() => {
              this.loadingScreenService.hide('company-users-spinner');
            }),
            //tap for asObservable object (not request)
            tap(() => {
              this.loadingScreenService.hide('company-users-spinner');
            })
          ).subscribe(x=>{
            this.companyUsers = this.companyUsers.filter(cu => cu.id !== id)
            this.companyUsersService.updateCachedCompanyUsers(this.companyUsers)
          })
        }
      })
  }

  edit(id:number){
    this.router.navigateByUrl('/company/company-users/'+id)
  }
}
