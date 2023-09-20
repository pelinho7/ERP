import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ContractorsListComponent } from './contractors-list/contractors-list.component';
import { ContractorsRoutingModule } from './contractors-routing.module';
import { NavbarsModule } from '../navbars/navbars.module';
import { ContractorComponent } from './contractor/contractor.component';
import { ContractorsListItemComponent } from './contractors-list-item/contractors-list-item.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormControlsModule } from '../form-controls/form-controls.module';
import { StaticDataModule } from '../static-data/static-data.module';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { NgxSpinnerModule } from 'ngx-spinner';



@NgModule({
  declarations: [
    ContractorsListComponent,
    ContractorComponent,
    ContractorsListItemComponent
  ],
  exports: [
    ContractorComponent,
    ContractorsListComponent,
],
imports: [
    CommonModule,
    ContractorsRoutingModule,
    ReactiveFormsModule,
    FormControlsModule,
    NavbarsModule,
    StaticDataModule,
    PaginationModule,
    FormsModule,
    NgxSpinnerModule,
  ]
})
export class ContractorsModule { }
