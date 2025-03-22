import { Component, OnInit } from '@angular/core';
import { ResizeWindowWatcherService } from '../services/resize-window-watcher.service';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { AccountService } from '../../account/services/account.service';
import { HttpClient } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { CompaniesService } from '../../companies/services/companies.service';
import { ChooseCompanyComponent } from '../../companies/choose-company/choose-company.component';
import { ResetPasswordComponent } from '../../account/reset-password/reset-password.component';

@Component({
  selector: 'app-top-navbar',
  templateUrl: './top-navbar.component.html',
  styleUrls: ['./top-navbar.component.css'],
})
export class TopNavbarComponent implements OnInit {

  bsModalRef?: BsModalRef;
  
  constructor(public resizeWindowWatcherService:ResizeWindowWatcherService
    ,private router:Router,private http:HttpClient
    ,public accountService:AccountService
    ,public companiesService:CompaniesService
    ,private toastr:ToastrService
    ,private modalService: BsModalService) { }

  ngOnInit(): void {
  }

  openSideNavbar(){
    var nav=(document.querySelector('#side-navbar') as HTMLElement);
    nav.style.width = '100%';
    nav.style.transition= '0.5s'

    var topNav=(document.querySelector('#top-navbar') as HTMLElement);
    topNav.style.zIndex='-1';
  }

  logout(){
    this.accountService.logout();
  }

  login(){
    this.router.navigateByUrl('/account/login')
  }

  register(){
    this.router.navigateByUrl('/account/register')
  }

  showAccountData(){
    this.router.navigateByUrl('/account/data')
  }

  changePassword(){
    this.router.navigateByUrl('/account/change-password')
  }

  newCompany(){
    this.router.navigateByUrl('/company/add')
  }

  changeCompany() {
      const initialState: ModalOptions = {
        initialState: {
          list: [],
          title: ''
        }
      };
      this.bsModalRef = this.modalService.show(ChooseCompanyComponent, initialState);
      this.bsModalRef.content.closeBtnName = 'Close';
    }
}
