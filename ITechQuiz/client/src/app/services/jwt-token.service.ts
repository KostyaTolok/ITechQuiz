import {Injectable} from '@angular/core';
import jwtDecode from "jwt-decode";

@Injectable()
export class JwtTokenService {

    private jwtToken: string = ""
    private decodedToken: { [key: string]: string } = {}

    constructor() {
    }

    setJwtToken(token: string | null) {
        if (token){
            this.jwtToken = token
            this.decodedToken = jwtDecode(token)
        }
    }
    
    removeToken(){
        this.jwtToken = ""
        this.decodedToken = {}
    }
    
    getJwtToken(){
        return this.jwtToken
    }
    
    getEmail(){
        return this.decodedToken['sub']
    }
    
    getRoles(){
        return this.decodedToken['roles']
    }
    
    getExpiryTime(){
        return parseInt(this.decodedToken['exp'])
    }

    isTokenExpired(): boolean {
        const expiryTime = this.getExpiryTime()
        if (expiryTime) {
            return ((1000 * expiryTime) - (new Date()).getTime()) < 5000;
        } else {
            return true;
        }
    }
}
