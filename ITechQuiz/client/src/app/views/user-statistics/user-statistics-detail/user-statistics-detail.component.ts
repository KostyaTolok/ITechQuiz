import {Component, OnDestroy, OnInit} from '@angular/core';
import {Answer} from "../../../models/answer";
import {Subscription} from "rxjs";
import {AnswersService} from "../../../services/answers.service";
import {ActivatedRoute} from "@angular/router";
import {Survey} from "../../../models/survey";
import {SurveysService} from "../../../services/surveys.service";

@Component({
    selector: 'user-statistics-detail',
    templateUrl: 'user-statistics-detail.component.html',
    styleUrls: ['user-statistics-detail.component.css']
})
export class UserStatisticsDetailComponent implements OnInit, OnDestroy {

    id?: string
    answers: Answer[] | undefined
    subscription?: Subscription
    survey: Survey | undefined
    correctAmounts: number[] = []
    correctAmount = 0
    incorrectAmount = 0
    currentDate: string = ""
    currentIndex = 0

    constructor(public answersService: AnswersService,
                private activeRoute: ActivatedRoute,
                public surveysService: SurveysService) {
        this.id = activeRoute.snapshot.params["id"]
    }

    ngOnInit(): void {
        this.loadAnswers()
        this.loadSurvey()
    }

    ngOnDestroy() {
        this.subscription?.unsubscribe()
    }

    loadAnswers() {
        if (this.id)
            this.subscription = this.answersService.getAnswers(this.id)
                .subscribe((data: Answer[]) => {
                    this.answers = data

                    for (const answer of data) {
                        let correctAmount = 0
                        for (const option of answer.selectedOptions) {
                            if (option.isCorrect)
                                correctAmount++
                        }
                        this.correctAmounts.push(correctAmount)
                    }
                })
    }

    loadSurvey() {
        if (this.id)
            this.subscription = this.surveysService.getSurvey(this.id)
                .subscribe((data: Survey) => this.survey = data)
    }

    isSameDate(date: string | undefined) {
        if (date) {
            const result = this.currentDate == date
            this.currentDate = date

            if (!result) {
                this.currentIndex = 0
                this.correctAmount = 0
                this.incorrectAmount = 0
            } else this.currentIndex++

            return result
        }

        return false
    }

    isSummary(answer: Answer) {
        if (this.answers) {
            const ind = this.answers.indexOf(answer)
            this.correctAmount += this.correctAmounts[ind]
            this.incorrectAmount += answer.selectedOptions.length - this.correctAmounts[ind]
            if ((this.answers[ind + 1] && answer.createdDate != this.answers[ind + 1].createdDate)
                || ind == this.answers.length - 1) {
                return true
            }
        }
        return false
    }

    getIncorrectAmount(answer: Answer) {
        return answer?.selectedOptions?.length - this.correctAmount
    }
}
