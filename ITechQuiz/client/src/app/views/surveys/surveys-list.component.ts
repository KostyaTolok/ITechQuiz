import {Component, OnDestroy, OnInit} from '@angular/core';
import {SurveysService} from "../../services/surveys.service";
import {Survey} from "../../models/survey";
import {Subscription} from "rxjs";
import {ActivatedRoute, Data} from "@angular/router";

@Component({
    selector: 'survey-list',
    templateUrl: 'surveys-list.component.html',
    styles: []
})
export class SurveysListComponent implements OnInit, OnDestroy {
    surveys: Survey[] = []
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