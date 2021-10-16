import {Component, OnDestroy, OnInit} from "@angular/core";
import {AuthService} from "../../services/auth.service";
import {Subscription} from "rxjs";

@Component({
    selector: 'header-insight',
    templateUrl: 'header-insight.component.html',
    styleUrls: []
})
export class HeaderInsightComponent implements OnInit, OnDestroy {
    title = "ITechQuiz"

    constructor(public authService: AuthService) {
    }

    ngOnInit() {

    }

    ngOnDestroy() {
    }
}