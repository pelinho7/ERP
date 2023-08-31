import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-email-verification',
  templateUrl: './email-verification.component.html',
  styleUrls: ['./email-verification.component.css']
})
export class EmailVerificationComponent implements OnInit {

  constructor(private router:Router
    , private toastr:ToastrService) { }

  ngOnInit(): void {
    // this.accountService.verifyEmail(this.router.url.substring(1)).subscribe((result:User)=>{
    //   this.toastr.info('Your email validated correctly');
    //   this.router.navigateByUrl('/');
    // });
  }

}
