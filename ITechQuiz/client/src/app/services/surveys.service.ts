import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Survey} from "../models/survey";
import {AuthService} from "./auth.service";
import {JwtTokenService} from "./jwt-token.service";

@Injectable()
export class SurveysService {

    private url = '/api/surveys'

    constructor(private http: HttpClient, private authService: AuthService,
                private jwtTokenService: JwtTokenService) {
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
        return this.http.get<Survey>(this.url + '/' + id)
    }
    
    updateSurvey(survey: Survey){
        const headers = new HttpHeaders().set("Authorization",
            `Bearer ${this.jwtTokenService.getJwtToken()}`)
        return this.http.put(this.url, survey, {
            headers: headers,
            responseType: 'text'
        })
    }
}
