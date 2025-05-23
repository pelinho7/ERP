import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountService } from './services/account.service';
import { AuthModule, LogLevel } from 'angular-auth-oidc-client';
import { environment } from 'src/environments/environment';
import { RouterModule } from '@angular/router';
import { LogInComponent } from './log-in/log-in.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AccountRoutingModule } from './account-routing.module';
import { FormControlsModule } from "../form-controls/form-controls.module";
import { RegisterComponent } from './register/register.component';
import { EmailVerificationComponent } from './email-verification/email-verification.component';
import { NewPasswordComponent } from './new-password/new-password.component';
import { AccountDataComponent } from './account-data/account-data.component';
import { ChangePasswordComponent } from './change-password/change-password.component';

@NgModule({
  declarations: [
    
  
    LogInComponent,
    ResetPasswordComponent,
    RegisterComponent,
    EmailVerificationComponent,
    NewPasswordComponent,
    AccountDataComponent,
    ChangePasswordComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    AccountRoutingModule,
    FormControlsModule
],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ],
  providers: []
})
export class AccountModule { }