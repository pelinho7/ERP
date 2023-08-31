import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductsListComponent } from './products-list/products-list.component';
import { ProductComponent } from './product/product.component';
import { AuthenticationGuard } from 'src/app/_guards/authentication.guard';

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
  {path:'',component:ProductsListComponent,canActivate:[AuthenticationGuard]},
  {path:':id',component:ProductComponent,canActivate:[AuthenticationGuard]},
  {path:'add',component:ProductComponent,canActivate:[AuthenticationGuard]},
];
// const routes: Routes = [
//   {path:'',component:ProductsListComponent},
//   {path:'list',component:ProductsListComponent},
// ];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProductsRoutingModule { }
