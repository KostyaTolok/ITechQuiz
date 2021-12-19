import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from "@angular/common/http";
import {Survey} from "../models/survey";
import {JwtTokenService} from "./jwt-token.service";

@Injectable()
export class SurveysService {

    private url = '/api/surveys'

    constructor(private http: HttpClient) {
    }

    getSurveys(personal: boolean = false, client: boolean = false, type: string | null = null, categoryIds: string[] = []) {
        let params = new HttpParams()
        for (const id of categoryIds) {
            params = params.append('categoryIds', id)
        }
        params = params.set("personal", personal)
        params = params.set("client", client)
        
        if (type != null) {
            params = params.set('surveyType', type)
        }

        return this.http.get<Survey[]>(this.url, {
            params: params
        })
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
