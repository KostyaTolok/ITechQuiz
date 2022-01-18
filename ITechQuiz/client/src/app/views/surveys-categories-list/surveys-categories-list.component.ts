import {Component, EventEmitter, Input, OnDestroy, OnInit, Output} from '@angular/core';
import {Survey} from "../../models/survey";
import {Category} from "../../models/category";
import {SurveysService} from "../../services/surveys.service";
import {CategoriesService} from "../../services/categories.service";
import {LocalStorageService} from "../../services/local-storage.service";
import {Subscription} from "rxjs";
import {ActivatedRoute, Data} from "@angular/router";

@Component({
    selector: 'surveys-categories-list',
    templateUrl: 'surveys-categories-list.component.html',
    styles: []
})
export class SurveysCategoriesListComponent implements OnInit, OnDestroy {

    @Input() isStatistics = false
    data: Data | undefined
    surveys: Survey[] | undefined
    categories: Category[] | undefined
    subscription?: Subscription
    categoryIds: string[] = []
    selectedCategories: boolean[] = []
    categoriesKey: string = ""
    idsKey: string = ""

    constructor(private surveysService: SurveysService,
                private categoriesService: CategoriesService,
                private localStorageService: LocalStorageService,
                private activatedRoute: ActivatedRoute) {
    }

    ngOnInit(): void {
        if (this.isStatistics) {
            this.categoriesKey = "statisticsSelectedCategories"
            this.idsKey = "statisticsCategoryIds"
        } else {
            this.categoriesKey = "selectedCategories"
            this.idsKey = "categoryIds"
        }
        this.loadCategories()
    }

    ngOnDestroy() {
        this.subscription?.unsubscribe()
    }

    loadSurveys() {
        if (this.isStatistics) {
            this.subscription = this.surveysService
                .getSurveys(true,false, this.data?.type, this.categoryIds, true)
                .subscribe((data: Survey[]) =>{ 
                    this.surveys = data
                })
            
        } else {
            
            this.activatedRoute.data.subscribe(data => {
                this.data = data
            })
            
            this.subscription = this.surveysService.getSurveys(false,false, this.data?.type, this.categoryIds)
                .subscribe((data: Survey[]) => {
                    this.surveys = data
                })
            
        }
    }

    loadCategories() {
        this.categoriesService.getCategories()
            .subscribe((data: Category[]) => {
                this.categories = data

                this.selectedCategories = JSON.parse(this.localStorageService.get(this.categoriesKey, "[]"))
                this.categoryIds = JSON.parse(this.localStorageService.get(this.idsKey, "[]"))
                this.loadSurveys()
            })
    }

    addCategoryId(id: string | undefined) {
        if (id) {
            if (!this.categoryIds.includes(id)) {
                this.categoryIds.push(id)
            } else {
                const index = this.categoryIds.indexOf(id)
                this.categoryIds.splice(index, 1)
            }
            this.localStorageService.set(this.categoriesKey, JSON.stringify(this.selectedCategories))
            this.localStorageService.set(this.idsKey, JSON.stringify(this.categoryIds))
            this.loadSurveys()
        }
    }


}
