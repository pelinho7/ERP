export class Product {
    id: number=0;
    code:string='';
    ean:string;
    pkWiU:string='';
    name:string;
    description:string='';
    unit:string='szt.';
    vatObject:string;
    vatValue:number=23;
    vatFlag:number=0;
    splitPayment:boolean=false;
    type:number=0;
    saleCurrency:string='PLN';
    saleNetPrice:number=0;
    saleGrossPrice:number=0;
    purchaseCurrency:string='PLN';
    purchaseNetPrice:number=0;
    purchaseGrossPrice:number=0;
    stockLevel:number=0;
    stockLevelControl:boolean=false;
    constructor(){}
}