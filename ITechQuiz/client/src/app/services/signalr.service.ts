import {Injectable} from '@angular/core';
import * as signalR from "@microsoft/signalr";

@Injectable({
    providedIn: 'root'
})
export class SignalrService {

    public notify: boolean = false
    
    constructor() {
    }
    
    private hubConnection?: signalR.HubConnection
    
    startConnection(){
        this.hubConnection = new signalR.HubConnectionBuilder()
            .withUrl('https://localhost:5001/notify')
            .build();
        
        this.hubConnection
            .start()
            .then(() => console.log('Connection started'))
            .catch(err => console.log('Error while starting connection: ' + err))
    }
    
    addNotificationListener(){
        if (this.hubConnection)
            this.hubConnection.on('Receive', (notify) => {
                this.notify = notify;
            });
    }
}
