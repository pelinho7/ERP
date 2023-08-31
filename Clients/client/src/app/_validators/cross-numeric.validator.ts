import { AbstractControl, UntypedFormControl, FormGroup, NG_VALIDATORS, ValidationErrors, Validator } from '@angular/forms';
import { Directive, Input } from '@angular/core';
  

@Directive({
    selector: '[appCrossNumericValidator]',
    providers: [{ provide: NG_VALIDATORS, useExisting: CrossNumericValidatorDirective, multi: true }]
  })
  export class CrossNumericValidatorDirective implements Validator {
    @Input('appCrossNumericValidator') appCrossNumericValidator: string;

    validate(control: UntypedFormControl): ValidationErrors | null {//AbstractControl
      
        const numericFieldsPair: NumericFieldsPair = JSON.parse(this.appCrossNumericValidator);

        const from = control.root.get(numericFieldsPair.from);
        const to = control.root.get(numericFieldsPair.to);

        // return null if controls haven't initialised yet
        if (!from || !to) {
          return null;
        }

        // return null if another validator has already found an error on the "from control"
        if (from.errors && !from.errors.appCrossNumericValidator) {
            return null;
        }
        // return null if another validator has already found an error on the "to control"
        if (to.errors && !to.errors.appCrossNumericValidator) {
          return null;
        }

        //if from value is greatet then to value change value of the opposite control
        if (from?.value!==null && to?.value!==null && from?.value>to?.value) {
          if(from?.value!=control.value){
            from?.setValue(control.value, {emitEvent:false});
          }
          if(to?.value!=control.value){
            to?.setValue(control.value, {emitEvent:false});
          }
        } 
        else {
          from?.setErrors(null);
          to?.setErrors(null);
        }
        
        return null;
    }
  }

  export interface NumericFieldsPair {
    from: string;
    to:string;
}