import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TextInputComponent } from './text-input/text-input.component';
import { ReactiveFormsModule } from '@angular/forms';
import { TextareaComponent } from './textarea/textarea.component';
import { SelectComponent } from './select/select.component';
import { NumberInputComponent } from './number-input/number-input.component';
import { CheckboxInputComponent } from './checkbox-input/checkbox-input.component';
import { SelectSearchComponent } from './select-search/select-search.component';
import { MatSelectModule } from '@angular/material/select';
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';

@NgModule({
  declarations: [
    TextInputComponent,
    TextareaComponent,
    SelectComponent,
    NumberInputComponent,
    CheckboxInputComponent,
    SelectSearchComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatSelectModule,
    NgxMatSelectSearchModule,
  ],
  exports: [
    TextInputComponent,
    TextareaComponent,
    SelectComponent,
    NumberInputComponent,
    CheckboxInputComponent,
    SelectSearchComponent,
  ]
})
export class FormControlsModule { }
