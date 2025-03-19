import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CompanyComponent } from './company/company.component';
import { AuthenticationGuard } from 'src/app/_guards/authentication.guard';

export const routes: Routes = [
  {path:':id',component:CompanyComponent,canActivate:[AuthenticationGuard]},
  {path:'add',component:CompanyComponent,canActivate:[AuthenticationGuard]},
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CompaniesRoutingModule { }
