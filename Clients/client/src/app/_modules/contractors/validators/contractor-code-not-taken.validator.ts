import { AbstractControl } from '@angular/forms';
import { map } from 'rxjs/operators';
import { ContractorsService } from '../services/contractors.service';

export class ProductCodeNotTaken {
  static createValidator(contractorsService: ContractorsService,id:number=null) {
    return (control: AbstractControl) => {
      return contractorsService.checkCodeNotTaken(control.value,id).pipe(
          map(res => {
        return res ? null : {codeTaken: true};
      }))
    }
  }
}