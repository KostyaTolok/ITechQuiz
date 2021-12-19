import {Component, OnDestroy, OnInit} from "@angular/core";
import {AuthService} from "../../services/auth.service";
import {Subscription} from "rxjs";
import {SignalrService} from "../../services/signalr.service";

@Component({
    selector: 'header-insight',
    templateUrl: 'header-insight.component.html',
    styleUrls: []
})
export class HeaderInsightComponent implements OnInit {
    title = "ITechQuiz"
    
    constructor(public authService: AuthService, public signalrService: SignalrService) {
    }

    ngOnInit() {
        this.signalrService.startConnection()
        this.signalrService.addNotificationListener()
    }
}