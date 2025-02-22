import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';

import { DocumentsRoutingModule } from './documents-routing.module';
import { DocumentComponent } from './document/document.component';
import { DocumentsListComponent } from './documents-list/documents-list.component';
import { DocumentsListItemComponent } from './documents-list-item/documents-list-item.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormControlsModule } from '../form-controls/form-controls.module';
import { NavbarsModule } from '../navbars/navbars.module';
import { StaticDataModule } from '../static-data/static-data.module';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { NgxSpinnerModule } from 'ngx-spinner';
import { OutsideClickDirective } from './directives/outside-click.directive';


@NgModule({
  declarations: [
    DocumentComponent,
    DocumentsListComponent,
    DocumentsListItemComponent,
    OutsideClickDirective
  ],
  exports: [
    DocumentComponent,
    DocumentsListComponent,
  ],
  imports: [
    CommonModule,
    DocumentsRoutingModule,
    ReactiveFormsModule,
    FormControlsModule,
    NavbarsModule,
    StaticDataModule,
    PaginationModule,
    FormsModule,
    NgxSpinnerModule,
  ],
  providers: [DatePipe] 
})
export class DocumentsModule { }
