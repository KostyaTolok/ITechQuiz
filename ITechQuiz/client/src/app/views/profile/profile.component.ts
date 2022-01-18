import {Component, OnInit} from '@angular/core';
import {User} from "../../models/user";
import {JwtTokenService} from "../../services/jwt-token.service";
import {Subscription} from "rxjs";
import {UsersService} from "../../services/users.service";
import {AuthService} from "../../services/auth.service";
import {Router} from "@angular/router";
import {AssignRequestService} from "../../services/assign-request.service";
import {Roles} from "../../utils/roles";
import {Survey} from "../../models/survey";
import {SurveysService} from "../../services/surveys.service";
import {SignalrService} from "../../services/signalr.service";

@Component({
    selector: 'profile',
    templateUrl: 'profile.component.html'
})
export class ProfileComponent implements OnInit {

    user: User = new User()
    roles: { [key: string]: string } = Roles
    role: string = "admin"
    subscription?: Subscription
    passedSurveys?: Survey[] | undefined
    createdSurveys?: Survey[] | undefined
    sortedByDate = true

    constructor(private jwtTokenService: JwtTokenService,
                private usersService: UsersService,
                private authService: AuthService,
                private router: Router,
                private assignRequestService: AssignRequestService,
                private surveysService: SurveysService,
                private signalrService : SignalrService) {
    }

    ngOnInit(): void {
        this.loadUser()
        this.loadPassedSurveys()
        this.loadCreatedSurveys()
    }

    loadUser() {
        this.subscription = this.usersService.getUserByEmail(
            this.jwtTokenService.getEmail())
            .subscribe((data: User) => this.user = data)
    }

    loadPassedSurveys() {
        this.subscription = this.surveysService.getSurveys(true,false, null,
            [], this.sortedByDate)
            .subscribe((data: Survey[]) => this.passedSurveys = data)
    }


    loadCreatedSurveys() {
        this.subscription = this.surveysService.getSurveys(false, true)
            .subscribe((data: Survey[]) => {
                this.createdSurveys = data
            })
    }

    logout() {
        this.authService.logoutUser()
        this.signalrService.restartConnection()
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
    
    setSortedByDate(value: boolean){
        this.sortedByDate = value
        this.loadPassedSurveys()
    }
}
