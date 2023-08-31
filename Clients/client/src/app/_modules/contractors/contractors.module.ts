import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ContractorsListComponent } from './contractors-list/contractors-list.component';
import { ContractorsRoutingModule } from './contractors-routing.module';
import { NavbarsModule } from '../navbars/navbars.module';



@NgModule({
  declarations: [
    ContractorsListComponent
  ],
  exports: [
    ContractorsListComponent,
],
imports: [
    CommonModule,
    ContractorsRoutingModule,
  ]
})
export class ContractorsModule { }
