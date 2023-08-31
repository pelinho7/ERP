import { AbstractControl } from '@angular/forms';
import { map } from 'rxjs/operators';
import { ProductsService } from '../services/products.service';

export class ProductCodeNotTaken {
  static createValidator(productsService: ProductsService) {
    return (control: AbstractControl) => {
      return productsService.checkCodeNotTaken(control.value).pipe(
          map(res => {
        return res ? null : {codeTaken: true};
      }))
    }
  }
}