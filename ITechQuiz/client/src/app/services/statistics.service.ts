import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {Survey} from "../models/survey";
import {QuestionStatistics} from "../models/question-statistics";

@Injectable()
export class StatisticsService {

    private url: string = "api/Statistics"
    constructor(private http: HttpClient) {
    }
    
    getStatistics(surveyId: string){

        return this.http.get<QuestionStatistics[]>(this.url, {
            params: {surveyId: surveyId}
        })
    }
}
