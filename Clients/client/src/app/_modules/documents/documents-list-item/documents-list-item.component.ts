import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { Document } from '../document';

@Component({
  selector: 'app-documents-list-item',
  templateUrl: './documents-list-item.component.html',
  styleUrls: ['./documents-list-item.component.css']
})
export class DocumentsListItemComponent implements OnInit {

  @Input() document:Document;
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
    this.selectChangedEvent.emit(this.document.id);
  }

  edit(){
    this.router.navigateByUrl('/documents/'+this.document.id)
  }

}
