import { Component, ElementRef, OnInit } from '@angular/core';
import { Document } from '../document';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MaskType } from '../../form-controls/text-input/mask-factory/mask-type';
import { ResizeWindowWatcherService } from '../../navbars/services/resize-window-watcher.service';
import { StaticDataService } from '../../static-data/static-data.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmService } from '../../message-dialogs/services/confirm.service';
import { DocumentsService } from '../services/documents.service';
import { ToastrService } from 'ngx-toastr';
import { LoadingScreenService } from '../../utilities/services/loading-screen.service';
import { DatePipe, Location } from '@angular/common';
import { ProductsService } from '../../products/services/products.service';
import { ReplaySubject } from 'rxjs';
import { Product } from '../../products/product';
import { PurchaseSale } from '../enums/purchase-sale';
import { DocItem } from '../doc-item';


@Component({
  selector: 'app-document',
  templateUrl: './document.component.html',
  styleUrls: ['./document.component.css','../../../shared css/list-item.css']
})
export class DocumentComponent implements OnInit {

  documentForm:FormGroup;
  editMode:boolean=false;
  itemdocument:boolean=false;
  document:Document
  loaded:boolean=false;
  saved:boolean=false;
  title:string='New item';
  navItems:string[]=["General","Address","Contact","Trade","Additional"]
  public maskType: typeof MaskType = MaskType;
  public purchaseSaleEnum: typeof PurchaseSale = PurchaseSale;
  public purchaseSale:PurchaseSale=PurchaseSale.Sale;

  public filteredProducts: ReplaySubject<Product[]> = new ReplaySubject<Product[]>(1);
  private products:Product[]=[]

  constructor(private fb:FormBuilder,private elem: ElementRef
    ,public resizeWindowWatcherService:ResizeWindowWatcherService
    ,public staticDataService:StaticDataService
    ,private router: Router
    ,private location: Location
    ,private confirmService:ConfirmService
    ,private documentsService:DocumentsService
    ,private toastr: ToastrService
    ,private loadingScreenService:LoadingScreenService
    ,private route: ActivatedRoute
    ,private datePipe: DatePipe
    ,private productsService:ProductsService) { }

  ngOnInit(): void {
    console.log('555555')
    this.route.params.subscribe(x=>{
      var id=x['id'];
      if(id!='add'){
        this.getDocument(id);
      }
      else{
        this.document=new Document();
        this.createDocumentForm();
      }
    })
  }

