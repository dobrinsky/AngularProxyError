import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: WeatherForecast[] = [];

  public forecastsTemp: any[] = [];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

    console.log(baseUrl + 'weatherforecast/list-weatherForecasts');

    http.get<WeatherForecast[]>(baseUrl + 'weatherforecast/list-weatherForecasts').subscribe(result => {
      console.log(result);
      //this.forecastsTemp = result as any[];
      this.forecasts = result;

    }, error => console.error(error));

    http.get(baseUrl + 'error/list-errors').subscribe(result => {
      console.log(result);
      
    }, error => console.error(error));
  }
}

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
