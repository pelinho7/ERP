export class Pagination{
    page:number=1;
    itemsPerPage:number=10;
    totalItems:number=null
    totalPages:number=null

    constructor(){}

    castJsonToClass(json:Pagination){
        if(json.itemsPerPage){
          this.itemsPerPage = json.itemsPerPage;
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