import {Component, OnDestroy, OnInit} from '@angular/core';
import {SurveysService} from "../../services/surveys.service";
import {Survey} from "../../models/survey";
import {ActivatedRoute} from "@angular/router";
import {Subscription} from "rxjs";

@Component({
    selector: 'survey-detail',
    templateUrl: 'survey-detail.component.html',
    styleUrls: ['survey-detail.component.css']
})
export class SurveyDetailComponent implements OnInit, OnDestroy {
    id?: string = undefined
    survey?: Survey = undefined
    subscription?: Subscription

    public constructor(private surveysService: SurveysService,
                       activeRoute: ActivatedRoute) {
        this.id = activeRoute.snapshot.params["id"]
    }

    ngOnInit() {
        if (this.id) {
            this.loadSurvey(this.id)
        }
    }

    ngOnDestroy() {
        this.subscription?.unsubscribe()
    }

    loadSurvey(id: string) {
        this.subscription = this.surveysService.getSurvey(id)
            .subscribe((data: Survey) => this.survey = data)
    }

}