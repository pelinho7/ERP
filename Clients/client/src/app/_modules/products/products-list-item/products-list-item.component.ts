import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Product } from '../product';
import { Router } from '@angular/router';

@Component({
  selector: 'app-products-list-item',
  templateUrl: './products-list-item.component.html',
  styleUrls: ['./products-list-item.component.css']
})
export class ProductsListItemComponent implements OnInit {

  @Input() product:Product;
  mouseOver:boolean=false;
  selected:boolean=false;
  //@Input() selectMode:boolean=false;
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
    this.selectChangedEvent.emit(this.product.id);
  }

  edit(){
    this.router.navigateByUrl('/products/'+this.product.id)
  }
  
}
