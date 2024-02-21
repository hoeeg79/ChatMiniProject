import { Component } from '@angular/core';
import { RoomsComponent } from './rooms/rooms.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'The Chad';
  rooms = [new RoomsComponent, new RoomsComponent];
  signedIn: boolean = false;

  onSignIn(){
    this.signedIn = true;
  }
}
