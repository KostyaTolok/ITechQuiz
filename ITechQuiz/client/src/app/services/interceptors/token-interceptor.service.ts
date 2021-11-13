import {Injectable} from '@angular/core';
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor, HttpHeaders
} from '@angular/common/http';
import {Observable} from 'rxjs';
import {JwtTokenService} from "../jwt-token.service";

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

    constructor(private jwtTokenService: JwtTokenService) {
    }

    intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
        
        if (!request.url.includes("login") && !request.url.includes("register")) {
            request = request.clone(
                {
                    headers: request.headers
                        .set("Authorization", `Bearer ${this.jwtTokenService.getJwtToken()}`)
                }
            )
        }

        return next.handle(request);
    }
}
