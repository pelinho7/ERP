import { Injectable } from '@angular/core';
import { AbstractControl, UntypedFormArray, UntypedFormGroup, ValidatorFn } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class FormHelpersService {

  constructor() { }

  getControlsFromArray(form:UntypedFormGroup,groupName:string){
    return (<UntypedFormArray>form.controls[groupName]).controls;
  }

  getControlFromGroup(formGroup:UntypedFormGroup,controlName:string){
    return (formGroup.controls[controlName]);
  }

  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      if (control.parent && control.parent.controls) {
        return control?.value === (control?.parent?.controls as { [key: string]: AbstractControl })[matchTo].value ? null : {isMatching: true}
      }
      return null;
    }
  }
}
