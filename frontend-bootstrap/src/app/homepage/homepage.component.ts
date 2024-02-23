import { Component } from '@angular/core';
import {FormControl} from "@angular/forms";
import {BaseDto, ServerLogInResponseDto} from "../BaseDto";
import {Router} from "@angular/router";
import {StateService} from "../state.service";

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.css']
})
export class HomepageComponent {
  username = new FormControl('');
  newRoom: FormControl<number> = new FormControl();


  constructor(private router: Router, public state: StateService){ }
  onSignIn(){
    this.state.signedIn = true;

    var dto = {
      eventType: "ClientWantsToSignIn",
      Username: this.username.value
    };

    this.state.ws.send(JSON.stringify(dto))
    this.state.username = this.username.value;
  }

  onEnterRoom(number: number){
    var dto = {
      eventType: "ClientWantsToEnterRoom",
      RoomId: number
    }
    this.state.ws.send(JSON.stringify(dto))
    this.state.currentRoom = number
    this.state.inRoom = true;

    this.router.navigateByUrl('/room/' + number);
  }


  onCreateNewRoom($event: MouseEvent) {
    var dto = {
      eventType: "ClientWantsToEnterRoom",
      RoomId: this.newRoom.value
    }
    this.state.ws.send(JSON.stringify(dto))
    this.state.currentRoom = this.newRoom.value
    this.state.inRoom = true;
    this.router.navigateByUrl('/room/' + this.newRoom.value);
  }
}
