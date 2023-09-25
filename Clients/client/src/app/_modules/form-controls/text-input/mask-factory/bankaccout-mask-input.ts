import { MaskTextInput } from "./mask-text-input";

export class BankaccoutMaskInput implements MaskTextInput {
    constructor(){}
    mask(event:any){
        var value:string=event.target.value;
        //unmask text
        value=value.split(" ").join("");
        //get current cursor position
        var cursorPosition=event.target.selectionStart;
        //
        var matches = value.match(/(?<=\d{2})\d{1,4}/g)
    
        if(matches != null){
          value = value.substring(0,2)+matches.map(x => ' '+x).join('')
          //update cursor position when space is added
          if(cursorPosition==event.target.value.length && event.target.value.length<value.length){
            cursorPosition++;
          }
        }
        //set new value
        event.target.value=value;
        //reload cursor position
        event.target.selectionStart=cursorPosition
        event.target.selectionEnd=cursorPosition
    }
}