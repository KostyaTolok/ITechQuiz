import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {JwtTokenService} from "./jwt-token.service";
import {AssignRequest} from "../models/assign-request";

@Injectable()
export class AssignRequestService {

    private url = 'api/admin/AssignRequests'

    constructor(private http: HttpClient,
                private jwtTokenService: JwtTokenService) {
    }

    getAssignRequests(includeRejected: boolean, sorted: boolean) {

        return this.http.get<AssignRequest[]>(this.url, {
            params: {includeRejected: includeRejected, sorted: sorted}
        })
    }

    acceptAssignRequest(id: string) {
        return this.http.post(`${this.url}/accept/${id}`,
            {},
            {
                responseType: "text"
            })
    }

    rejectAssignRequest(id: string) {
        return this.http.post(`${this.url}/reject/${id}`,
            {},
            {
                responseType: "text"
            })
    }

    deleteAssignRequest(id: string) {
        return this.http.delete(`${this.url}/${id}`,
            {
                responseType: "text"
            })
    }

    makeAssignRequest(userId: string, role: string) {
        return this.http.post(`${this.url}`,
            {
                userId: userId,
                role: role
            },
            {
                responseType: "text"
            })
    }
}
