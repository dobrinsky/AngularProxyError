import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  public baseUrlValue: string = "";

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrlValue = baseUrl;
  }
}
