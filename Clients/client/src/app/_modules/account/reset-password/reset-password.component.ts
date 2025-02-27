import { Component, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AccountService } from '../services/account.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent implements OnInit {

  resetPasswordForm:UntypedFormGroup;

  constructor(
    private fb:UntypedFormBuilder,public bsModalRef: BsModalRef
    ,private accountService:AccountService
    ,private router:Router,private toastr:ToastrService) { }

  ngOnInit(): void {
    this.resetPasswordForm=this.fb.group({
      email:['',Validators.required],
    })
  }

  resetPassword(){
    var login:string = this.resetPasswordForm.controls['email'].value;
    this.accountService.resetPassword(login).subscribe((result:boolean)=>{
      if(result){
        this.toastr.info('On your email was sent mail');
        this.bsModalRef.hide();
        this.bsModalRef=null;
        this.router.navigateByUrl('/');
      }
    })
  }

}
