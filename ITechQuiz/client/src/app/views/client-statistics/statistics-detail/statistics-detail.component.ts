import {Component, OnDestroy, OnInit} from '@angular/core';
import {Answer} from "../../../models/answer";
import {AnswersService} from "../../../services/answers.service";
import {Subscription} from "rxjs";
import {ActivatedRoute} from "@angular/router";
import {StatisticsService} from "../../../services/statistics.service";
import {QuestionStatistics} from "../../../models/question-statistics";
import {Survey} from "../../../models/survey";
import {SurveysService} from "../../../services/surveys.service";

@Component({
    selector: 'statistics-survey-detail',
    templateUrl: 'statistics-detail.component.html',
    styles: []
})
export class StatisticsDetailComponent implements OnInit, OnDestroy {

    statistics: QuestionStatistics[] = []
    id: string | undefined
    subscription?: Subscription
    survey? : Survey

    constructor(private statisticsService: StatisticsService,
                private activeRoute: ActivatedRoute,
                private surveysService: SurveysService) {
        this.id = activeRoute.snapshot.params["id"]
    }

    ngOnInit(): void {
        this.loadStatistics()
        this.loadSurvey()
    }

    ngOnDestroy() {
        this.subscription?.unsubscribe()
    }

    loadStatistics() {
        if (this.id)
            this.subscription = this.statisticsService.getStatistics(this.id)
                .subscribe((data: QuestionStatistics[]) => {
                    this.statistics = data

                    for (const question of data) {
                        let sum = 0
                        question.optionsStatistics.forEach((option) => {
                            sum += option.answersAmount!
                        })
                        
                        question.optionsStatistics.forEach((option) => {
                            if (sum!= 0) 
                                option.answersPercent = (option.answersAmount! / sum) * 100
                            else option.answersPercent = 0
                        })
                    }
                })
    }

    loadSurvey(){
        if (this.id)
            this.subscription = this.surveysService.getSurvey(this.id)
                .subscribe((data: Survey) => this.survey = data)
    }
}
