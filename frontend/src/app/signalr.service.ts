import { Injectable } from '@angular/core';
import * as signalr from '@microsoft/signalr';

@Injectable({
  providedIn: 'root',
})
export class SignalrService {
  private connection: signalr.HubConnection;

  public startConnection = () => {
    this.connection = new signalr.HubConnectionBuilder()
      .withUrl('https://localhost:5001/liveDataHub', {
        skipNegotiation: true,
        transport: signalr.HttpTransportType.WebSockets,
      })
      .build();

    this.connection
      .start()
      .then(() => console.log('Connection started...'))
      .catch((error) =>
        console.log(`Error while starting connection: ${error}`)
      );
  };

  public addLiveDataListener = () => {
    this.connection.on('LiveData', (data: string) => {
      console.log(data);
    });
  };
}
