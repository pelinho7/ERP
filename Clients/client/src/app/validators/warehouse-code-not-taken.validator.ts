import { AbstractControl } from '@angular/forms';
import { map } from 'rxjs/operators';
import { WarehouseService } from '../_services/warehouse.service';

export class WarehouseCodeNotTaken {
  static createValidator(warehouseService: WarehouseService) {
    return (control: AbstractControl) => {
      return warehouseService.checkCodeNotTaken(control.value).pipe(
          map(res => {
        return res ? null : {warehouseCodeTaken: true};
      }))
    }
  }
}