import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { CompaniesService } from '../services/companies.service';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { LoadingScreenService } from '../../utilities/services/loading-screen.service';
import { delay } from 'rxjs/operators';
import { Router } from '@angular/router';

@Component({
  selector: 'app-choose-company',
  templateUrl: './choose-company.component.html',
  styleUrls: ['./choose-company.component.css']
})
export class ChooseCompanyComponent implements OnInit {

  chooseCompanyForm:UntypedFormGroup;
  companiesMap: Map<number, string> = new Map();
  
  constructor(private fb:UntypedFormBuilder, public bsModalRef: BsModalRef
    ,public companiesService:CompaniesService
    ,private loadingScreenService:LoadingScreenService
    ,private router:Router) { }

  ngOnInit(): void {
    this.chooseCompanyForm=this.fb.group({
      company:['',Validators.required],
    })

    this.loadingScreenService.show('choose-company-spinner');
    this.companiesService.getUserCompanies().subscribe(companies=>{
      companies.forEach(c=>{
        this.companiesMap.set(c.id,`VATID: ${c.countryCode}${c.vatId} Name: ${c.name}`)
      })
    })
    .add(()=>{
      this.loadingScreenService.hide('choose-company-spinner');
    })
  }

  chooseCompany(){
    var companyId=(this.chooseCompanyForm.value as any).company;

    this.loadingScreenService.show('choose-company-spinner');
    this.companiesService.changeCompany(companyId).subscribe(_=>{

    })

    .add(()=>{
      this.loadingScreenService.hide('choose-company-spinner');
      this.bsModalRef.hide();
      this.bsModalRef=null;
      this.router.navigateByUrl('/');
    })
  }

}
