import { AbstractControl } from '@angular/forms';
import { map } from 'rxjs/operators';
import { AccountService } from '../services/account.service';

export class ValidateEmailNotTaken {
  static createValidator(accountService: AccountService,id:number=null) {
    return (control: AbstractControl) => {
      return accountService.checkEmailNotTaken(control.value,id).pipe(
          map(res => {
        return res ? null : {emailTaken: true};
      }))
    }
  }
}