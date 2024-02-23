import { Component } from '@angular/core';
import { RoomsComponent } from './rooms/rooms.component';
import { FormControl } from '@angular/forms';
import { ServerLogInResponseDto, BaseDto } from './BaseDto';
import {Router} from "@angular/router";
import {StateService} from "./state.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'The Chad';
  constructor(private router: Router, public state: StateService){}

  onHomeButtonPress() {
    this.state.inRoom = false;
    this.state.messages = [];
    this.router.navigateByUrl('')
  }
}
