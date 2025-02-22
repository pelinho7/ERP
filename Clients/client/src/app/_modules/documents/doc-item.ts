export class DocItem {
    id: number=0;
    productId: number=0;
    code:string='';
    ean:string='';
    quantity:number=0;
    price:number=0;
    discount:number=0;
    discountedPrice:number=0;
    pkWiU:string='';
    name:string;
    description:string='';
    unit:string='szt.';
    vatObject:string;
    vatValue:number=23;
    vatFlag:number=0;
    
    constructor(){}
}