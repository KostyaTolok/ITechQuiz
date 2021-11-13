import {Component, OnDestroy, OnInit} from '@angular/core';
import {User} from "../../../../models/user";
import {Subscription} from "rxjs";
import {UsersService} from "../../../../services/users.service";
import {Survey} from "../../../../models/survey";
import {JwtTokenService} from "../../../../services/jwt-token.service";

@Component({
    selector: 'admin-users-list',
    templateUrl: 'admin-users-list.component.html',
})
export class AdminUsersListComponent implements OnInit, OnDestroy {

    users: User[] | undefined
    subscription?: Subscription
    
    constructor(private usersService: UsersService,
                private jwtTokenService: JwtTokenService) {
    }

    ngOnInit(): void {
        this.loadUsers()
    }

    ngOnDestroy() {
        this.subscription?.unsubscribe()
    }

    loadUsers() {
        this.subscription = this.usersService.getUsers()
            .subscribe((data: User[]) => this.users = data)
    }
    
    isAdmin(email: string | undefined){
        if (email)
            return email == this.jwtTokenService.getEmail()
        return false
    }
}
