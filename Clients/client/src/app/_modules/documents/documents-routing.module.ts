import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DocumentsListComponent } from './documents-list/documents-list.component';
import { DocumentComponent } from './document/document.component';

export const routes: Routes = [
  {path:'',component:DocumentsListComponent},
  {path:':id',component:DocumentComponent},
  {path:'add',component:DocumentComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DocumentsRoutingModule { }
