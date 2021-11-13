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
        const headers = new HttpHeaders().set("Authorization",
            `Bearer ${this.jwtTokenService.getJwtToken()}`)

        return this.http.get<AssignRequest[]>(this.url, {
            params: {includeRejected: includeRejected, sorted: sorted},
            headers: headers
        })
    }

    acceptAssignRequest(id: string) {
        const headers = new HttpHeaders().set("Authorization",
            `Bearer ${this.jwtTokenService.getJwtToken()}`)

        return this.http.post(`${this.url}/accept/${id}`,
            {},
            {
                responseType: "text",
                headers: headers
            })
    }

    rejectAssignRequest(id: string) {
        const headers = new HttpHeaders().set("Authorization",
            `Bearer ${this.jwtTokenService.getJwtToken()}`)

        return this.http.post(`${this.url}/reject/${id}`,
            {},
            {
                headers: headers,
                responseType: "text"
            })
    }

    deleteAssignRequest(id: string) {
        const headers = new HttpHeaders().set("Authorization",
            `Bearer ${this.jwtTokenService.getJwtToken()}`)

        return this.http.delete(`${this.url}/${id}`,
            {
                headers: headers,
                responseType: "text"
            })
    }

    makeAssignRequest(userId: string, role: string) {
        const headers = new HttpHeaders().set("Authorization",
            `Bearer ${this.jwtTokenService.getJwtToken()}`)

        return this.http.post(`${this.url}`,
            {
                userId: userId,
                role: role
            },
            {
                headers: headers,
                responseType: "text"
            })
    }
}
