import { Injectable } from '@angular/core';
import { NgxSpinner, NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusyService {
  busyRequestCount=0;
  constructor(private spinnerService:NgxSpinnerService) { }

  // busy(name:string){
  //   this.busyRequestCount++;
  //   this.spinnerService.show(name,{
  //     type:'line-scale-party',
  //     bdColor:'rgba(255,255,255,0)',
  //     color:'#333333'
  //   });
  // }

  idle(name:string){
    this.busyRequestCount--;
    //console.log(name+' ' +this.busyRequestCount)
    //this.busyRequestCount=0;
    this.spinnerService.hide(name);
    // if(this.busyRequestCount<=0){
    //   this.busyRequestCount=0;
    //   this.spinnerService.hide(name);
    // }
  }
}
