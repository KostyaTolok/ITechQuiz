import {Component, OnDestroy, OnInit} from '@angular/core';
import {Survey} from "../../../models/survey";
import {Category} from "../../../models/category";
import {Data} from "@angular/router";
import {Subscription} from "rxjs";
import {SurveysService} from "../../../services/surveys.service";
import {SignalrService} from "../../../services/signalr.service";
import {LocalStorageService} from "../../../services/local-storage.service";

@Component({
    selector: 'statistic-surveys-list',
    templateUrl: 'statistics-list.component.html'
})
export class StatisticsListComponent implements OnInit, OnDestroy {

    surveys: Survey[] | undefined
    subscription?: Subscription

    constructor(private surveysService: SurveysService,
                private signalrService: SignalrService,
                private localStorageService: LocalStorageService) {
    }

    ngOnInit(): void {
        this.loadSurveys()
        this.signalrService.notify = false;
        this.localStorageService.set("notify", "false")
    }

    loadSurveys() {
        this.subscription = this.surveysService.getSurveys(false, true)
            .subscribe((data: Survey[]) => {
                this.surveys = data
            })
    }

    ngOnDestroy() {
        this.subscription?.unsubscribe()
    }
}
