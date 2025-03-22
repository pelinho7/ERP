import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SharedModule } from './_modules/shared.module';
import { HomeComponent } from './home/home.component';
import { FormsModule } from '@angular/forms';
import { CrossNumericValidatorDirective } from './_validators/cross-numeric.validator';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
//import { LoadingInterceptor } from './_interceptors/loading.interceptor';
import { EmailVerificationComponent } from './email-verification/email-verification.component';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import { MAT_SELECTSEARCH_DEFAULT_OPTIONS, MatSelectSearchOptions } from 'ngx-mat-select-search';
import { AccountModule } from './_modules/account/account.module';
import { AdminPanelRoutingModule } from './_modules/admin-panel/admin-panel-routing.module';
import { AdminPanelModule } from './_modules/admin-panel/admin-panel.module';
import { NavbarsModule } from './_modules/navbars/navbars.module';
import { ProductsModule } from './_modules/products/products.module';
import { CompaniesModule } from './_modules/companies/companies.module';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    SharedModule,
    AccountModule,
    AdminPanelModule,
    NavbarsModule,
    ProductsModule,
    CompaniesModule
  ],
  exports:[
    NavbarsModule
  ],
  providers: [
    {provide:HTTP_INTERCEPTORS,useClass:ErrorInterceptor,multi:true},
    //{provide:HTTP_INTERCEPTORS,useClass:LoadingInterceptor,multi:true},
    {provide:HTTP_INTERCEPTORS,useClass:JwtInterceptor,multi:true},
    {
      provide: MAT_SELECTSEARCH_DEFAULT_OPTIONS,
      useValue: <MatSelectSearchOptions>{
        noEntriesFoundLabel: 'No options found',
        placeholderLabel:'Search'
      }
    },
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  bootstrap: [AppComponent]
})
export class AppModule { }




