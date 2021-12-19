import {Component, OnInit} from '@angular/core';
import {Subscription} from "rxjs";
import {CategoriesService} from "../../../../services/categories.service";
import {ActivatedRoute, Router} from "@angular/router";
import {Category} from "../../../../models/category";
import {Survey} from "../../../../models/survey";

@Component({
    selector: 'client-category-detail',
    templateUrl: 'client-category-detail.component.html'
})
export class ClientCategoryDetailComponent implements OnInit {

    id?: string = undefined
    category: Category = new Category()
    subscription?: Subscription
    categories: Category[] = []
    nameError: boolean = false
    
    constructor(private categoriesService: CategoriesService,
                private activatedRoute: ActivatedRoute,
                private router: Router) {
        this.id = activatedRoute.snapshot.params["id"]
    }

    ngOnInit(): void {
        if (this.id) {
            this.loadCategory(this.id)
        } else {
            this.category = new Category()
        }
        this.loadCategories()
    }

    loadCategory(id: string) {
        this.subscription = this.categoriesService.getCategory(id)
            .subscribe((data: Category) => this.category = data)
    }
    
    loadCategories(){
        this.subscription = this.categoriesService.getCategories()
            .subscribe((data: Category[]) => this.categories = data)
    }

    saveCategory() {
        if (this.id) {
            this.categoriesService.updateCategory(this.category)
                .subscribe((data) => {
                        console.log(data)
                        this.router.navigateByUrl("/client/categories")
                    }
                )
        } else {
            for (const category of this.categories) {
                if (category.title == this.category.title){
                    this.nameError = true;
                    return
                }
            }
            this.categoriesService.addCategory(this.category)
                .subscribe((data) => {
                        console.log(data)
                        this.router.navigateByUrl("/client/categories")
                    }
                )
        }
    }
    
    deleteCategory(){
        if (this.id){
            this.categoriesService.deleteCategory(this.id)
                .subscribe((data) => {
                console.log(data)
                this.router.navigateByUrl("/client/categories")
            })
        }
    }

}
