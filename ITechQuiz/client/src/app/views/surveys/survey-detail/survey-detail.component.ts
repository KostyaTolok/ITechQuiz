import {Component, OnDestroy, OnInit} from '@angular/core';
import {SurveysService} from "../../../services/surveys.service";
import {Survey} from "../../../models/survey";
import {ActivatedRoute, Router} from "@angular/router";
import {Subscription} from "rxjs";
import {Answer} from "../../../models/answer";
import {Option} from "../../../models/option";
import {AnswersService} from "../../../services/answers.service";
import {LocalStorageService} from "../../../services/local-storage.service";
import {MatTooltipModule} from '@angular/material/tooltip';
import {DataRowOutlet} from "@angular/cdk/table";
import {AuthService} from "../../../services/auth.service";
import {SignalrService} from "../../../services/signalr.service";

@Component({
    selector: 'survey-detail',
    templateUrl: 'survey-detail.component.html'
})
export class SurveyDetailComponent implements OnInit, OnDestroy {
    id?: string = undefined
    survey?: Survey = undefined
    subscription?: Subscription
    answers: Answer[] = []
    isAnonymous = false
    errorMessage = ""
    previousAnswers: Answer[] = []

    public constructor(private surveysService: SurveysService,
                       private answersService: AnswersService,
                       private activeRoute: ActivatedRoute,
                       private localStorageService: LocalStorageService,
                       public authService: AuthService,
                       private router: Router,
                       private signalrService: SignalrService) {
        this.id = activeRoute.snapshot.params["id"]
    }

    ngOnInit() {
        if (this.id) {
            this.loadSurvey(this.id)
        }
        if (!this.authService.loginRequired)
            this.isAnonymous = 
                JSON.parse(this.localStorageService.get(`${this.id}-isAnonymous`, "false"))
        else
            this.isAnonymous = true
    }

    ngOnDestroy() {
        this.subscription?.unsubscribe()
    }

    loadSurvey(id: string) {
        this.subscription = this.surveysService.getSurvey(id)
            .subscribe((data: Survey) => {
                this.survey = data

                for (const q of this.survey.questions) {
                    let answer = new Answer()
                    answer.questionId = q.id

                    this.answers.push(answer)
                }
                if (!this.authService.loginRequired)
                    this.loadPreviousAnswers()
            })
    }

    loadPreviousAnswers() {
        if (this.id)
            this.answersService.getAnswers(this.id)
                .subscribe((data: Answer[]) => {
                    this.previousAnswers = data
                })
    }

    addOrRemoveSelectedOption(questionIndex: number, optionIndex: number, option: Option) {

        if (this.answers[questionIndex].selectedOptions.includes(option)) {
            const ind = this.answers[questionIndex].selectedOptions.indexOf(option)
            this.answers[questionIndex].selectedOptions.splice(ind, 1)
        } else {
            this.answers[questionIndex].selectedOptions.push(option)
        }

    }

    answerSurvey() {
        if (this.id) {
            if (this.previousAnswers.length != 0 && !this.isAnonymous && !this.survey!.isMultipleAnswersAllowed) {
                this.errorMessage = "Нельзя проходить этот опрос неанонимно несколько раз"
                return
            }
        }
        if (this.survey)
            for (let i = 0; i < this.survey.questions.length; i++) {
                if (this.survey.questions[i].required && this.answers[i].selectedOptions.length == 0) {
                    this.errorMessage = "Ответьте на все обязательные опросы"
                    return
                }
                // @ts-ignore
                if (this.answers[i].selectedOptions.length > this.survey.questions[i].maxSelected) {
                    this.errorMessage = "Превышено максимальное количество выбранных вариантов ответа"
                    return
                }
            }

        if (this.isAnonymous)
            this.answers.forEach(a => a.isAnonymous = true)

        if (this.survey?.isAnonymousAllowed)
            this.answers.forEach(a => a.isAnonymousAllowed = true)

        if (this.survey?.id) {
            this.signalrService.invokeNotify(this.survey.id)
        }

        this.answersService.addAnswers(this.answers, this.isAnonymous, this.survey?.id)
            .subscribe(() => {
                this.localStorageService.remove(`${this.id}-isAnonymous`)
                this.router.navigateByUrl("/")
            })
    }

    setIsAnonymous(value: boolean) {
        this.localStorageService.set(`${this.id}-isAnonymous`, JSON.stringify(value))
        this.isAnonymous = value
    }

    disableTooltip(questionIndex: number, optionIndex: number, option: Option) {
        return !this.answers[questionIndex].selectedOptions.includes(option);
    }
}