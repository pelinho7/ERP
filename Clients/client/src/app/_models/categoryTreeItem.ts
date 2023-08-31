import { Category } from "./category"

export class CategoryTreeItem {
    // public int Id { get; set; }
    // public string Code { get; set; }
    // public string Label { get; set; }
    // public int? ParentCategoryId {get;set;}
    // public bool Deleted { get; set; }
    // public bool Visible { get; set; }=true;
    // public int TreeLevel { get; set; }
    // public List<CategoryTreeItemDto> Subcategories { get; set; }=new List<CategoryTreeItemDto>();


    id:number=0
    parentCategoryId:number
    code:string=''
    label:string=''
    visible:boolean
    treeLevel:number
    subCategories:CategoryTreeItem[]=[]
   
    constructor(){}

    categoryToCategoryTreeItem(category:Category,treeLevel:number){
        this.id=category.id;
        this.parentCategoryId=category.parentCategoryId;
        this.code=category.code;
        this.label=category.label;
        this.treeLevel=treeLevel;
        this.visible=true;
    }
}