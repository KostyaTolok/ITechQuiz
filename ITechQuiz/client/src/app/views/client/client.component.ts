import {Component, OnInit} from '@angular/core';
import {SurveyLinks} from "../../utils/survey-types";
import {ClientValues} from "../../utils/client-values";

@Component({
    selector: 'client-view',
    templateUrl: 'client.component.html',
    styleUrls: []
})
export class ClientComponent implements OnInit {

    links: { [link: string]: string } = ClientValues

    constructor() {
    }

    ngOnInit(): void {
    }

}
