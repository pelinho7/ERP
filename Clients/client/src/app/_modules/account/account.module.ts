import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountService } from './services/account.service';
import { AuthModule, LogLevel } from 'angular-auth-oidc-client';
import { environment } from 'src/environments/environment';
import { RouterModule } from '@angular/router';


@NgModule({
  declarations: [
    
  ],
  imports: [
    CommonModule,
    RouterModule,
    AuthModule.forRoot({
      config: {
        authority: /*'https://localhost:8001'*/environment.identityUrl,
        // redirectUrl: window.location.origin+'/account/login-redirect',
        redirectUrl: window.location.origin,
        postLogoutRedirectUri: window.location.origin,
        silentRenewUrl: window.location.origin + '/assets/silent-renew.html',
        clientId: 'angular_client',
        scope: 'openid profile erpAPI offline_access',
        // responseType: 'id_token token',
        responseType: 'code',
        silentRenew: true,
        useRefreshToken: true,
        logLevel: LogLevel.Debug,
        autoUserInfo:false,
      },
    }),
  ],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ],
  providers: [AccountService]
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
