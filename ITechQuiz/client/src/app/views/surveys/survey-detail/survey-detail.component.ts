import {Component, OnDestroy, OnInit} from '@angular/core';
import {SurveysService} from "../../services/surveys.service";
import {Survey} from "../../models/survey";
import {ActivatedRoute, Router} from "@angular/router";
import {Subscription} from "rxjs";
import {Answer} from "../../models/answer";
import {Option} from "../../models/option";
import {AnswersService} from "../../services/answers.service";
import {LocalStorageService} from "../../services/local-storage.service";

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
    
    public constructor(private surveysService: SurveysService,
                       private answersService: AnswersService,
                       private activeRoute: ActivatedRoute,
                       private localStorageService: LocalStorageService,
                       private router: Router) {
        this.id = activeRoute.snapshot.params["id"]
    }

    ngOnInit() {
        if (this.id) {
            this.loadSurvey(this.id)
        }
        
        this.isAnonymous = JSON.parse(this.localStorageService.get("isAnonymous", "false"))
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
            })
    }

    addOrRemoveSelectedOption(index: number, option: Option) {
        if (this.answers[index].selectedOptions.includes(option)) {
            const optionIndex = this.answers[index].selectedOptions.indexOf(option)
            this.answers[index].selectedOptions.splice(optionIndex, 1)
        }
        else
            this.answers[index].selectedOptions.push(option)
    }

    answerSurvey() {
        this.answersService.addAnswers(this.answers, this.isAnonymous)
            .subscribe((data) => {
                console.log(data)
                
                this.router.navigateByUrl("/")
            })
    }

    setIsAnonymous(value: boolean) {
        this.localStorageService.set("isAnonymous", JSON.stringify(value))
        this.isAnonymous = value
    }
}