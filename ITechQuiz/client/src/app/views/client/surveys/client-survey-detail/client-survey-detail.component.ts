import {Component, OnDestroy, OnInit} from '@angular/core';
import {Survey} from "../../../../models/survey";
import {Subscription} from "rxjs";
import {SurveysService} from "../../../../services/surveys.service";
import {ActivatedRoute, Data, Router} from "@angular/router";
import {Option} from "../../../../models/option";
import {Question} from "../../../../models/question";
import {SurveyTypes} from "../../../../utils/survey-types";
import {CategoriesService} from "../../../../services/categories.service";
import {Category} from "../../../../models/category";

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
    categories: Category[] = []
    category: Category| undefined
    errorMessage = ""

    public constructor(private surveysService: SurveysService,
                       private activatedRoute: ActivatedRoute,
                       private router: Router,
                       private categoriesService: CategoriesService) {
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
        this.categoriesService.getCategories()
            .subscribe((data: Category[]) => this.categories = data)
    }

    ngOnDestroy() {
        this.subscription?.unsubscribe()
    }

    loadSurvey(id: string) {
        this.subscription = this.surveysService.getSurvey(id)
            .subscribe((data: Survey) => this.survey = data)
    }

    saveSurvey() {
        if (this.id && this.survey) {
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
        } else if (this.survey) {
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
        if (this.survey)
            this.survey.questions?.push(question)
    }

    deleteQuestion(question: Question) {
        if (question && this.survey) {
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
    
    addCategory(){
        if (this.category){
            let addCategory = true
            for (const category of this.survey.categories) {
                if (category.id == this.category.id){
                    addCategory =false
                }
            }
            if (addCategory) {
                this.survey.categories.push(this.category)
                this.errorMessage = ""
            }
            else
                this.errorMessage = "Данная категория уже добавлена"
        }
    }
    
    removeCategory(ind: number){
        this.survey.categories.splice(ind, 1)
    }
}
