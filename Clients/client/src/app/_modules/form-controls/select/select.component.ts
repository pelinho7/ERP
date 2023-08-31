import { Component, EventEmitter, Input, OnInit, Output, Self } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';

@Component({
  selector: 'app-select',
  templateUrl: './select.component.html',
  styleUrls: ['./select.component.css','../control.css']
})
export class SelectComponent implements ControlValueAccessor {
  @Input() label:string;
  @Input() keyValueMap:Map<any,string>;
  @Input() hideNullValue:boolean=false;
  @Output() valueChangeEvent: EventEmitter<number> = new EventEmitter();

  
  constructor(@Self() public ngControl: NgControl) { 
    this.ngControl.valueAccessor=this;
  }

  writeValue(obj: any): void {
  }
  registerOnChange(fn: any): void {
  }
  registerOnTouched(fn: any): void {
  }

  change(value:number){
    this.valueChangeEvent.emit(value);
  }

  asIsOrder(a:any, b:any) {
    return 1;
  }
}