  createDocumentForm(){ 
    this.documentForm=this.fb.group({
      id:[this.document.id,Validators.required],
      test:['this.document.id',Validators.required],

      //dateOfIssue:['29.08.2023'],
      dateOfIssue:[this.datePipe.transform(this.document.dateOfIssue,"yyyy-MM-dd"), Validators.required],

      contrName:[this.document.contrName,[Validators.required,Validators.maxLength(300)]],
      contrCountryCode:[this.document.contrCountryCode],
      contrVatId:[this.document.contrVatId,[Validators.maxLength(13)]],
      contrStreet:[this.document.contrStreet,[Validators.maxLength(150)]],
      contrStreetNo:[this.document.contrStreetNo,[Validators.maxLength(10)]],
      contrApartmentNo:[this.document.contrApartmentNo,[Validators.maxLength(10)]],
      contrZipCode:[this.document.contrZipCode,[Validators.maxLength(6)]],
      contrPostalOffice:[this.document.contrPostalOffice,[Validators.maxLength(120)]],
      contrCity:[this.document.contrCity,[Validators.maxLength(120)]],
      contrCountry:[this.document.contrCountry,[Validators.maxLength(80)]],
      contrPhone:[this.document.contrPhone,[Validators.maxLength(30)]],
      contrEmail:[this.document.contrEmail,[Validators.email,Validators.maxLength(50)]],

      recName:[this.document.recName,[Validators.required,Validators.maxLength(300)]],
      recCountryCode:[this.document.recCountryCode],
      recVatId:[this.document.recVatId,[Validators.maxLength(13)]],
      recStreet:[this.document.recStreet,[Validators.maxLength(150)]],
      recStreetNo:[this.document.recStreetNo,[Validators.maxLength(10)]],
      recApartmentNo:[this.document.recApartmentNo,[Validators.maxLength(10)]],
      recZipCode:[this.document.recZipCode,[Validators.maxLength(6)]],
      recPostalOffice:[this.document.recPostalOffice,[Validators.maxLength(120)]],
      recCity:[this.document.recCity,[Validators.maxLength(120)]],
      recCountry:[this.document.recCountry,[Validators.maxLength(80)]],
      recPhone:[this.document.recPhone,[Validators.maxLength(30)]],
      recEmail:[this.document.recEmail,[Validators.email,Validators.maxLength(50)]],
      recStatus:[this.document.recStatus],

      items: this.fb.array([])
    })
    //console.log(this.document.dateOfIssue.toISOString().split('T')[0])

    // var timeZoneDifference = (this.document.dateOfIssue.getTimezoneOffset() / 60) * -1; //convert to positive value.
    // this.document.dateOfIssue.setTime(this.document.dateOfIssue.getTime() + (timeZoneDifference * 60) * 60 * 1000);
    // this.document.dateOfIssue.toISOString()

    this.document.dateOfIssue.setMonth(1)
    console.log(this.datePipe.transform(this.document.dateOfIssue,"yyyy-MM-dd"))
    console.log(this.document.dateOfIssue.getUTCFullYear())

    // this.documentForm.controls['code'].setAsyncValidators(documentCodeNotTaken.createValidator(this.documentsService,this.document.id));
    // this.documentForm.controls['vatId'].setAsyncValidators(documentPolishVatId.createValidator(this.documentForm.controls['countryCode']));

    this.loaded=true;
    this.loadingScreenService.show('document-products-list-spinner');
    this.productsService.getAll().subscribe(x=>{
      this.products=x;
      this.filteredProducts.next(x)
      console.log(x)
    })
    .add(()=>{
      this.loadingScreenService.hide('document-products-list-spinner');
    })
  }

  // Getter dla łatwego dostępu do formArray
  get items():FormArray {
    return this.documentForm.get('items') as FormArray;
  }

  initItems(docItem:DocItem=null): FormGroup {

    var item=this.fb.group({
      code: [docItem?.code, Validators.required],
      name: ['', Validators.required],
      ean: [''],
      productId: [Validators.required],
      quantity: [0, Validators.required],
      vatObject:[],
      unit: ['szt.', Validators.required],
      price: [0, Validators.required],
      discount: [0, Validators.required],
      discountedPrice: [0, Validators.required],
      description: [''],
      pkWiU: [''],
      editMode: [true],
      added: [false],
    });

    return item;
  }


  addItem(index:number) {
    this.items.controls[index].get('added').patchValue(true)
    this.items.controls[index].get('editMode').patchValue(false)
  }

  initItem() {
    this.items.push(this.initItems());
  }

  saveItem(index:number) {
    this.items.at(index).markAllAsTouched();
    if(this.items.at(index).valid){

    }
  }

  deleteItem(index:number) {
    this.items.removeAt(index)
  }

  editItem(index:number) {
    this.items.controls[index].get('editMode').patchValue(true)
  }

  cancelEditingItem(index:number) {
    if(this.items.controls[index].get('added').value){
      this.items.controls[index].get('editMode').patchValue(false)
    }
    else{
      this.items.removeAt(index)
    }
  }

  countryCodeChanged(event:any){
    this.documentForm.controls['vatId'].updateValueAndValidity();
  }

  scrollToSection(sectionId:number){
    let sections = this.elem.nativeElement.querySelectorAll('.form-section');
    sections[sectionId].scrollIntoView();
  }

