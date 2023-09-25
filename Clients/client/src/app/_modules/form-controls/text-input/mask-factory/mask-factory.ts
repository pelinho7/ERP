import { BankaccoutMaskInput } from "./bankaccout-mask-input";
import { MaskTextInput } from "./mask-text-input";
import { MaskType } from "./mask-type";

export class MaskFactory {

    createMask(maskType:MaskType):MaskTextInput{
        if(maskType === MaskType.Bankaccount){
            return new BankaccoutMaskInput()
        }
        else{
            console.log("Unsupported mask");
            return null;
        }
    }
}