import { AbstractControl } from '@angular/forms';
import { map } from 'rxjs/operators';
import { AccountService } from '../_services/account.service';

export class ValidateLoginNotTaken {
  static createValidator(accountService: AccountService) {
    return (control: AbstractControl) => {
      return accountService.checkLoginNotTaken(control.value).pipe(
          map(res => {
        return res ? null : {loginTaken: true};
      }))
    }
  }
}