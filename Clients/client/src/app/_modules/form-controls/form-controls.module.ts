import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TextInputComponent } from './text-input/text-input.component';
import { ReactiveFormsModule } from '@angular/forms';
import { TextareaComponent } from './textarea/textarea.component';
import { SelectComponent } from './select/select.component';
import { NumberInputComponent } from './number-input/number-input.component';
import { CheckboxInputComponent } from './checkbox-input/checkbox-input.component';



@NgModule({
  declarations: [
    TextInputComponent,
    TextareaComponent,
    SelectComponent,
    NumberInputComponent,
    CheckboxInputComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  exports: [
    TextInputComponent,
    TextareaComponent,
    SelectComponent,
    NumberInputComponent,
    CheckboxInputComponent
  ]
})
export class FormControlsModule { }
