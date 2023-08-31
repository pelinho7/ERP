export class navbarItem {
    url:string=''
    text:string=''
    subItems:navbarItem[]=[]
   
    constructor(text: string,url: string) {
        this.text = text;
        this.url = url;
      }
}