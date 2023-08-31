import { Component, ElementRef, EventEmitter, Input, OnInit, Output, Self, ViewChild } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';

@Component({
  selector: 'app-number-input',
  templateUrl: './number-input.component.html',
  styleUrls: ['./number-input.component.css','../control.css']
})
export class NumberInputComponent implements ControlValueAccessor {
  @Input() label:string;
  @Input() type = 'number';
  @Input() max:number = null;
  @Input() min:number = null;
  @Input() decimalPlaces:number = 0;
  @ViewChild('numberInput') input:ElementRef;
  @Output() valueChangeEvent: EventEmitter<number> = new EventEmitter();
  
  constructor(@Self() public ngControl: NgControl) { 
    this.ngControl.valueAccessor=this;
    // this.input.nativeElement.value=parseFloat(this.input.nativeElement.value).toFixed(this.decimalPlaces)
  }

  writeValue(obj: any): void {
  }
  registerOnChange(fn: any): void {
  }
  registerOnTouched(fn: any): void {
  }

  ngAfterViewInit(): void {
      this.input.nativeElement.value=parseFloat(this.input.nativeElement.value).toFixed(this.decimalPlaces)
  }

  setDecimalPlaces($event:any) {
    $event.target.value = parseFloat($event.target.value).toFixed(this.decimalPlaces);
  }

  getStep(){
      return 1/(Math.pow(10,this.decimalPlaces));
  }

  change(value:number){
    this.valueChangeEvent.emit(value);
  }
}
