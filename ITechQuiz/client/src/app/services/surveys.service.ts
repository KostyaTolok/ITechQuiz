import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Survey} from "../models/survey";
import {JwtTokenService} from "./jwt-token.service";

@Injectable()
export class SurveysService {

    private url = '/api/surveys'

    constructor(private http: HttpClient) {
    }

    getSurveys(type: string) {
        if (type != null) {
            return this.http.get<Survey[]>(this.url, {
                params: {surveyType: type}
            })
        } else {
            return this.http.get<Survey[]>(this.url)
        }
    }

    getSurvey(id: string) {
        return this.http.get<Survey>(`${this.url}/${id}`)
    }

    updateSurvey(survey: Survey) {
        return this.http.put(this.url, survey, {
            responseType: 'text'
        })
    }

    addSurvey(survey: Survey) {
        return this.http.post<Survey>(this.url, survey)
    }

    deleteSurvey(id: string) {
        return this.http.delete(`${this.url}/${id}`, {
            responseType: 'text'
        })
    }
}
