import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Contractor } from '../contractor';
import { Router } from '@angular/router';

@Component({
  selector: 'app-contractors-list-item',
  templateUrl: './contractors-list-item.component.html',
  styleUrls: ['./contractors-list-item.component.css']
})
export class ContractorsListItemComponent implements OnInit {

  @Input() contractor:Contractor;
  mouseOver:boolean=false;
  selected:boolean=false;
  @Output() selectChangedEvent: EventEmitter<number> = new EventEmitter();

  private _selectMode:boolean=false;
  @Input() set selectMode(value: boolean) {
    if(this._selectMode && !value){
      this.selected=false;
    }
    this._selectMode = value;
 }
 
 get selectMode(): boolean {
     return this._selectMode;
 }

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  mouseEnter(){
    this.mouseOver=true;
  }

  mouseLeave(){
    this.mouseOver=false;
  }

  select(event:any){
    event.stopPropagation();
    this.selected=!this.selected;
    this.selectChangedEvent.emit(this.contractor.id);
  }

  edit(){
    this.router.navigateByUrl('/contractors/'+this.contractor.id)
  }

}
