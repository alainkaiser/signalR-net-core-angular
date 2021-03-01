import { Injectable } from '@angular/core';
import * as signalr from '@microsoft/signalr';
import ILiveData from './models/live-data-dto.model';

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
    this.connection.on('LiveData', (data: ILiveData) => {
      console.log(data);

      console.log(data.firstName);
      console.log(data.lastName);
    });
  };
}
