import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthenticationGuard } from 'src/app/_guards/authentication.guard';
import { ContractorsListComponent } from './contractors-list/contractors-list.component';
import { ContractorComponent } from './contractor/contractor.component';

// const routes: Routes = [];
export const routes: Routes = [
  {path:'',component:ContractorsListComponent,canActivate:[AuthenticationGuard]},
  {path:':id',component:ContractorComponent,canActivate:[AuthenticationGuard]},
  {path:'add',component:ContractorComponent,canActivate:[AuthenticationGuard]},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ContractorsRoutingModule { }
