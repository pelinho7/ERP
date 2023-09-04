export class Pagination{
    page:number=1;
    pageSize:number=10;
    totalItems:number=null
    totalPages:number=null

    constructor(){}

    castJsonToClass(json:Pagination){
        if(json.pageSize){
          this.pageSize = json.pageSize;
        }
        else{
          this.pageSize = 10;
        }
        if(json.page){
          this.page = json.page;
        }
        if(json.totalPages){
          this.totalPages = json.totalPages;
        }
        if(json.totalItems){
          this.totalItems = json.totalItems;
        }
      }
}