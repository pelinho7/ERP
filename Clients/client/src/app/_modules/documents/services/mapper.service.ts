import { Injectable } from '@angular/core';
import { Product } from '../../products/product';
import { DocItem } from '../doc-item';

@Injectable({
  providedIn: 'root'
})
export class MapperService {

  constructor() { }

  product2DocItem(product:Product):DocItem{
    var docItem=new DocItem();
    docItem.code=product.code;
    docItem.name=product.name;

    return docItem;
  }
}
