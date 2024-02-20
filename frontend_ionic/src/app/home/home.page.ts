import { Component } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
})
export class HomePage {
  username!: string;

  constructor(private http: HttpClient,
              private router: Router) {}

  onSignIn() {
    if(this.username.length > 0){
      this.router.navigateByUrl("/mainpage")
    }
  }
}
