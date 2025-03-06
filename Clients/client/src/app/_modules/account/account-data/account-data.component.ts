import { Component, OnInit } from '@angular/core';
import { ValidateEmailNotTaken } from '../validators/email-not-taken.validator';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { FormHelpersService } from 'src/app/_services/form-helpers.service';
import { AccountService } from '../services/account.service';
import { ToastrService } from 'ngx-toastr';
import { MobileNavbarHelpersService } from 'src/app/_services/mobile-navbar-helpers.service';
import { AccountData } from '../models/accountData';

@Component({
  selector: 'app-account-data',
  templateUrl: './account-data.component.html',
  styleUrls: ['./account-data.component.css']
})
export class AccountDataComponent implements OnInit {

  accountDataForm:UntypedFormGroup;
  public loadData:boolean=false;

  constructor(private fb:UntypedFormBuilder,public formHelpersService:FormHelpersService
    ,private accountService:AccountService
    ,private toastr:ToastrService,public mobileNavbarHelpersService:MobileNavbarHelpersService) { }

  ngOnInit(): void {
    // this.accountDataForm=this.fb.group({
    //   email:['data.email',Validators.email],
    //   firstName:['data.firstName',Validators.required],
    //   lastName:['data.lastName',Validators.required]
    // })

    // this.accountDataForm.controls['email'].setAsyncValidators(ValidateEmailNotTaken.createValidator(this.accountService));
    // this.loadData=true;

    this.accountService.getAccountData().subscribe((data:AccountData)=>{
      this.accountDataForm=this.fb.group({
        email:[data.email,Validators.email],
        firstName:[data.firstName,Validators.required],
        lastName:[data.lastName,Validators.required]
      })

      this.accountDataForm.controls['email'].setAsyncValidators(ValidateEmailNotTaken.createValidator(this.accountService,data.id));
      this.loadData=true;
    })
  }

  save(){
    this.accountService.updateAccountData(this.accountDataForm.value).subscribe((user:any)=>{
      this.toastr.info('Account data updated');
    })
  }

}
