import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {LoginModel} from "../models/login-model";
import {catchError, map} from "rxjs/operators";
import {throwError} from "rxjs";
import {RegisterModel} from "../models/register-model";
import {JwtTokenService} from "./jwt-token.service";
import {LocalStorageService} from "./local-storage.service";

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
                private jwtTokenService: JwtTokenService,
                private localStorageService: LocalStorageService) {
    }

    loginUser(model: LoginModel) {
        return this.http.post(this.url + '/login', model, {responseType: 'text'})
            .pipe(map(data => {
                        this.jwtTokenService.setJwtToken(data)
                        this.localStorageService.set("token", data)
                    }
                ),
                catchError(err => {
                    return throwError(err.error)
                })
            )
    }

    logoutUser() {
        this.localStorageService.remove("token")
        this.jwtTokenService.removeToken()
    }

    registerUser(model: RegisterModel) {
        return this.http.post(this.url + '/register', model, {responseType: "text"})
            .pipe(
                map(data => {
                        this.jwtTokenService.setJwtToken(data)
                        this.localStorageService.set("token", data)
                    }
                ),
                catchError(err => {
                    return throwError(err.error)
                })
            )
    }
}
