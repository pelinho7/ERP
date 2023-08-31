import { AbstractControl } from '@angular/forms';
import { map } from 'rxjs/operators';
import { ProductsService } from '../services/products.service';

export class ProductCodeNotTaken {
  static createValidator(productsService: ProductsService,id:number=null) {
    return (control: AbstractControl) => {
      return productsService.checkCodeNotTaken(control.value,id).pipe(
          map(res => {
        return res ? null : {codeTaken: true};
      }))
    }
  }
}