import {Component, OnInit} from '@angular/core';
import {Category} from "../../../../models/category";
import {Subscription} from "rxjs";
import {CategoriesService} from "../../../../services/categories.service";
import {Survey} from "../../../../models/survey";

@Component({
    selector: 'client-categories-list',
    templateUrl: 'client-categories-list.component.html',
})
export class ClientCategoriesListComponent implements OnInit {

    categories: Category[] | undefined

    subscription?: Subscription
    
    constructor(private categoriesService : CategoriesService) {
    }

    ngOnInit(): void {
        this.loadCategories()
    }

    loadCategories() {
        this.subscription = this.categoriesService.getCategories()
            .subscribe((data: Category[]) => this.categories = data)
    }
}
