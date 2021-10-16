import {Component, OnInit} from '@angular/core';
import {Survey} from "../../../../models/survey";
import {Subscription} from "rxjs";
import {SurveysService} from "../../../../services/surveys.service";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
    selector: 'admin-survey-detail',
    templateUrl: "admin-survey-detail.component.html",
    styleUrls: ["admin-survey-detail.component.css"]
})
export class AdminSurveyDetailComponent implements OnInit {
    id?: string = undefined
    survey: Survey = new Survey()
    subscription?: Subscription

    public constructor(private surveysService: SurveysService,
                       activeRoute: ActivatedRoute, private router: Router) {
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
    
    saveSurvey(){
        this.surveysService.updateSurvey(this.survey)
            .subscribe((data)=>{
                console.log(data)
                this.router.navigate([".."])
                }
            )
    }
}
