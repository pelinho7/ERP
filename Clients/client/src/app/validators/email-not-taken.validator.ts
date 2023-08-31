import { AbstractControl } from '@angular/forms';
import { map } from 'rxjs/operators';
import { AccountService } from '../_services/account.service';

export class ValidateEmailNotTaken {
  static createValidator(accountService: AccountService) {
    return (control: AbstractControl) => {
      return accountService.checkEmailNotTaken(control.value).pipe(
          map(res => {
        return res ? null : {emailTaken: true};
      }))
    }
  }
}