import {Component, OnInit} from '@angular/core';
import {ChangePasswordModel} from "../../models/change-password-model";
import {AuthService} from "../../services/auth.service";
import {Router} from "@angular/router";
import {JwtTokenService} from "../../services/jwt-token.service";

@Component({
    selector: 'change-password',
    templateUrl: 'change-password.component.html'
})
export class ChangePasswordComponent implements OnInit {

    model: ChangePasswordModel = new ChangePasswordModel()
    errorMessage: string = ""

    constructor(private authService: AuthService, private router: Router,
                private jwtTokenService: JwtTokenService) {
    }

    ngOnInit(): void {
        this.model.email = this.jwtTokenService.getEmail()
    }

    changePassword() {
        this.authService.changePassword(this.model)
            .subscribe((data) => {
                    console.log(data)
                    this.router.navigateByUrl("/profile")
                },
                error => {
                    console.log(error)
                    this.errorMessage = error
                })
    }

}
