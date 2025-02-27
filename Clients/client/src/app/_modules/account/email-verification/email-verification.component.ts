import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../services/account.service';
import { Router } from '@angular/router';
import { User } from '../models/user';

@Component({
  selector: 'app-email-verification',
  templateUrl: './email-verification.component.html',
  styleUrls: ['./email-verification.component.css']
})
export class EmailVerificationComponent implements OnInit {

  constructor(private accountService:AccountService,private router:Router
    , private toastr:ToastrService) { }

  ngOnInit(): void {
    console.log('aaaaaaaaaaa')
    this.accountService.verifyEmail(this.router.url.substring(1)).subscribe((result:User)=>{
      this.toastr.info('Your email validated correctly');
      this.router.navigateByUrl('/');
    });
  }
}
