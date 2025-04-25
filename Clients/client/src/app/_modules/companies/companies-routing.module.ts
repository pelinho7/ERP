import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CompanyComponent } from './company/company.component';
import { AuthenticationGuard } from 'src/app/_guards/authentication.guard';
import { CompanyAdminGuardGuard } from './guards/company-admin-guard.guard';
import { CompanyUsersComponent } from './company-users/company-users.component';
import { CompanyUserComponent } from './company-user/company-user.component';

export const routes: Routes = [
  {path:'company-users/add',component:CompanyUserComponent,canActivate:[AuthenticationGuard,CompanyAdminGuardGuard]},
  {path:'company-users/:id',component:CompanyUserComponent,canActivate:[AuthenticationGuard,CompanyAdminGuardGuard]},
  {path:'company-users',component:CompanyUsersComponent,canActivate:[AuthenticationGuard,CompanyAdminGuardGuard]},
  {path:':id',component:CompanyComponent,canActivate:[AuthenticationGuard,CompanyAdminGuardGuard]},
  {path:'add',component:CompanyComponent,canActivate:[AuthenticationGuard,CompanyAdminGuardGuard]},
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CompaniesRoutingModule { }
