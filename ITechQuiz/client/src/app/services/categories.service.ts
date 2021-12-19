import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Survey} from "../models/survey";
import {Category} from "../models/category";

@Injectable({
    providedIn: 'root'
})
export class CategoriesService {

    private url = '/api/categories'

    constructor(private http: HttpClient) {
    }

    getCategories() {
        return this.http.get<Category[]>(this.url)
    }

    getCategory(id: string) {
        return this.http.get<Category>(`${this.url}/${id}`)
    }

    updateCategory(category: Category) {
        return this.http.put(this.url, category, {
            responseType: 'text'
        })
    }

    addCategory(category: Category) {
        return this.http.post<Category>(this.url, category)
    }

    deleteCategory(id: string) {
        return this.http.delete(`${this.url}/${id}`, {
            responseType: 'text'
        })
    }
}
