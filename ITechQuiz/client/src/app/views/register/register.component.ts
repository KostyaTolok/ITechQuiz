import {Component, OnInit} from '@angular/core';
import {AuthService} from "../../services/auth.service";
import {RegisterModel} from "../../models/register-model";
import {Router} from "@angular/router";
import {SignalrService} from "../../services/signalr.service";

@Component({
    selector: 'register',
    templateUrl: 'register.component.html',
    styleUrls: []
})
export class RegisterComponent {

    model: RegisterModel = new RegisterModel()
    errorMessage: string = ""

    constructor(private authService: AuthService, private router: Router,
                private signalrService : SignalrService) {
    }

    onRegister() {
        this.authService.registerUser(this.model)
            .subscribe(() => {
                this.signalrService.restartConnection()
                this.router.navigate([""]).then(r => r)
            }, error => {
                console.error(error)
                this.errorMessage = error
            })
    }
}
