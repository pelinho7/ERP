import { HomeComponent } from '../../home/home.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdminPanelComponent } from './admin-panel.component';
import { AppRoutingModule } from 'src/app/app-routing.module';

export const routes: Routes = [
  {
    path: '',
    component: AdminPanelComponent,
    children: [
      { path: '', pathMatch: 'full', redirectTo: 'home' },
      { path: 'home', component: HomeComponent },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})


export class AdminPanelRoutingModule { }




// import { NgModule } from '@angular/core';
// import { Routes, RouterModule } from '@angular/router';
// import { AdminPanelComponent } from './admin-panel.component';

// const routes: Routes = [
//   {
//     path: '',
//     component: AdminPanelComponent
//   }
// ];

// @NgModule({
//   imports: [RouterModule.forChild(routes)],
//   exports: [RouterModule]
// })
// export class AdminPanelRoutingModule { }