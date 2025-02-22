import { Component, EventEmitter, Input, OnInit, Output, Self } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';

@Component({
  selector: 'app-datepicker',
  templateUrl: './datepicker.component.html',
  styleUrls: ['./datepicker.component.css']
})
export class DatepickerComponent implements ControlValueAccessor {

  @Input() label:string;
  @Input() type = 'number';
  @Input() max:string = null;
  @Input() min:string = null;
  @Input() decimalPlaces:number = 0;
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

  ngAfterViewInit(): void {
  }

}
