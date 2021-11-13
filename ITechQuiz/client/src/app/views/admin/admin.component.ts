import { Component, OnInit } from '@angular/core';
import {SurveyLinks} from "../../utils/survey-types";
import {AdminLinks} from "../../utils/admin-values";

@Component({
  selector: 'admin-view',
  templateUrl: 'admin.component.html',
})
export class AdminComponent implements OnInit {

  links: { [link: string]: string } = AdminLinks
  
  constructor() { }

  ngOnInit(): void {
  }

}
