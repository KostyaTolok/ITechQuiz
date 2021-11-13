import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {User} from "../../models/user";
import {JwtTokenService} from "../../services/jwt-token.service";
import {AuthService} from "../../services/auth.service";
import {Router} from "@angular/router";

@Component({
    selector: 'user-detail',
    templateUrl: 'user-detail.component.html'
})
export class UserDetailComponent implements OnInit {
    
    @Input() user: User | undefined
    @Input() isAdmin = false
    constructor(public jwtTokenService: JwtTokenService,
                public authService: AuthService) {
    }

    ngOnInit(): void {
    }
    
}
