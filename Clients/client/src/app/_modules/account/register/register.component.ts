import { Component, OnInit } from '@angular/core';
import { UntypedFormArray, UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../services/account.service';
import { ValidateEmailNotTaken } from 'src/app/validators/email-not-taken.validator';
import { Registration } from '../models/registration';
import { FormHelpersService } from 'src/app/_services/form-helpers.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  public registration:Registration;
  registerForm:UntypedFormGroup;
  public loadData:boolean=false;
  constructor(private fb:UntypedFormBuilder,public formHelpersService:FormHelpersService
    ,private accountService:AccountService
    ,private router:Router,private toastr:ToastrService) { }

  ngOnInit(): void {
    this.registerForm=this.fb.group({
      email:['',Validators.email],
      firstName:['',Validators.required],
      lastName:['',Validators.required],
      password:['',[Validators.required,Validators.minLength(4),Validators.maxLength(8)]],
      passwordRepeated:['',[Validators.required,this.formHelpersService.matchValues('password')]],
      agreements: this.fb.array([])
    })

    this.registerForm.controls['email'].setAsyncValidators(ValidateEmailNotTaken.createValidator(this.accountService));
  this.loadData=true;
  }

  register(){
    this.accountService.register(this.registerForm.value).subscribe(user=>{
      this.toastr.info('On your email sent verification mail')
      this.router.navigateByUrl('/');
    })
  }
}
