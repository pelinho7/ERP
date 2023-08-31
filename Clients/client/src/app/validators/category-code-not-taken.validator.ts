import { AbstractControl } from '@angular/forms';
import { map } from 'rxjs/operators';
import { CategoryService } from '../_services/category.service';

export class CategoryCodeNotTaken {
  static createValidator(categoryService: CategoryService) {
    return (control: AbstractControl) => {
      return categoryService.checkCodeNotTaken(control.value).pipe(
          map(res => {
        return res ? null : {categoryCodeTaken: true};
      }))
    }
  }
}