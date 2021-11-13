import {Component, Inject, LOCALE_ID, OnDestroy, OnInit} from '@angular/core';
import {User} from "../../../../models/user";
import {Subscription} from "rxjs";
import {UsersService} from "../../../../services/users.service";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
    selector: 'admin-user-detail',
    templateUrl: 'admin-user-detail.component.html'
})
export class AdminUserDetailComponent implements OnInit, OnDestroy {

    user: User = new User()
    id?: string
    public disableEnd: string
    public dateNow: string
    public role: string = ""
    subscription?: Subscription

    constructor(private usersService: UsersService,
                private router: Router, private activatedRoute: ActivatedRoute) {
        this.id = activatedRoute.snapshot.params["id"]
        this.disableEnd = new Date().toISOString().slice(0, 16)
        this.dateNow = new Date().toISOString().slice(0, 16)
    }

    ngOnInit(): void {
        if (this.id) {
            this.loadUser(this.id)
        }
    }

    loadUser(id: string | undefined) {
        if (id)
            this.subscription = this.usersService.getUserById(id)
                .subscribe((data: User) => {
                    this.user = data
                    this.role = this.user.roles[0]
                })
    }

    disableUser() {
        if (this.id) {
            this.usersService.disableUser(this.id, this.disableEnd)
                .subscribe((data) => {
                    console.log(data)

                    if (this.id)
                        this.loadUser(this.id)
                })
        }
    }

    enableUser() {
        if (this.id) {
            this.usersService.enableUser(this.id)
                .subscribe((data) => {
                    console.log(data)

                    if (this.id)
                        this.loadUser(this.id)
                })
        }
    }

    deleteUser() {
        if (this.id) {
            this.usersService.deleteUser(this.id)
                .subscribe((data) => {
                    console.log(data)

                    this.router.navigateByUrl("admin/users")
                })
        }
    }
    
    removeFromRole(){
        if (this.id && this.role){
            this.usersService.removeFromRole(this.id, this.role)
                .subscribe((data) => {
                    console.log(data)

                    this.loadUser(this.id)
                })
        }
    }

    ngOnDestroy() {
        this.subscription?.unsubscribe()
    }
}
