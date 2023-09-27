export class Contractor {
    id: number=0;
    code:string='';
    repFirstName:string='';
    repLastName:string='';
    countryCode:string='';
    vatId:string='';
    name:string;

    // public set code(code: string) {
    //     console.log('this._code')
    //     this._code=code.toUpperCase()
    //             console.log(this._code)
    // }
    // public get code() {
    //     return 'ggg'//this._code.toUpperCase()
    // }
    // private _code: string='';

    street:string='';
    streetNo:string='';
    apartmentNo:string='';
    zipCode:string='';
    postalOffice:string='';
    country:string='';
    city:string='';

    phone:string='';
    email:string='';
    mobile:string='';
    fax:string='';
    wwwAddress:string='';

    discount:number=0;
    paymentForm:number=0;
    bankAccountNumber:string='';
    swiftCode:string='';

    additionalInformation:string='';
    note:string='';
    splitPayment:boolean=false;
    type:number=0;
    status:number=0;
    activeTaxpayer:boolean=false;

    constructor(){}
}