  close(){
    if(this.documentForm.dirty){
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
    var document=(this.documentForm.value as Document);
    //document.code=document.code.toUpperCase();
    console.log(document)
    return;
    this.documentForm.markAllAsTouched();
    
    if(this.documentForm.valid){
      var document=(this.documentForm.value as Document);
      
      this.loadingScreenService.show();

      if(this.editMode){
        this.update(document);
      }
      else{
        this.add(document);
      }
    }
  }

  add(document:Document){
    this.documentsService.add(document).subscribe(x=>{
      this.saved=true;
    })
    .add(()=>{
      this.loadingScreenService.hide();
    })
  }

  update(document:Document){
    this.documentsService.update(document).subscribe(x=>{
      this.saved=true;
    })
    .add(()=>{
      this.loadingScreenService.hide();
    })
  }


  getDocument(id:number){
    this.loadingScreenService.show('document-spinner');
    this.documentsService.getById(id).subscribe(x=>{
      this.document=x;
      this.editMode=true;
      //this.title=this.document.code;
      this.createDocumentForm();
      //console.log(x)
    })
    .add(()=>{
      this.loadingScreenService.hide('document-spinner');
    })
  }

  addNew(){
    this.saved=false;
    this.router.navigateByUrl('/documents/add')
  }


  //products combobox region
  productChanged(product:Product,itemIndex:number,el: HTMLElement){
    var price=this.purchaseSale == PurchaseSale.Sale? product.saleNetPrice:product.purchaseGrossPrice;
    product.vatObject=this.staticDataService.getVatJsonObject(product.vatValue,product.vatFlag)

    this.items.controls[itemIndex].get('name').patchValue(product.name)
    this.items.controls[itemIndex].get('code').patchValue(product.code)
    this.items.controls[itemIndex].get('ean').patchValue(product.ean)
    this.items.controls[itemIndex].get('unit').patchValue(product.unit)
    this.items.controls[itemIndex].get('productId').patchValue(product.id)
    this.items.controls[itemIndex].get('pkWiU').patchValue(product.pkWiU)
    this.items.controls[itemIndex].get('ean').patchValue(product.ean)
    this.items.controls[itemIndex].get('description').patchValue('dsadsad')
    this.items.controls[itemIndex].get('vatObject').patchValue(product.vatObject)
    this.items.controls[itemIndex].get('price').patchValue(price)
    this.items.controls[itemIndex].get('discountedPrice').patchValue(price)
    this.items.controls[itemIndex].get('discount').patchValue(0)
    //close dropdown afer select
    el.classList.remove('d-block')
  }

  productNameChanged(text:string,el: HTMLElement,itemIndex:number){
    var temp = this.products.filter(x=>
      x.name.toUpperCase().includes(text.toUpperCase())
      || x.code.toUpperCase().includes(text.toUpperCase())
      || x.ean.toUpperCase().includes(text.toUpperCase()));
    this.filteredProducts.next(temp);
    el.classList.add('d-block')
    this.items.controls[itemIndex].get('productId').patchValue(0)
  }

  productNameInputFocusLost(el: HTMLElement,itemIndex:number){
    if(!el.classList.contains('mouse-enter')){
      el.classList.remove('d-block')
      this.adjustTheNameToTheSelectedProduct(itemIndex)
    }
  }

  adjustTheNameToTheSelectedProduct(itemIndex:number){
    var productId= this.items.controls[itemIndex].get('productId').value;
    if(productId == 0){
      this.items.controls[itemIndex].get('name').patchValue('');
    }
    else{
      var product = this.products.find(x=>
        x.name.toUpperCase().includes(this.items.controls[itemIndex].get('name').value.toUpperCase()));
        if(product){
          this.items.controls[itemIndex].get('name').patchValue(product.name);
        }
        else{
          this.items.controls[itemIndex].get('name').patchValue('');
        }
    }
  }

  productsDropdownOnMouseEnter(el: HTMLElement){
    el.classList.add('mouse-enter')
  }

  productsDropdownOnMouseLeave(el: HTMLElement){
    el.classList.remove('mouse-enter')
  }

  onOutsideProductDropdownClick(el:HTMLElement,itemIndex:number){
    if(el.classList.contains('d-block')){
      this.adjustTheNameToTheSelectedProduct(itemIndex);
    }
    el.classList.remove('d-block')
  }
  //endregion

}
