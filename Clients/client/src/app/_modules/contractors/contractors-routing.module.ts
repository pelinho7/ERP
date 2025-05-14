import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthenticationGuard } from 'src/app/_guards/authentication.guard';
import { ContractorsListComponent } from './contractors-list/contractors-list.component';
import { ContractorComponent } from './contractor/contractor.component';
import { SetCompanyGuard } from '../companies/guards/set-company.guard';

// const routes: Routes = [];
export const routes: Routes = [
  {path:'',component:ContractorsListComponent,canActivate:[AuthenticationGuard,SetCompanyGuard]},
  {path:':id',component:ContractorComponent,canActivate:[AuthenticationGuard,SetCompanyGuard]},
  {path:'add',component:ContractorComponent,canActivate:[AuthenticationGuard,SetCompanyGuard]},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ContractorsRoutingModule { }
