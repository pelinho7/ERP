import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TopNavbarComponent } from './top-navbar/top-navbar.component';
import { SideNavbarComponent } from './side-navbar/side-navbar.component';
import { ResizeWindowWatcherService } from './services/resize-window-watcher.service';
import { AccountService } from '../account/services/account.service';
import { AccountModule } from '../account/account.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { FormNavComponent } from './form-nav/form-nav.component';


@NgModule({
  declarations: [
    TopNavbarComponent,
    SideNavbarComponent,
    FormNavComponent
  ],
  imports: [
    CommonModule,
    AccountModule,
    // BrowserAnimationsModule,
    BsDropdownModule
  ],
  exports: [
    TopNavbarComponent,
    SideNavbarComponent,
    FormNavComponent,
  ],
  providers: [ResizeWindowWatcherService]
})
export class NavbarsModule { }
