import {Injectable} from '@angular/core';
import {
  BaseDto,
  MessagesInRoom,
  ServerAddsClientToRoomDto,
  ServerBroadcastMessageWithUsernameDto,
  ServerLogInResponseDto
} from "./BaseDto";

@Injectable({
  providedIn: 'root'
})
export class StateService {
  //Homepage variables
  rooms = [];
  signedIn: boolean = false;
  ws: WebSocket = new WebSocket("ws://localhost:8181");

  //For rooms
  messages: MessagesInRoom[] = [];
  username: string;
  currentRoom: number;
  inRoom: boolean;

  constructor() {
    this.ws.onmessage = message => {
      const messageFromServer = JSON.parse(message.data) as BaseDto<any>

      // @ts-ignore
      this[messageFromServer.eventType].call(this, messageFromServer);
    }
  }

  ServerAddsClientToRoom(dto: ServerAddsClientToRoomDto){
    dto.oldMessages.forEach(message => this.messages.push(message));

  }

  ServerLogInResponse(dto: ServerLogInResponseDto){
    dto.rooms.sort((a, b) => a - b);
    dto.rooms.forEach(number => this.rooms.push(number));
  }

  ServerBroadcastMessageWithUsername(dto: ServerBroadcastMessageWithUsernameDto){
    this.messages.push(dto.message)
  }
}
