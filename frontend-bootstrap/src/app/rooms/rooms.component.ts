import { Component } from '@angular/core';
import {BaseDto, MessagesInRoom, ServerAddsClientToRoomDto, ServerLogInResponseDto} from "../BaseDto";
import {Router} from "@angular/router";
import {StateService} from "../state.service";
import {FormControl} from "@angular/forms";

@Component({
  selector: 'app-rooms',
  templateUrl: './rooms.component.html',
  styleUrls: ['./rooms.component.css']
})
export class RoomsComponent {
  messageField = new FormControl('');

  constructor(private router: Router, public state: StateService){ }

  onSendMessage() {
    var dto = {
      eventType: "ClientWantsToBroadcastToRoom",
      Message: this.messageField.value,
      RoomId: this.state.currentRoom
    };
    this.state.ws.send(JSON.stringify(dto));
  }

  onDeleteMessage() {

  }
}
