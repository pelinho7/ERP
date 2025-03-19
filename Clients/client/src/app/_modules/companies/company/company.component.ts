import { Component, ElementRef, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { StaticDataService } from '../../static-data/static-data.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmService } from '../../message-dialogs/services/confirm.service';
import { ToastrService } from 'ngx-toastr';
import { MaskType } from '../../form-controls/text-input/mask-factory/mask-type';
import { ResizeWindowWatcherService } from 'src/app/_services/resize-window-watcher.service';
import { Company } from '../models/company';
import { CompaniesService } from '../services/companies.service';
import { Location } from '@angular/common';
import { LoadingScreenService } from '../../utilities/services/loading-screen.service';
import { PolishVatId } from '../validators/polish-vatId.validator';

@Component({
  selector: 'app-company',
  templateUrl: './company.component.html',
  styleUrls: ['./company.component.css','../../../shared css/list-item.css']
})
export class CompanyComponent implements OnInit {

  companyForm:FormGroup;
  editMode:boolean=false;
  itemcompany:boolean=false;
  company:Company
  loaded:boolean=false;
  saved:boolean=false;
  title:string='New item';
  navItems:string[]=["General","Address","Contact","Trade"]
  public maskType: typeof MaskType = MaskType;

  constructor(private fb:FormBuilder,private elem: ElementRef
    ,public resizeWindowWatcherService:ResizeWindowWatcherService
    ,public staticDataService:StaticDataService
  ,private companiesService:CompaniesService
  ,private confirmService:ConfirmService
  ,private location: Location
,private loadingScreenService:LoadingScreenService
,private route: ActivatedRoute
,private toastr: ToastrService) { }

  ngOnInit(): void {
    this.company=new Company();
    this.createCompanyForm();
    this.route.params.subscribe(x=>{
          var id=x['id'];
          if(id!='add'){
            this.getCompany(id);
          }
          else{
            this.company=new Company();
            this.createCompanyForm();
          }
        })
  }

  createCompanyForm(){ 
      this.companyForm=this.fb.group({
        id:[this.company.id,Validators.required],
        name:[this.company.name,[Validators.required,Validators.maxLength(300)]],
        countryCode:[this.company.countryCode],
        vatId:[this.company.vatId,[Validators.required,Validators.maxLength(13)]],
        street:[this.company.street,[Validators.required,Validators.maxLength(150)]],
        streetNo:[this.company.streetNo,[Validators.maxLength(10)]],
        apartmentNo:[this.company.apartmentNo,[Validators.maxLength(10)]],
        zipCode:[this.company.zipCode,[Validators.required,Validators.maxLength(6)]],
        postalOffice:[this.company.postalOffice,[Validators.required,Validators.maxLength(120)]],
        city:[this.company.city,[Validators.required,Validators.maxLength(120)]],
        country:[this.company.country,[Validators.required,Validators.maxLength(80)]],
  
        phone:[this.company.phone,[Validators.required,Validators.maxLength(30)]],
        email:[this.company.email,[Validators.email,Validators.maxLength(50)]],
        mobile:[this.company.mobile,[Validators.maxLength(30)]],
        fax:[this.company.fax,[Validators.maxLength(30)]],
        wwwAddress:[this.company.wwwAddress],
  
        bankAccountNumber:[this.company.bankAccountNumber,[Validators.maxLength(34)]],
        swiftCode:[this.company.swiftCode,[Validators.minLength(8),Validators.maxLength(11)]],
      })
  
      this.companyForm.controls['vatId'].setAsyncValidators(PolishVatId.createValidator(this.companyForm.controls['countryCode']));
  
      this.loaded=true;
    }
  
    countryCodeChanged(event:any){
      this.companyForm.controls['vatId'].updateValueAndValidity();
    }

    scrollToSection(sectionId:number){
        let sections = this.elem.nativeElement.querySelectorAll('.form-section');
        sections[sectionId].scrollIntoView();
      }
    
      close(){
        if(this.companyForm.dirty){
          this.confirmService.confirm('Changes have not been saved', 'If you close the form you will loose the unsaved data. Are you sure you want to close the form without saving?').subscribe(result=>{
    
            if(result){
              this.location.back();
            }
          })
        }
        else{
          this.location.back();
        }
      }
    
      save(){
        var company=(this.companyForm.value as Company);

        this.companyForm.markAllAsTouched();
        
        if(this.companyForm.valid){
          var company=(this.companyForm.value as Company);
          
          this.loadingScreenService.show();

          if(this.editMode){
            this.update(company);
          }
          else{
            this.add(company);
          }
        }
      }
    
      add(company:Company){
        this.companiesService.add(company).subscribe(x=>{
          this.saved=true;
          this.toastr.info('Created');
          this.companyForm.markAsPristine();
        })
        .add(()=>{
          this.loadingScreenService.hide();
        })
      }
    
      update(company:Company){
        this.companiesService.update(company).subscribe(x=>{
          this.saved=true;
          this.toastr.info('Updated');
          this.companyForm.markAsPristine();//set dirty false
        })
        .add(()=>{
          this.loadingScreenService.hide();
        })
      }
    
      getCompany(id:number){
        this.loadingScreenService.show('company-spinner');
        this.companiesService.getById(id).subscribe(x=>{
          this.company=x;
          this.editMode=true;
          this.createCompanyForm();
          //console.log(x)
        })
        .add(()=>{
          this.loadingScreenService.hide('company-spinner');
        })
      }
}
