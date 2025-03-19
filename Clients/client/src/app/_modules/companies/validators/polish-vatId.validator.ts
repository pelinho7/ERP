import { AbstractControl } from "@angular/forms";
import { of } from "rxjs";

export class PolishVatId {
  static createValidator(countryCodeControl: AbstractControl) {
    return (vatIdControl: AbstractControl) => {
      const countryCode = countryCodeControl.value;
      const vatId:string =vatIdControl.value;
      if((countryCode.length == 0 || countryCode.toLocaleLowerCase() == 'pl')
        && vatId.length > 0){
        const regexIsNumberOnly = /^[0-9]+$/;
        if(vatId.length!=10 || !regexIsNumberOnly.test(vatId)){
          return of({ 'invalidPolishVatID': true });
        }
        else{
          var weights=[6, 5, 7, 2, 3, 4, 5, 6, 7]
          
          var vatDigits=vatId.split('');

          var controlSum=parseInt(vatDigits[9]);
          vatDigits=vatDigits.slice(0,9)
          for (let i = 0; i <= 8; i++) {
            weights[i]*=parseInt(vatDigits[i]);
          }

          var digitsSum=weights.reduce((a, b) => a + b, 0);
          if(digitsSum%11!=controlSum){
            return of({ 'invalidPolishVatID': true });
          }

        }
      }
        

      return of(null);
    }
  }
}