import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomePage } from './home.page';
import {MainpageComponent} from "../mainpage/mainpage.component";

const routes: Routes = [
  {
    path: '',
    component: HomePage,
  },
  {
    path: 'mainpage',
    component: MainpageComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomePageRoutingModule {}
