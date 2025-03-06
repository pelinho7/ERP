import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LogInComponent } from './log-in/log-in.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { RegisterComponent } from './register/register.component';
import { EmailVerificationComponent } from './email-verification/email-verification.component';
import { NewPasswordComponent } from './new-password/new-password.component';
import { AccountDataComponent } from './account-data/account-data.component';
import { ChangePasswordComponent } from './change-password/change-password.component';

export const routes: Routes = [
  {path:'login',component:LogInComponent},
  {path:'register',component:RegisterComponent},
  {path:'email-confirmation',component:EmailVerificationComponent},
  {path:'new-password',component:NewPasswordComponent},
  {path:'data',component:AccountDataComponent},
  {path:'change-password',component:ChangePasswordComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AccountRoutingModule { }
