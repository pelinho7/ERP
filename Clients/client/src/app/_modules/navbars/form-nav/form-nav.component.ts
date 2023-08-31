import { Component, ElementRef, Input, OnInit } from '@angular/core';
import { ResizeWindowWatcherService } from '../services/resize-window-watcher.service';

@Component({
  selector: 'app-form-nav',
  templateUrl: './form-nav.component.html',
  styleUrls: ['./form-nav.component.css']
})
export class FormNavComponent implements OnInit {

  @Input() navItems:string[]=[];
  constructor(public resizeWindowWatcherService:ResizeWindowWatcherService
    ,private elem: ElementRef) { }

  ngOnInit(): void {
  }

  scrollToSection(sectionId:number){
    let sections = document.querySelectorAll('.form-section');
    sections[sectionId].scrollIntoView();
  }
}
