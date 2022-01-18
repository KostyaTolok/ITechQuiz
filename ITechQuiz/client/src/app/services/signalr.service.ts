import {Injectable} from '@angular/core';
import * as signalR from "@microsoft/signalr";
import {JwtTokenService} from "./jwt-token.service";
import {LocalStorageService} from "./local-storage.service";

@Injectable({
    providedIn: 'root'
})
export class SignalrService {

    public notify: boolean = false

    constructor(private jwtTokenService: JwtTokenService,
                private localStorageService: LocalStorageService) {
    }

    private hubConnection?: signalR.HubConnection

    startConnection() {
        this.hubConnection = new signalR.HubConnectionBuilder()
            .withUrl('https://localhost:5001/notify', {
                skipNegotiation: true,
                transport: signalR.HttpTransportType.WebSockets,
                accessTokenFactory: () => this.jwtTokenService.getJwtToken()
            })
            .build();

        this.hubConnection
            .start()
            .then(() => console.log('Connection started'))
            .catch(err => console.log('Error while starting connection: ' + err))
        
        this.addNotificationListener()
    }
    
    stopConnection(){
        this.hubConnection?.stop()
            .then(() => console.log('Connection stopped'))
    }

    addNotificationListener() {
        if (this.hubConnection)
            this.hubConnection.on('Receive', (notify) => {
                this.notify = notify;
                this.localStorageService.set("notify", JSON.parse(notify))
            });
    }
    
    restartConnection(){
        this.stopConnection()
        this.startConnection()
    }

    invokeNotify(surveyId: string) {
        this.hubConnection?.invoke("Notify", surveyId)
    }
}
