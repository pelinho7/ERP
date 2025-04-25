import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CompaniesRoutingModule } from './companies-routing.module';
import { CompanyComponent } from './company/company.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormControlsModule } from '../form-controls/form-controls.module';
import { NavbarsModule } from '../navbars/navbars.module';
import { StaticDataModule } from '../static-data/static-data.module';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { NgxSpinnerModule } from 'ngx-spinner';
import { ChooseCompanyComponent } from './choose-company/choose-company.component';
import { CompanyUsersComponent } from './company-users/company-users.component';
import { CompanyUserComponent } from './company-user/company-user.component';
import { RouterModule } from '@angular/router';


@NgModule({
  declarations: [
    CompanyComponent,
    ChooseCompanyComponent,
    CompanyUsersComponent,
    CompanyUserComponent,
  ],
  exports: [
    CompanyComponent,
],
imports: [
  CommonModule,
  CompaniesRoutingModule,
      ReactiveFormsModule,
      FormControlsModule,
      NavbarsModule,
      StaticDataModule,
      PaginationModule,
      FormsModule,
      NgxSpinnerModule,
  ],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ],
  providers: []
})
export class CompaniesModule { }