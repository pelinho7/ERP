import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-choose-company',
  templateUrl: './choose-company.component.html',
  styleUrls: ['./choose-company.component.css']
})
export class ChooseCompanyComponent implements OnInit {

  constructor(public bsModalRef: BsModalRef) { }

  ngOnInit(): void {
  }

}
