export class Vat {
    // id: number;
    vatValue:number;
    vatFlag:number;
    // textName:string;
    // constructor(id: number,vatValue:number,vatFlag:number,textName:string){
    //     this.id=id;
    //     this.vatValue=vatValue;
    //     this.vatFlag=vatFlag;
    //     this.textName=textName;
    // }
    constructor(vatValue:number,vatFlag:number=0){
        this.vatValue=vatValue;
        this.vatFlag=vatFlag;
    }
}