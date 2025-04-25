export class CompanyUser {
    id: number=0;
    companyId: number=0;
    userId: number=0;
    administrator:boolean=false;
    contractorsModuleRead:boolean=false;
    contractorsModuleWrite:boolean=false;
    productsModuleRead:boolean=false;
    productsModuleWrite:boolean=false;
    archived:boolean=false;

    userEmail: string='';
    
    constructor(){}
}