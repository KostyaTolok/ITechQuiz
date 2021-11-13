import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {User} from "../models/user";

@Injectable()
export class UsersService {

    private url = '/api/admin/users'

    constructor(private http: HttpClient) {
    }

    getUsers() {

        return this.http.get<User[]>(this.url)
    }

    getUserById(id: string) {

        return this.http.get<User>(`${this.url}/${id}`)
    }

    getUserByEmail(email: string) {

        return this.http.get<User>(`${this.url}/${email}`)
    }

    deleteUser(id: string) {

        return this.http.delete(`${this.url}/${id}`, {
            responseType: "text"
        })
    }

    disableUser(id: string, disableEnd: string) {

        return this.http.post(`${this.url}/disable`, {
            userId: id,
            disableEnd: disableEnd
        }, {
            responseType: "text"
        })
    }

    enableUser(id: string) {

        return this.http.post(`${this.url}/enable/${id}`,
            {},
            {
                responseType: "text"
            })
    }

    removeFromRole(userId: string, role: string) {

        return this.http.post(`${this.url}/remove-from-role`, {
            userId: userId,
            role: role
        }, {
            responseType: "text"
        })
    }

}
