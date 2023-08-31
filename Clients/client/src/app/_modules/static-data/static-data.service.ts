import { Injectable } from '@angular/core';
import { Vat } from './models/vat';

@Injectable({
  providedIn: 'root'
})
export class StaticDataService {

  constructor() { }

  getCurrencies():Map<string,string>{
    var currencies: Map<string, string> = new Map();
    currencies.set("PLN", "PLN");
    currencies.set("EUR", "EUR");
    currencies.set("USD", "USD");
    currencies.set("GBP", "GBP");

    return currencies;
  }

  getProductTypes():Map<number,string>{
    var types: Map<number, string> = new Map();
    types.set(0, "service");
    types.set(1, "item");

    return types;
  }

  getPolishVats():Map<string,string>{
    var vats: Map<string, string> = new Map();
    vats.set(JSON.stringify(new Vat(23)), "23%");
    vats.set(JSON.stringify(new Vat(8)), "8%");
    vats.set(JSON.stringify(new Vat(7)), "7%");
    vats.set(JSON.stringify(new Vat(5)), "5%");
    vats.set(JSON.stringify(new Vat(4)), "4%");
    vats.set(JSON.stringify(new Vat(0)), "0%");
    vats.set(JSON.stringify(new Vat(0,1)), "Exempt");
    vats.set(JSON.stringify(new Vat(0,2)), "Non taxable");

    return vats;
  }

  getVatJsonObject(vatValue:number, vatFlag:number=0):string{
    return JSON.stringify(new Vat(vatValue,vatFlag));
  }

  getVatObjectFromJsonObject(getVatJsonObject:string){
    var vatObject = JSON.parse(getVatJsonObject) as Vat;
    return vatObject;
  }
}
