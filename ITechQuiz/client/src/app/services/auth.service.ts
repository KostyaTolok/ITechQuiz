import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {LoginModel} from "../models/login-model";
import {catchError, map} from "rxjs/operators";
import {throwError} from "rxjs";
import {RegisterModel} from "../models/register-model";
import {JwtTokenService} from "./jwt-token.service";
import {ChangePasswordModel} from "../models/change-password-model";

@Injectable()
export class AuthService {

    private url = 'api/auth'

    public get loginRequired(): boolean {
        return this.jwtTokenService.isTokenExpired()
    }

    public get isAdmin(): boolean {
        const roles = this.jwtTokenService.getRoles()
        if (roles) {
            return roles.includes("admin")
        } else return false
    }

    public get isClient(): boolean {
        const roles = this.jwtTokenService.getRoles()
        if (roles) {
            return roles.includes("client")
        } else return false
    }

    constructor(private http: HttpClient,
                private jwtTokenService: JwtTokenService) {
    }

    loginUser(model: LoginModel) {

        return this.http.post(`${this.url}/login`, model, {
            responseType: 'text'
        })
            .pipe(map(data => {
                        this.jwtTokenService.setJwtToken(data)
                    }
                ),
                catchError(err => {
                    return throwError(err.error)
                })
            )
    }

    logoutUser() {
        this.jwtTokenService.removeToken()
    }

    registerUser(model: RegisterModel) {
        return this.http.post(`${this.url}/register`, model, {
            responseType: "text"
        })
            .pipe(
                map(data => {
                        this.jwtTokenService.setJwtToken(data)
                    }
                ),
                catchError(err => {
                    return throwError(err.error)
                })
            )
    }

    changePassword(model: ChangePasswordModel) {

        return this.http.post(`${this.url}/change-password`, model, {
            responseType: "text"
        }).pipe(catchError(err => {
            return throwError(err.error)
        }))
    }
}
