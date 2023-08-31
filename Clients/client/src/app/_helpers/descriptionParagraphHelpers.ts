export function removeAngularTags(html:string){
  //get component html and split it by space
  var splittedHtml:string[]=html.split(' ');
  //remove angular tags
  splittedHtml=splittedHtml.filter(z=>!z.includes('_ngcontent'));
  //rejoin html
  return splittedHtml.join(' ');
}