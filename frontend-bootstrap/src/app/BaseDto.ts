export class BaseDto<T> {
    eventType: string;

    constructor(init?: Partial<T> ) {
      this.eventType = this.constructor.name;
      Object.assign(this, init);
    }
  }

  export class ServerLogInResponseDto extends BaseDto<ServerLogInResponseDto>{
    rooms?: number[];
  }

  export class  ServerAddsClientToRoomDto extends BaseDto<ServerAddsClientToRoomDto>{
   message?: string;
   oldMessages?: MessagesInRoom[];
  }

  export class ServerBroadcastMessageWithUsernameDto extends BaseDto<ServerBroadcastMessageWithUsernameDto>{
    message: MessagesInRoom;
  }

  export class MessagesInRoom {
    message: string;
    username: string;
    roomid?: number;
    id?: number;
  }
