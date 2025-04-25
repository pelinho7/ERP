import { UserBase } from "./userBase";

export class User extends UserBase {
    companyId:number;
    companyType:number;
    token :string;
}