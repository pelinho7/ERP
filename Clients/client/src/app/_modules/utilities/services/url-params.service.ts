import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UrlParamsService {

  constructor() { }

  urlParams2Object(urlParams:string){
    var object =JSON.parse('{"' + decodeURI(urlParams.replace(/&/g, "\",\"").replace(/=/g,"\":\"")) + '"}')
    return object;
  }
}
