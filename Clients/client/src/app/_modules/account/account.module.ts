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

@NgModule({
  declarations: [
    
  
    LogInComponent,
            ResetPasswordComponent,
            RegisterComponent,
            EmailVerificationComponent,
            NewPasswordComponent
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



// import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
// import { CommonModule } from '@angular/common';
// import { RouterModule } from '@angular/router';

// import { AccountRoutingModule } from './account-routing.module';
// import { AccountComponent } from './account.component';


// @NgModule({
//   declarations: [
//     AccountComponent,
//   ],
//   imports: [
//     CommonModule,
//     RouterModule,
//     AccountRoutingModule
//   ],
//   schemas: [
//     CUSTOM_ELEMENTS_SCHEMA
//   ],
//   bootstrap: [AccountComponent]
// })
// export class AdminPanelModule { }
