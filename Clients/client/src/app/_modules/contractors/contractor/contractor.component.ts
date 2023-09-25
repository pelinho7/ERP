import { Component, ElementRef, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Contractor } from '../contractor';
import { StaticDataService } from '../../static-data/static-data.service';
import { ResizeWindowWatcherService } from '../../navbars/services/resize-window-watcher.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { LoadingScreenService } from '../../utilities/services/loading-screen.service';
import { ConfirmService } from '../../message-dialogs/services/confirm.service';
import { ContractorsService } from '../services/contractors.service';
import { Location } from '@angular/common';
import { ContractorCodeNotTaken } from '../validators/contractor-code-not-taken.validator';
import { ContractorPolishVatId } from '../validators/contractor-polish-vatId.validator';
import { MaskType } from '../../form-controls/text-input/mask-factory/mask-type';
//import { ContractorPolishVatId } from '../validators/contractor-polish-vatId.validator';

@Component({
  selector: 'app-contractor',
  templateUrl: './contractor.component.html',
  styleUrls: ['./contractor.component.css','../../../shared css/list-item.css']
})
export class ContractorComponent implements OnInit {

  contractorForm:FormGroup;
  editMode:boolean=false;
  itemContractor:boolean=false;
  contractor:Contractor
  loaded:boolean=false;
  saved:boolean=false;
  title:string='New item';
  navItems:string[]=["General","Address","Contact","Trade","Additional"]
  public maskType: typeof MaskType = MaskType;

  constructor(private fb:FormBuilder,private elem: ElementRef
    ,public resizeWindowWatcherService:ResizeWindowWatcherService
    ,public staticDataService:StaticDataService
    ,private router: Router
    ,private location: Location
    ,private confirmService:ConfirmService
    ,private contractorsService:ContractorsService
    ,private toastr: ToastrService
    ,private loadingScreenService:LoadingScreenService
    ,private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.params.subscribe(x=>{
      var id=x['id'];
      if(id!='add'){
        this.getContractor(id);
      }
      else{
        this.contractor=new Contractor();
        this.createContractorForm();
      }
    })
  }

  createContractorForm(){ 
    this.contractorForm=this.fb.group({
      id:[this.contractor.id,Validators.required],
      code:[this.contractor.code,[Validators.required,Validators.maxLength(30)]],
      name:[this.contractor.name,[Validators.required,Validators.maxLength(300)]],
      repFirstName:[this.contractor.repFirstName,[Validators.maxLength(100)]],
      repLastName:[this.contractor.repLastName,[Validators.maxLength(150)]],
      countryCode:[this.contractor.countryCode],
      vatId:[this.contractor.vatId,[Validators.maxLength(13)]],
      street:[this.contractor.street,[Validators.maxLength(150)]],
      streetNo:[this.contractor.streetNo,[Validators.maxLength(10)]],
      apartmentNo:[this.contractor.apartmentNo,[Validators.maxLength(10)]],
      zipCode:[this.contractor.zipCode,[Validators.maxLength(6)]],
      postalOffice:[this.contractor.postalOffice,[Validators.maxLength(120)]],
      city:[this.contractor.city,[Validators.maxLength(120)]],
      country:[this.contractor.country,[Validators.maxLength(80)]],

      phone:[this.contractor.phone,[Validators.maxLength(30)]],
      email:[this.contractor.email,[Validators.email,Validators.maxLength(50)]],
      mobile:[this.contractor.mobile,[Validators.maxLength(30)]],
      fax:[this.contractor.fax,[Validators.maxLength(30)]],
      wwwAddress:[this.contractor.wwwAddress],

      discount:[this.contractor.discount,[Validators.min(0),Validators.max(100)]],
      paymentForm:[this.contractor.paymentForm,[Validators.required]],
      bankAccountNumber:[this.contractor.bankAccountNumber,[Validators.maxLength(34)]],
      splitPayment:[this.contractor.splitPayment],
      swiftCode:[this.contractor.swiftCode,[Validators.minLength(8),Validators.maxLength(11)]],

      note:[this.contractor.note],
      additionalInformation:[this.contractor.additionalInformation],
      type:[this.contractor.type],
      status:[this.contractor.status],
      activeTaxpayer:[this.contractor.activeTaxpayer],
    })

    this.contractorForm.controls['code'].setAsyncValidators(ContractorCodeNotTaken.createValidator(this.contractorsService,this.contractor.id));
    this.contractorForm.controls['vatId'].setAsyncValidators(ContractorPolishVatId.createValidator(this.contractorForm.controls['countryCode']));

    this.loaded=true;
  }

  countryCodeChanged(event:any){
    this.contractorForm.controls['vatId'].updateValueAndValidity();
  }

  scrollToSection(sectionId:number){
    let sections = this.elem.nativeElement.querySelectorAll('.form-section');
    sections[sectionId].scrollIntoView();
  }

  purchaseNetPriceChanged(value:number){
    var vat =this.staticDataService.getVatObjectFromJsonObject(this.contractorForm.get('vatObject').value)
    this.contractorForm.patchValue({
      purchaseGrossPrice: value*(vat.vatValue+100)/100, 
    });
  }

  saleNetPriceChanged(value:number){
    var vat =this.staticDataService.getVatObjectFromJsonObject(this.contractorForm.get('vatObject').value)
    this.contractorForm.patchValue({
      saleGrossPrice: value*(vat.vatValue+100)/100, 
    });
  }

  close(){
    if(this.contractorForm.dirty){
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
    var contractor=(this.contractorForm.value as Contractor);
    console.log(contractor)
    //return;
    this.contractorForm.markAllAsTouched();
    
    if(this.contractorForm.valid){
      var contractor=(this.contractorForm.value as Contractor);
      
      this.loadingScreenService.show();

      if(this.editMode){
        this.update(contractor);
      }
      else{
        this.add(contractor);
      }
    }
  }

  add(contractor:Contractor){
    this.contractorsService.add(contractor).subscribe(x=>{
      this.saved=true;
    })
    .add(()=>{
      this.loadingScreenService.hide();
    })
  }

  update(contractor:Contractor){
    this.contractorsService.update(contractor).subscribe(x=>{
      this.saved=true;
    })
    .add(()=>{
      this.loadingScreenService.hide();
    })
  }

  getContractor(id:number){
    this.loadingScreenService.show('contractor-spinner');
    this.contractorsService.getById(id).subscribe(x=>{
      this.contractor=x;
      this.editMode=true;
      this.title=this.contractor.code;
      this.createContractorForm();
      //console.log(x)
    })
    .add(()=>{
      this.loadingScreenService.hide('contractor-spinner');
    })
  }

}
