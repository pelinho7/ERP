import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EmailVerificationComponent } from './email-verification/email-verification.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { HomeComponent } from './home/home.component';
import { AdminGuard } from './_guards/admin.guard';
import { AuthenticationGuard } from './_guards/authentication.guard';


const routes: Routes = [
  {path:'',component:HomeComponent},
  {path:'home',component:HomeComponent},
  {
    path: 'admin-panel',
    loadChildren: () => import('./_modules/admin-panel/admin-panel.module').then(m => m.AdminPanelModule)
  },
  {
    path: 'products',
    loadChildren: () => import('./_modules/products/products.module').then(m => m.ProductsModule)
  },
  {
    path: 'contractors',
    loadChildren: () => import('./_modules/contractors/contractors.module').then(m => m.ContractorsModule)
  },
  // {path:'account',component:AccountDataComponent,canActivate:[AuthenticationGuard]},
  // {path:'products/:category',component:ProductsListComponent},
  // {path:'product/:code',component:ProductComponent},
  // {path:'account/login',component:LogInComponent},
  // {path:'account/register',component:RegisterComponent},
  // {path:'account/email-confirmation',component:EmailVerificationComponent},
  // {path:'account/new-password',component:NewPasswordComponent},
  // {path:'cart/list',component:CartComponent},
  // {
  //   path:'',
  //   runGuardsAndResolvers:'always',
  //   canActivate:[AuthenticationGuard],
  //   children:[
  //     {
  //       path:'',
  //       runGuardsAndResolvers:'always',
  //       canActivate:[AdminGuard],
  //       children:[
  //         {path:'attributes',component:AttributesListComponent},
  //         {path:'categories',component:CategoriesListComponent},
  //         {path:'products-managment',component:ProductsManagmentListComponent},
  //         {path:'products-managment/create',component:ProductManagmentComponent},
  //         {path:'products-managment/edit/:id',component:ProductManagmentComponent},
  //         {path:'warehouses',component:WarehousesListComponent},
  //       ]
  //     },

  //     {path:'account/data',component:AccountDataComponent},
  //     {path:'account/change-password',component:ChangePasswordComponent},
  //     {path:'account/user-agreements',component:UserAgreementsComponent},
  //     {path:'account/shipping-addresses',component:ShippingAddressesComponent},
  //   ]
  // },
  // {path:'cart',component:CartComponent},
  {path:'not-found',component:NotFoundComponent},
  {path:'server-error',component:ServerErrorComponent},
  {path:'**',component:HomeComponent, pathMatch:'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }