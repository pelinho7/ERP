import { Component, ElementRef, HostListener, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Product } from '../product';
import { Location } from '@angular/common';
import { ResizeWindowWatcherService } from '../../navbars/services/resize-window-watcher.service';
import { StaticDataService } from '../../static-data/static-data.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmService } from '../../message-dialogs/services/confirm.service';
import { ProductsService } from '../services/products.service';
import { ToastrService } from 'ngx-toastr';
import { LoadingScreenService } from '../../utilities/services/loading-screen.service';
import { ProductCodeNotTaken } from '../validators/product-code-not-taken.validator';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css','../../../shared css/list-item.css']
})
export class ProductComponent implements OnInit {

  productForm:FormGroup;
  editMode:boolean=false;
  itemProduct:boolean=false;
  product:Product
  loaded:boolean=false;
  saved:boolean=false;
  title:string='New item';
  navItems:string[]=["General","Product Codes","Parameters","Additional"]
  constructor(private fb:FormBuilder,private elem: ElementRef
    ,public resizeWindowWatcherService:ResizeWindowWatcherService
    ,public staticDataService:StaticDataService
    ,private router: Router
    ,private location: Location
    ,private confirmService:ConfirmService
    ,private productsService:ProductsService
    ,private toastr: ToastrService
    ,private loadingScreenService:LoadingScreenService
    ,private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.params.subscribe(x=>{
      console.log()
      var id=x['id'];
      if(id!='add'){
        this.getProduct(id);
      }
      else{
        this.product=new Product();
        this.createProductForm();
      }
    })
   

    // if(!this.editMode){
    //   this.productForm.controls['code'].setAsyncValidators(ProductCodeNotTaken.createValidator(this.productsService));
    // }
  }

  createProductForm(){
    this.product.vatObject=this.staticDataService.getVatJsonObject(this.product.vatValue,this.product.vatFlag)
  
    this.setStockInputsVisibility(this.product.type);

    this.productForm=this.fb.group({
      id:[this.product.id,Validators.required],
      code:[{value: this.product.code, disabled: this.editMode},[Validators.required,Validators.maxLength(30)]],
      name:[this.product.name,[Validators.required,Validators.maxLength(100)]],
      ean:[this.product.ean,[Validators.required,Validators.maxLength(13)]],
      pkwiu:[this.product.pkwiu],
      type:[this.product.type],
      unit:[this.product.unit,Validators.required],
      stockLevelControl:[this.product.stockLevelControl],
      stockLevel:[this.product.stockLevel],
      vatObject:[this.product.vatObject],
      saleCurrency:[this.product.saleCurrency],
      saleNetPrice:[this.product.saleNetPrice,Validators.min(0.01)],
      saleGrossPrice:[this.product.saleGrossPrice,Validators.min(0.01)],
      purchaseCurrency:[this.product.purchaseCurrency],
      purchaseNetPrice:[this.product.purchaseNetPrice,Validators.min(0.01)],
      purchaseGrossPrice:[this.product.purchaseGrossPrice,Validators.min(0.01)],
      description:[this.product.descriprion],
      splitPayment:[this.product.splitPayment],
    })

    if(!this.editMode){
      this.productForm.controls['code'].setAsyncValidators(ProductCodeNotTaken.createValidator(this.productsService,this.product.id));
    }

    this.loaded=true;
  }

  setStockInputsVisibility(productType:number){
    if(productType!=0){
      this.productForm.patchValue({
        stockLevelControl: false, 
      });
    }
    this.itemProduct=productType==1;
    if(productType==1){
      this.productForm.patchValue({
        stockLevelControl: true, 
      });
    }
  }

  scrollToSection(sectionId:number){
    let sections = this.elem.nativeElement.querySelectorAll('.form-section');
    sections[sectionId].scrollIntoView();
  }

  purchaseNetPriceChanged(value:number){
    var vat =this.staticDataService.getVatObjectFromJsonObject(this.productForm.get('vatObject').value)
    this.productForm.patchValue({
      purchaseGrossPrice: value*(vat.vatValue+100)/100, 
    });
  }

  saleNetPriceChanged(value:number){
    var vat =this.staticDataService.getVatObjectFromJsonObject(this.productForm.get('vatObject').value)
    this.productForm.patchValue({
      saleGrossPrice: value*(vat.vatValue+100)/100, 
    });
  }

  typeChanged(value:number){
    this.setStockInputsVisibility(value);
  }

  stockLevelControlChanged(value:boolean){
    //this.setStockInputsVisibility(value);
  }

  close(){
    if(this.productForm.dirty){
      this.confirmService.confirm('Changes have not been saved', 'If you close the form you will loose the unsaved data. Are you sure you want to close the form without saving?').subscribe(result=>{

        if(result){
          this.location.back();
        }
      })
    }
    else{
      this.location.back();
    }
  }

  save(){
    if(this.productForm.valid){
      var product=(this.productForm.value as Product);
      var vat =this.staticDataService.getVatObjectFromJsonObject(product.vatObject)
      product.vatFlag=vat.vatFlag;
      product.vatValue=vat.vatValue;

      this.loadingScreenService.show();
      console.log(product)
      // product.descriprion='';
      // product.pkwiu='';
      if(this.editMode){
        this.update(product);
      }
      else{
        this.add(product);
      }
    }
  }

  add(product:Product){
    this.productsService.add(product).subscribe(x=>{
      this.saved=true;
    })
    .add(()=>{
      this.loadingScreenService.hide();
    })
  }

  update(product:Product){
    this.productsService.update(product).subscribe(x=>{
      this.saved=true;
    })
    .add(()=>{
      this.loadingScreenService.hide();
    })
  }

  getProduct(id:number){
    this.loadingScreenService.show('product-spinner');
    this.productsService.getById(id).subscribe(x=>{
      this.product=x;
      this.editMode=true;
      this.title=this.product.code;
      this.createProductForm();
      //console.log(x)
    })
    .add(()=>{
      this.loadingScreenService.hide('product-spinner');
    })
  }
}
