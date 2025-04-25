import { Component, ElementRef, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ResizeWindowWatcherService } from 'src/app/_services/resize-window-watcher.service';
import { CompanyUser } from '../models/companyUser';
import { ActivatedRoute, Router } from '@angular/router';
import { UsersService } from '../../account/services/users.service';
import { Subject } from 'rxjs';
import { concatMap, debounceTime, distinctUntilChanged, finalize, map, switchMap, takeUntil } from 'rxjs/operators';
import { LoadingScreenService } from '../../utilities/services/loading-screen.service';
import { CompanyUsersService } from '../services/company-users.service';
import { CompaniesService } from '../services/companies.service';
import { Company } from '../models/company';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-company-user',
  templateUrl: './company-user.component.html',
  styleUrls: ['./company-user.component.css','../../../shared css/list-item.css']
})
export class CompanyUserComponent implements OnInit,OnDestroy {

  loaded:boolean=false;
  companyUserForm:FormGroup;
  title:string='New item';
  companyUser:CompanyUser;
  usersMap: Map<number, string> = new Map();
  userSearchTextChanged: Subject<string> = new Subject<string>();
  reloadSelectDataSubject: Subject<void> = new Subject<void>();
  currentComapnyUsers:CompanyUser[]
  editMode:boolean=false;
  saved:boolean=false;
  private destroy$ = new Subject<void>();

  constructor(private fb:FormBuilder,private elem: ElementRef
    ,public resizeWindowWatcherService:ResizeWindowWatcherService
    ,private route: ActivatedRoute
    ,private usersService:UsersService
    ,private loadingScreenService:LoadingScreenService
    ,private companyUsersService:CompanyUsersService
    ,private companiesService:CompaniesService
    ,private toastr: ToastrService
    ,private router:Router
  ) { }

  ngOnInit(): void {
    
//set empty row to enable filter
this.usersMap.set(0,'')

//get users if the user does not enter text in the filter for 1 second 
this.userSearchTextChanged.pipe(
  debounceTime(1000),
  distinctUntilChanged()
).subscribe(value => {
  this.loadingScreenService.show('users-spinner');
  this.usersService.getUsers(value).pipe(
    takeUntil(this.destroy$),
    finalize(() => {
      this.loadingScreenService.hide('users-spinner');
    })
  ).subscribe(users=>{
    //remove users currently associated with the company
    var currentUserIds=this.currentComapnyUsers.map((user) => user.userId);
    users=users.filter(x=>!currentUserIds.includes(x.userId));
    this.usersMap.clear();
    if(users!=null && users.length>0){
      users.forEach(u=>{
        this.usersMap.set(u.userId,u.email);
      })
      this.reloadSelectDataSubject.next();
    }
    else{
      this.usersMap.set(0,'')
    }
})
});

    this.route.params.subscribe(x=>{
      var id=x['id'];
      // if(id!='add'){
      //   // this.getCompany(id);
        
      // }
      // else{
      //   this.companyUser=new CompanyUser();
      // }

      this.companyUser=new CompanyUser();
        this.loadingScreenService.show('company-user-spinner');
        this.companiesService.currentCompany$
          .pipe(concatMap((c) => {
            takeUntil(this.destroy$),
            this.companyUser.companyId=c.id;
            return this.companyUsersService.getCompanyUsers(c.id)
          })).pipe(
            takeUntil(this.destroy$),
            finalize(() => {
              this.loadingScreenService.hide('company-user-spinner');
            })
          )
          .subscribe((companyU:CompanyUser[]) => {
            this.currentComapnyUsers=companyU;
            if(id!='add'){
              this.editMode=true;
              this.title='Edit item'
              this.companyUser = this.currentComapnyUsers.find(x=>x.id == id);
              this.usersMap.set(this.companyUser.userId,this.companyUser.userEmail);
            }
            this.loadingScreenService.hide('company-user-spinner');

            this.createCompanyUserForm();
          })


    })

  }

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
}

  createCompanyUserForm(){ 
    console.log(this.companyUser.companyId)
        this.companyUserForm=this.fb.group({
          id:[this.companyUser.id,Validators.required],
          companyId:[this.companyUser.companyId,Validators.required],
          // userId:[this.companyUser.userId,Validators.required],
          userId:[{value: this.companyUser.userId, disabled: this.editMode},[Validators.required]],
          administrator:[this.companyUser.administrator],
          productsModuleRead:[this.companyUser.productsModuleRead],
          productsModuleWrite:[this.companyUser.productsModuleWrite],
          contractorsModuleRead:[this.companyUser.contractorsModuleRead],
          contractorsModuleWrite:[this.companyUser.contractorsModuleWrite],
        })
        
        this.loaded=true;
      }

      administratorChanged(event:any){

      }

      searchTextChanged(text:any){
        if(text.length>=3){
          this.userSearchTextChanged.next(text)
        }
      }

      save(){
        var companyUser=(this.companyUserForm.value as CompanyUser);

        this.companyUserForm.markAllAsTouched();
        
        if(this.companyUserForm.valid){
          var companyUser=(this.companyUserForm.value as CompanyUser);
          
          this.loadingScreenService.show('company-user-spinner');

          if(this.editMode){
            companyUser.userId=this.companyUser.userId;
            this.update(companyUser);
          }
          else{
            this.add(companyUser);
          }
        }
      }

      add(companyUser:CompanyUser){
        this.companyUsersService.add(companyUser).pipe(
          takeUntil(this.destroy$),
          finalize(() => {
            this.loadingScreenService.hide('company-user-spinner');
          })
        )
        .subscribe(x=>{
          this.saved=true;
          this.toastr.info('Created');
          this.currentComapnyUsers.push(x);
          this.companyUsersService.updateCachedCompanyUsers(this.currentComapnyUsers);
          this.router.navigateByUrl('/company/company-users')
        })
      }
    
      update(companyUser:CompanyUser){
        this.companyUsersService.update(companyUser).pipe(
          finalize(() => {
            takeUntil(this.destroy$),
            this.loadingScreenService.hide('company-user-spinner');
          })
        ).subscribe(x=>{
          this.saved=true;
          this.toastr.info('Updated');
          //find updated element
          const index = this.currentComapnyUsers.findIndex(cu => cu.id == x.id);
          if (index !== -1) {
            x.userEmail =this.currentComapnyUsers[index].userEmail
            this.currentComapnyUsers[index] = { ...x };
          }
          //update cached company users
          this.companyUsersService.updateCachedCompanyUsers(this.currentComapnyUsers);
          this.router.navigateByUrl('/company/company-users')
        })
      }
}
