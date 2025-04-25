import { CompanyBasic } from "./companyBasic";
import { CompanyUser } from "./companyUser";

export class Company extends CompanyBasic {
    // id: number=0;
    // countryCode:string='';
    // vatId:string='';
    // name:string;

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

    bankAccountNumber:string='';
    swiftCode:string='';

    companyCurrentUser:CompanyUser=null;

    constructor(){
        super();
    }
}