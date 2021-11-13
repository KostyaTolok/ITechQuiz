import {Component, OnDestroy, OnInit} from '@angular/core';
import {AssignRequestService} from "../../../../services/assign-request.service";
import {AssignRequest} from "../../../../models/assign-request";
import {Subscription} from "rxjs";
import {LocalStorageService} from "../../../../services/local-storage.service";

@Component({
    selector: 'admin-assign-requests-list',
    templateUrl: 'admin-assign-requests-list.component.html'
})
export class AdminAssignRequestsListComponent implements OnInit, OnDestroy {

    public requests: AssignRequest[] | undefined
    subscription?: Subscription
    includeRejected = true
    sorted = false

    constructor(private assignRequestService: AssignRequestService,
                private localStorageService: LocalStorageService) {
    }

    ngOnInit(): void {

        this.includeRejected = JSON.parse(this.localStorageService.get("includeRejected", "true"))

        this.sorted = JSON.parse(this.localStorageService.get("sorted", "false"))

        this.loadAssignRequests()
    }

    ngOnDestroy() {
        this.subscription?.unsubscribe()
    }

    loadAssignRequests() {
        this.subscription = this.assignRequestService.getAssignRequests
        (this.includeRejected, this.sorted)
            .subscribe((data: AssignRequest[]) => this.requests = data)
    }

    setIncludeRejected(value: boolean) {
        this.localStorageService.set("includeRejected", JSON.stringify(value))
        this.includeRejected = value
        this.loadAssignRequests()
    }

    setSorted(value: boolean) {
        this.localStorageService.set("sorted", JSON.stringify(value))
        this.sorted = value
        this.loadAssignRequests()
    }
}
