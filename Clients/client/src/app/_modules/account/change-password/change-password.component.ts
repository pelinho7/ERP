import { Component, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../services/account.service';
import { Router } from '@angular/router';
import { FormHelpersService } from 'src/app/_services/form-helpers.service';
import { MobileNavbarHelpersService } from 'src/app/_services/mobile-navbar-helpers.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {
  changePasswordForm:UntypedFormGroup;
  public loadData:boolean=false;
  
  constructor(private fb:UntypedFormBuilder,public formHelpersService:FormHelpersService
    ,private accountService:AccountService,private router:Router
    ,private toastr:ToastrService) { }

  ngOnInit(): void {
    this.changePasswordForm=this.fb.group({
      actualPassword:['',[Validators.required,Validators.minLength(4),Validators.maxLength(8)]],
      newPassword:['',[Validators.required,Validators.minLength(4),Validators.maxLength(8)]],
      newPasswordRepeated:['',[Validators.required,this.formHelpersService.matchValues('newPassword')]],
    });

    this.loadData=true;
  }

  changePassword(){
    this.accountService.changePassword(this.changePasswordForm.value).subscribe(_=>{
      this.toastr.info('Password changed');
      this.accountService.logout();
      this.router.navigateByUrl('/');
    })
  }
}
