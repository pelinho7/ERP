import { AbstractControl } from '@angular/forms';
import { map } from 'rxjs/operators';
import { AttributeService } from '../_services/attribute.service';

export class AttributeCodeNotTaken {
  static createValidator(attributeService: AttributeService) {
    return (control: AbstractControl) => {
      return attributeService.checkCodeNotTaken(control.value).pipe(
          map(res => {
        return res ? null : {attributeCodeTaken: true};
      }))
    }
  }
}