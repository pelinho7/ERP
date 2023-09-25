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

  getPaymentForms():Map<number,string>{
    var types: Map<number, string> = new Map();
    types.set(0, "Cash");
    types.set(1, "Transfer");
    types.set(2, "Card");

    return types;
  }

  getCountryPrefixes():Map<string,string>{
    var prefixes: Map<string, string> = new Map();
    prefixes.set("", "");
    prefixes.set("PL", "PL");
    prefixes.set("DE", "DE");
    prefixes.set("IT", "IT");

    return prefixes;
  }

  getContractorTypes():Map<number,string>{
    var types: Map<number, string> = new Map();
    types.set(0, "Natural person");
    types.set(1, "Business Entity");

    return types;
  }

  getContractorStatuses():Map<number,string>{
    var types: Map<number, string> = new Map();
    types.set(0, "Domestic");
    types.set(1, "Intra-EU");
    types.set(2, "Non-EU");
    types.set(3, "Trilateral Intra-EU");
    types.set(4, "OSS procedure");

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
