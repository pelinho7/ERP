import { Component, Input, OnInit, Self } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR, NgControl } from '@angular/forms';
import { MaskType } from './mask-factory/mask-type';
import { MaskFactory } from './mask-factory/mask-factory';
import { MaskTextInput } from './mask-factory/mask-text-input';

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.css','../control.css'],
})
export class TextInputComponent implements ControlValueAccessor {
  @Input() label:string;
  @Input() type = 'text';
  @Input() upperCaseOnly:boolean = false;

  @Input() set acceptableCharsRegex(acceptableCharsRegex: string) {
    if(this._acceptableCharsRegex==null){
      this._acceptableCharsRegex = new RegExp(acceptableCharsRegex);
    }
  }
  private _acceptableCharsRegex: RegExp=null;

  @Input() set unacceptableCharsRegex(unacceptableCharsRegex: string) {
    if(this._unacceptableCharsRegex==null){
      this._unacceptableCharsRegex = new RegExp(unacceptableCharsRegex);
    }
  }
  private _unacceptableCharsRegex: RegExp=null;

  @Input() set maskType(maskType: MaskType) {
    if(this._maskTextInput == null){
      var factory = new MaskFactory();
      this._maskTextInput = factory.createMask(maskType)
    }
  }
  private _maskTextInput :MaskTextInput=null;
  
  constructor(@Self() public ngControl: NgControl) { 
    this.ngControl.valueAccessor=this;
  }

  writeValue(obj: any): void {
  }
  registerOnChange(fn: any): void {
  }
  registerOnTouched(fn: any): void {
  }

  keypress(event:any){
    if(this._acceptableCharsRegex!=null){
      if (!this._acceptableCharsRegex.test(event.key)) {
        return false;
      }
    }

    if(this._unacceptableCharsRegex!=null){
      if (this._unacceptableCharsRegex.test(event.key)) {
        return false;
      }
    }

    return true;
  }

  maskInputedText(event:any){
    this._maskTextInput?.mask(event)
  }
}
