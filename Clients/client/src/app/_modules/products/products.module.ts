import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductsRoutingModule } from './products-routing.module';
import { ProductComponent } from './product/product.component';
import { ProductsListComponent } from './products-list/products-list.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormControlsModule } from '../form-controls/form-controls.module';
import { NavbarsModule } from "../navbars/navbars.module";
import { AppModule } from "../../app.module";
import { ContractorsModule } from '../contractors/contractors.module';
import { StaticDataModule } from '../static-data/static-data.module';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ProductsListItemComponent } from './products-list-item/products-list-item.component';
import { NgxSpinnerModule } from 'ngx-spinner';


@NgModule({
    declarations: [
        ProductComponent,
        ProductsListComponent,
        ProductsListItemComponent
    ],
    exports: [
        ProductComponent,
        ProductsListComponent
    ],
    imports: [
        CommonModule,
        ProductsRoutingModule,
        ReactiveFormsModule,
        FormControlsModule,
        NavbarsModule,
        StaticDataModule,
        PaginationModule,
        FormsModule,
        NgxSpinnerModule,
    ]
})
export class ProductsModule { }
