import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class LoadingScreenService {
    busyRequestCount=0;
    constructor(private spinnerService:NgxSpinnerService) { }
  
    show(name:string="main-spinner"){
      this.busyRequestCount++;
      this.spinnerService.show(name,{
        type:'line-scale-party',
        bdColor:'rgba(255,255,255,0)',
        color:'#333333'
      });
    }
  
    hide(name:string="main-spinner"){
      this.busyRequestCount--;
      this.spinnerService.hide(name);
    }
  }
