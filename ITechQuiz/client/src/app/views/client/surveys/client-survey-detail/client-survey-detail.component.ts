import {Component, OnDestroy, OnInit} from '@angular/core';
import {Survey} from "../../../../models/survey";
import {Subscription} from "rxjs";
import {SurveysService} from "../../../../services/surveys.service";
import {ActivatedRoute, Data, Router} from "@angular/router";
import {Option} from "../../../../models/option";
import {Question} from "../../../../models/question";
import {SurveyTypes} from "../../../../utils/survey-types";

@Component({
    selector: 'client-survey-detail',
    templateUrl: "client-survey-detail.component.html"
})
export class ClientSurveyDetailComponent implements OnInit, OnDestroy {
    id?: string = undefined
    survey: Survey = new Survey()
    subscription?: Subscription
    data: Data | undefined
    types: { [name: string]: string } = SurveyTypes

    public constructor(private surveysService: SurveysService,
                       private activatedRoute: ActivatedRoute, private router: Router) {
        this.id = activatedRoute.snapshot.params["id"]
    }

    ngOnInit() {
        this.activatedRoute.data.subscribe(data => {
            this.data = data
        })
        if (this.id) {
            this.loadSurvey(this.id)
        } else {
            this.survey = new Survey()
            this.survey.type = this.data?.type
        }
    }

    ngOnDestroy() {
        this.subscription?.unsubscribe()
    }

    loadSurvey(id: string) {
        this.subscription = this.surveysService.getSurvey(id)
            .subscribe((data: Survey) => this.survey = data)
    }

    saveSurvey() {
        if (this.id) {
            this.surveysService.updateSurvey(this.survey)
                .subscribe((data) => {
                        console.log(data)
                        if (this.data?.type == "ForStatistics") {
                            this.router.navigateByUrl("/client/statistic-surveys")
                        } else if (this.data?.type == "ForQuiz") {
                            this.router.navigateByUrl("/client/quiz-surveys")
                        } else this.router.navigateByUrl("/")
                    }
                )
        } else {
            this.surveysService.addSurvey(this.survey)
                .subscribe((data) => {
                        console.log(data)
                        if (this.data?.type == "ForStatistics") {
                            this.router.navigateByUrl("/client/statistic-surveys")
                        } else if (this.data?.type == "ForQuiz") {
                            this.router.navigateByUrl("/client/quiz-surveys")
                        } else this.router.navigateByUrl("/")
                    }
                )
        }

    }

    addQuestion() {
        let question = new Question()
        question.surveyId = this.id
        question.maxSelected = 0
        this.survey.questions?.push(question)
    }

    deleteQuestion(question: Question) {
        if (question) {
            const index = this.survey.questions.indexOf(question)
            this.survey.questions.splice(index, 1)
        }
    }

    addOption(question: Question) {
        if (question) {
            let option = new Option()
            option.questionId = question.id
            question.options.push(option)
            if (question.maxSelected != undefined) {
                question.maxSelected++
            }
        }
    }

    deleteOption(question: Question, option: Option) {
        if (option && question) {
            const index = question.options.indexOf(option)
            question.options.splice(index, 1)
            if (question.maxSelected) {
                question.maxSelected--
            }
        }
    }

    deleteSurvey() {
        if (this.id) {
            this.surveysService.deleteSurvey(this.id)
                .subscribe((data) => {
                    console.log(data)

                    if (this.data?.type == "ForStatistics") {
                        this.router.navigateByUrl("/client/statistic-surveys")
                    } else if (this.data?.type == "ForQuiz") {
                        this.router.navigateByUrl("/client/quiz-surveys")
                    } else this.router.navigateByUrl("/")
                })
        }
    }
}
