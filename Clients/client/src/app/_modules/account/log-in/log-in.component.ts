import { Component, ElementRef, OnInit } from '@angular/core';


import { FormControl, Validators, FormGroup, FormBuilder } from '@angular/forms';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { AccountService } from '../services/account.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UrlService } from 'src/app/_services/url.service';
import { ResetPasswordComponent } from '../reset-password/reset-password.component';
import { take } from 'rxjs/operators';


@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.css']
})

export class LogInComponent implements OnInit {
  bsModalRef?: BsModalRef;
  logInForm:FormGroup;
  loaded:boolean=false;

  constructor(
    private fb:FormBuilder,private modalService: BsModalService
    ,private accountService:AccountService
    ,private router:Router
    ,private toastr:ToastrService
    ,private urlService:UrlService) { }

  ngOnInit(): void {
    this.logInForm=this.fb.group({
      login:['',Validators.required],
      password:['',[Validators.required,Validators.minLength(4)]],
    })
    this.loaded=true;
  }

  logIn(){
    this.accountService.logIn(this.logInForm.value).subscribe(user=>{
      var prevUrl = this.urlService.getPreviousUrl();
      if(prevUrl.length>0){
        this.router.navigateByUrl(prevUrl);
      }
      else{
        this.router.navigateByUrl('/');
      }
    })
  }

  resendVerificationEmail(){
    this.accountService.resendVerificationEmail(this.logInForm.value).subscribe(user=>{
      this.toastr.info('On your email sent verification mail')
      this.router.navigateByUrl('/');
    })
  }

  openResetPasswordModal() {
    const initialState: ModalOptions = {
      initialState: {
        list: [],
        title: ''
      }
    };
    this.bsModalRef = this.modalService.show(ResetPasswordComponent, initialState);
    this.bsModalRef.content.closeBtnName = 'Close';
  }
}



