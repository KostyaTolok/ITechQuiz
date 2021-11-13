import {Inject, Injectable} from '@angular/core';
import jwtDecode from "jwt-decode";
import {LocalStorageService} from "./local-storage.service";
import {CookieService} from 'ngx-cookie-service';

@Injectable()
export class JwtTokenService {

    private jwtToken: string = ""
    private decodedToken: { [key: string]: string } = {}

    constructor(private localStorage: LocalStorageService,
                @Inject(CookieService) private cookieService: CookieService) {
    }

    setJwtTokenFromStorage() {
        const token = this.cookieService.get("token")
        if (token) {
            this.jwtToken = token
            this.decodedToken = jwtDecode(token)
        }
    }

    setJwtToken(token: string | undefined) {
        if (token) {

            this.jwtToken = token
            this.decodedToken = jwtDecode(token)
            
            const expirationDate = new Date(0)
            expirationDate.setUTCSeconds(this.getExpiryTime())
            
            this.cookieService.set("token", token,
                expirationDate)
        }
    }

    removeToken() {
        this.cookieService.delete("token")
        this.jwtToken = ""
        this.decodedToken = {}
    }

    getJwtToken() {
        return this.jwtToken
    }

    getEmail() {
        return this.decodedToken['sub']
    }

    getRoles() {
        return this.decodedToken['roles']
    }

    getExpiryTime() {
        return parseInt(this.decodedToken['exp'])
    }

    isTokenExpired(): boolean {
        const expiryTime = this.getExpiryTime()
        if (expiryTime) {
            return (Math.floor((new Date).getTime() / 1000)) >= expiryTime
        } else {
            return true;
        }
    }
}
