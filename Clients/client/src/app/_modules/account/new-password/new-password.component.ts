import { Component, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../services/account.service';
import { Router } from '@angular/router';
import { FormHelpersService } from 'src/app/_services/form-helpers.service';

@Component({
  selector: 'app-new-password',
  templateUrl: './new-password.component.html',
  styleUrls: ['./new-password.component.css']
})
export class NewPasswordComponent implements OnInit {

  newPasswordForm:UntypedFormGroup;
  loadData:boolean=false;
  constructor(private accountService:AccountService,private router:Router
    , private toastr:ToastrService,private fb:UntypedFormBuilder
    , private formHelpersService:FormHelpersService) { }

  ngOnInit(): void {
    var idIndex=this.router.url.indexOf('?id=');
    if(idIndex<0){
      this.toastr.error('Incorrect url');
      return;
    }
    var tempUrl = this.router.url.substring(idIndex);
    var tokenIndex=this.router.url.indexOf('&token=');
    if(tokenIndex<0){
      this.toastr.error('Incorrect url');
      return;
    }
    var data = tempUrl.split("&token=");
    var token=data[1];
    var userId=data[0].replace('?id=','');

    this.newPasswordForm=this.fb.group({
      userId:[userId],
      resetPasswordToken:[token],
      newPassword:['',[Validators.required,Validators.minLength(4)]],
      newPasswordRepeated:['',[Validators.required,this.formHelpersService.matchValues('newPassword')]],
    });

    this.loadData=true;
  }

  setNewPassword(){
    this.accountService.newPassword(this.newPasswordForm.value).subscribe(_=>{
      this.toastr.info('Your password has been changed');
      this.router.navigateByUrl('/');
    })
  }

}
