import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {AssignRequest} from "../../models/assign-request";
import {AssignRequestService} from "../../services/assign-request.service";
import {Subscription} from "rxjs";
import {B} from "@angular/cdk/keycodes";

@Component({
    selector: 'assign-requests-list',
    templateUrl: 'assign-requests-list.component.html'
})
export class AssignRequestsListComponent implements OnInit {

    @Input() requests: AssignRequest[] | undefined = []
    @Input() isAdmin: boolean = false
    @Output() updateRequestsEvent = new EventEmitter<void>()
    
    subscription?: Subscription

    constructor(private assignRequestService: AssignRequestService) {
    }

    ngOnInit(): void {
    }

    acceptAssignRequest(id: string | undefined) {
        if (id)
            this.assignRequestService.acceptAssignRequest(id)
                .subscribe((data) => {
                    console.log(data)
                    this.updateRequestsEvent.emit()
                })
    }

    rejectAssignRequest(id: string | undefined) {
        if (id)
            this.assignRequestService.rejectAssignRequest(id)
                .subscribe((data) => {
                    console.log(data)
                    this.updateRequestsEvent.emit()
                })
    }

    deleteAssignRequest(id: string | undefined) {
        if (id)
            this.assignRequestService.deleteAssignRequest(id)
                .subscribe((data) => {
                    console.log(data)
                    this.updateRequestsEvent.emit()
                })
    }
}
