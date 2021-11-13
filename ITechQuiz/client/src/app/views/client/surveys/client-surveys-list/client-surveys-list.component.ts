import {Component, OnDestroy, OnInit} from "@angular/core";
import {ActivatedRoute, Data} from "@angular/router";
import {Subscription} from "rxjs";
import {SurveysService} from "../../../../services/surveys.service";
import {Survey} from "../../../../models/survey";
import {CookieService} from "ngx-cookie-service";

@Component({
    selector: "client-surveys-list",
    templateUrl: "client-surveys-list.component.html"
})
export class ClientSurveysListComponent implements OnInit, OnDestroy {
    surveys: Survey[] | undefined
    data: Data | undefined

    subscription?: Subscription

    public constructor(private surveysService: SurveysService,
                       private activatedRoute: ActivatedRoute) {
    }

    ngOnInit() {
        this.activatedRoute.data.subscribe(data => {
            this.data = data
        })
        this.loadSurveys()
    }

    ngOnDestroy() {
        this.subscription?.unsubscribe()
    }

    loadSurveys() {
        this.subscription = this.surveysService.getSurveys(this.data?.type)
            .subscribe((data: Survey[]) => this.surveys = data)
    }
}