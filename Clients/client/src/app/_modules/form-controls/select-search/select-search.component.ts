import { Component, ElementRef, EventEmitter, Input, OnInit, Output, Self, ViewChild } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';
import { ReplaySubject, Subject } from 'rxjs';

@Component({
  selector: 'app-select-search',
  templateUrl: './select-search.component.html',
  styleUrls: ['./select-search.component.css','../control.css']
})
export class SelectSearchComponent implements ControlValueAccessor {
  @Input() label:string;
  @Input() hideNullValue:boolean=false;
  @Output() valueChangeEvent: EventEmitter<any> = new EventEmitter();

  public filteredData: ReplaySubject<Map<any,string>> = new ReplaySubject<Map<any,string>>(1);
  @ViewChild('filterRef', { read: ElementRef}) inputElement: any;

    @Input() set keyValueMap(data: Map<any,string>) {
      if(this._keyValueMap==null){
        this._keyValueMap = data;
        this.filteredData.next(data);
      }
    }

    get keyValueMap(): Map<any,string> {
      return this._keyValueMap;
    }
    private _keyValueMap: Map<any,string>=null;
  


  constructor(@Self() public ngControl: NgControl) { 
    this.ngControl.valueAccessor=this;
  }

  filter(event:any){
    var filter=(event.target.value as string).toLowerCase();
    if(filter.length>0){
      var keyValueMapTemp:Map<any,string>=new Map();
      var filtered = [...this._keyValueMap.entries()].filter( it => it[1].toLowerCase().includes(filter) ) 
      filtered.forEach(x=>keyValueMapTemp.set(x[0],x[1]))
      this.filteredData.next(keyValueMapTemp);
    }
    else{
      this.filteredData.next(this._keyValueMap);
    }
  }

  cleanFilter(){
    const input = this.inputElement.nativeElement;
    input.value = '';   
    this.filteredData.next(this._keyValueMap);
  }

  setFocusOnFilter(event:any){
    if(event){
      this.inputElement.nativeElement.focus()
    }
  }

  writeValue(obj: any): void {
  }
  registerOnChange(fn: any): void {
  }
  registerOnTouched(fn: any): void {
  }

  change(event:any){
    this.valueChangeEvent.emit(event.value);
  }

  asIsOrder(a:any, b:any) {
    return 1;
  }
}












