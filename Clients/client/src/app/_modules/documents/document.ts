import { DocItem } from "./doc-item";

export class Document {
    id: number=0;
    
    contrId: number=0;
    contrCode:string='';
    contrCountryCode:string='';
    contrVatId:string='';
    contrName:string;
    contrStreet:string='';
    contrStreetNo:string='';
    contrApartmentNo:string='';
    contrZipCode:string='';
    contrPostalOffice:string='';
    contrCountry:string='';
    contrCity:string='';
    contrPhone:string='';
    contrEmail:string='';

    recId: number=0;
    recCode:string='';
    recCountryCode:string='';
    recVatId:string='';
    recName:string;
    recStreet:string='';
    recStreetNo:string='';
    recApartmentNo:string='';
    recZipCode:string='';
    recPostalOffice:string='';
    recCountry:string='';
    recCity:string='';
    recPhone:string='';
    recEmail:string='';
    recStatus:number=0;

    

    
    splitPayment:boolean=false;
    

    dateOfIssue:Date=new Date('2022-1-1')

    items:DocItem[]=[];

    constructor(){}
}