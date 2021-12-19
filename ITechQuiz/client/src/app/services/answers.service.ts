import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Answer} from "../models/answer";

@Injectable({
    providedIn: 'root'
})
export class AnswersService {

    private url = 'api/Answers'

    constructor(private http: HttpClient) {
    }

    addAnswers(answers: Answer[], isAnonymous: boolean) {
        return this.http.post(`${this.url}`, answers, {
            params: {isAnonymous: isAnonymous},
            responseType: "text",
        })
    }

    getAnswers(id: string | undefined = undefined) {
        if (id)
            return this.http.get<Answer[]>(`${this.url}`, {
                params: {surveyId: id}
            })
        else
            return this.http.get<Answer[]>(`${this.url}`)
    }
}
