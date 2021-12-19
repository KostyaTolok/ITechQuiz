import {Component, Input, OnDestroy, OnInit} from '@angular/core';
import {User} from "../../models/user";
import {JwtTokenService} from "../../services/jwt-token.service";
import {AuthService} from "../../services/auth.service";
import {MatExpansionModule} from '@angular/material/expansion';
import {Survey} from "../../models/survey";
import {Subscription} from "rxjs";
import {SurveysService} from "../../services/surveys.service";

@Component({
    selector: 'user-detail',
    templateUrl: 'user-detail.component.html'
})
export class UserDetailComponent implements OnInit {

    @Input() user: User | undefined
    @Input() isAdmin = false
    @Input() passedSurveys: Survey[] | undefined
    
    openCreatedSurveys = false
    openPassedSurveys = false

    constructor(public jwtTokenService: JwtTokenService,
                public authService: AuthService) {
    }

    ngOnInit(): void {
    }
    

}
