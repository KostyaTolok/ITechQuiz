import {Component} from '@angular/core';
import {AuthService} from "../../services/auth.service";
import {LoginModel} from "../../models/login-model";
import {Router} from "@angular/router";

@Component({
    selector: 'login',
    templateUrl: 'login.component.html',
    styleUrls: []
})
export class LoginComponent {

    model: LoginModel = new LoginModel()
    errorMessage: string = ""

    constructor(private authService: AuthService, private router: Router) {
    }

    onLogin() {
        this.authService.loginUser(this.model)
            .subscribe(() => {
                this.router.navigate([""]).then(r => r)
            }, error => {
                console.error(error)
                this.errorMessage = error
            })
    }
}
