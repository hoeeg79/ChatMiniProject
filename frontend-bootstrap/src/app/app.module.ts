import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { RoomsComponent } from './rooms/rooms.component';
import {Route, RouterModule} from "@angular/router";
import { HomepageComponent } from './homepage/homepage.component';
import {ReactiveFormsModule} from "@angular/forms";

const routes: Route[] = [
  {
    path: 'room/:id',
    component: RoomsComponent
  },
  {
    path: '',
    component: HomepageComponent
  }
]

@NgModule({
  declarations: [
    AppComponent,
    RoomsComponent,
    HomepageComponent
  ],
  imports: [
    RouterModule.forRoot(routes),
    BrowserModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
