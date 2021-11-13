import {Component, OnInit} from '@angular/core';
import {SurveyLinks} from "../../utils/survey-types";

@Component({
    selector: 'client-view',
    templateUrl: 'client.component.html',
    styleUrls: []
})
export class ClientComponent implements OnInit {

    links: { [link: string]: string } = SurveyLinks

    constructor() {
    }

    ngOnInit(): void {
    }

}
