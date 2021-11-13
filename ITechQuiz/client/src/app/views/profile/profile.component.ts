import {Component, OnInit} from '@angular/core';
import {User} from "../../models/user";
import {JwtTokenService} from "../../services/jwt-token.service";
import {Subscription} from "rxjs";
import {UsersService} from "../../services/users.service";
import {AuthService} from "../../services/auth.service";
import {Router} from "@angular/router";
import {AssignRequestService} from "../../services/assign-request.service";
import {Roles} from "../../utils/roles";

@Component({
    selector: 'profile',
    templateUrl: 'profile.component.html'
})
export class ProfileComponent implements OnInit {

    user: User = new User()
    roles: { [key: string]: string } = Roles
    role: string = "admin"
    subscription?: Subscription

    constructor(private jwtTokenService: JwtTokenService,
                private usersService: UsersService,
                private authService: AuthService,
                private router: Router,
                private assignRequestService: AssignRequestService) {
    }

    ngOnInit(): void {
        this.loadUser()
    }

    loadUser() {
        this.subscription = this.usersService.getUserByEmail(
            this.jwtTokenService.getEmail())
            .subscribe((data: User) => this.user = data)
    }

    logout() {
        this.authService.logoutUser()
        this.router.navigateByUrl("/")
    }

    makeAssignRequest() {
        if (this.user?.id && this.role)
            this.assignRequestService.makeAssignRequest(this.user.id, this.role)
                .subscribe((data) => {
                    console.log(data)
                    this.loadUser()
                })
    }
}
