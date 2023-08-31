import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthenticationGuard } from 'src/app/_guards/authentication.guard';
import { ContractorsListComponent } from './contractors-list/contractors-list.component';

// const routes: Routes = [];
export const routes: Routes = [
  // {
  //   path: '',
  //   component: ProductsListComponent,
  //   children: [
  //     { path: '', pathMatch: 'full', redirectTo: 'list' },
  //     { path: 'list', component: ProductsListComponent },
  //   ]
  // },

  // {path:'',component:ProductsListComponent,canActivate:[AuthenticationGuard]},
  {path:'',component:ContractorsListComponent},
];
// const routes: Routes = [
//   {path:'',component:ProductsListComponent},
//   {path:'list',component:ProductsListComponent},
// ];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ContractorsRoutingModule { }
