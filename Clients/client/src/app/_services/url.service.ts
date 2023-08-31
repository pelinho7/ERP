import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UrlService {

  prevoiusUrl:string='';
  constructor() { }

  setPreviousUrl(url:string){
    this.prevoiusUrl=url;
    console.log('qq '+this.prevoiusUrl)
  }

  getPreviousUrl(){
    var url=this.prevoiusUrl;
    this.prevoiusUrl='';
    return url;
  }
}
