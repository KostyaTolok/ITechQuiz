import {Component} from '@angular/core';
import {AuthService} from "../../services/auth.service";
import {LoginModel} from "../../models/login-model";
import {Router} from "@angular/router";
import {SignalrService} from "../../services/signalr.service";

@Component({
    selector: 'login',
    templateUrl: 'login.component.html',
    styleUrls: []
})
export class LoginComponent {

    model: LoginModel = new LoginModel()
    errorMessage: string = ""

    constructor(private authService: AuthService, private router: Router,
                private signalrService: SignalrService) {
    }

    onLogin() {
        this.authService.loginUser(this.model)
            .subscribe(() => {
                this.signalrService.restartConnection()
                this.router.navigate([""]).then(r => r)
            }, error => {
                console.error(error)
                this.errorMessage = error
            })
    }
}
