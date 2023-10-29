
# Weather App C# .NET Core TDD Clean Architecture

A sample project (Weather App) built with C# .NET Core using CQRS Pattern, TDD and Clean Architecture

## Installation

- Clone this Project
- [Download and Install .NET Core SDK (3.1) ](https://dotnet.microsoft.com/en-us/download/dotnet/3.1)
- Download and Install your favorite IDE (Visual Studio, Visual Studio Code, Rider)
- Copy/Move `/WeatherApp.Web/example.appsettings.json` to `/WeatherApp.Web/appsettings.json`, edit your BaseApiURL config

```bash
  "BaseApiURL": "http://localhost:5114"
```
- Copy/Move `/WeatherApp.WebAPI/example.appsettings.json` to `/WeatherApp.WebAPI/appsettings.json`, edit your `ApiKey` config

```bash
 "OpenWeatherMap": {
    "BaseApiURL": "http://api.openweathermap.org/data/2.5/",
    "ApiKey": "<your_own_key>"
  }
```

- and Connection Strings

```bash
  "ConnectionStrings": {
    "Sqlite": "Data Source=weather.db"
  },
```

- Publish this App using `dotnet` command
```bash
  dotnet publish
```
- Run Web API (with Swagger UI), with following command
```bash
  cd .\WeatherApp.WebAPI\bin\Debug\netcoreapp3.1\
  dotnet WeatherApp.WebAPI.dll
```
  When Web API has started, you can access it in URL `http://localhost:5114/` or access Swagger UI in `http://localhost:5114/swagger/index.html`

- Goto root folder solution (`skip` if you already in root folder solution)
```bash
  cd ..\..\..\..
```
- Run Web (Frontend), with following command

```bash
  cd .\WeatherApp.Web\bin\Debug\netcoreapp3.1\
  dotnet WeatherApp.Web.dll
```
When Web (Frontend) has started, you can access it in URL `http://localhost:5000`

## Running Tests

To run tests, run the following command

```bash
  dotnet test
```
## Notes

Because of OpenWeatherMap doesn't have an api to get country and city data, so I created sample data for countries and cities, namely
- For countries: Indonesia (ID), and Australia (AU)
- For cities in Indonesia: Bekasi, Bogor, Jakarta, Depok, Tangerang, South Tangerang
- For cities in Australia: Adelaide, Brisbane, Canberra, Darwin, Perth, Sydney

Also, I use the "cityName, countryCode" format in the "q" parameter in the weather api in OpenWeatherMap to increase accuracy because there are several city names that are the same in several countries.
`http://api.openweathermap.org/data/2.5/weather?q=cityName,countryCode`

## API Reference

#### Get All Country
You can get all country in database

```http
  GET /country
```

| Parameter |
| :-------- |
| None |

Example request :
```http
  GET http://localhost:5114/country
```

This is sample body request (JSON) for this API
```http
[
  {
    "id": "daa7e2cb-ec18-4ede-8bcf-bdb6df030d64",
    "code": "AU",
    "name": "Australia"
  },
  {
    "id": "da0b2d14-bf3e-43b4-911a-fc05574490fe",
    "code": "ID",
    "name": "Indonesia"
  }
]
```

#### Get All City By Country Code
You can get all city by country code in database

```http
  GET /city/{countryCode}
```

| Parameter | Type     | Is Required                | Description                |
| :-------- | :------- | :------------------------- | :----------- |
| `countryCode` | `string` of country code | **Yes**. | Country Code |

Example request :
```http
  GET http://localhost:5114/city/ID
```

This is sample body request (JSON) for this API
```http
[
  {
    "id": "5331f925-4661-43c9-bc1b-91f78301ddb0",
    "countryCode": "ID",
    "name": "Bekasi",
    "lat": -6.2349,
    "lon": 106.9896
  },
  {
    "id": "b694c23e-76b2-4f32-853f-23cfa21a4937",
    "countryCode": "ID",
    "name": "Bogor",
    "lat": -6.5944,
    "lon": 106.7892
  },
  {
    "id": "ade68989-f876-42b9-a630-7170871549eb",
    "countryCode": "ID",
    "name": "Depok",
    "lat": -6.402484,
    "lon": 106.794243
  },
  {
    "id": "2c251bbc-e2fa-4788-91f9-976d0d05e741",
    "countryCode": "ID",
    "name": "Jakarta",
    "lat": -6.2146,
    "lon": 106.8451
  },
  {
    "id": "a91e6066-7dc2-4949-95b0-d3ad559d90bc",
    "countryCode": "ID",
    "name": "Tangerang",
    "lat": -6.1781,
    "lon": 106.63
  },
  {
    "id": "270e44e0-dacf-43b6-8bb3-e38db73f1539",
    "countryCode": "ID",
    "name": "Tangerang Selatan",
    "lat": -6.32674,
    "lon": 106.730042
  }
]
```

#### Get Weather Data By Country Code And City Name
You can get weather data in selected location (by country code and city name)

```http
  GET /weather/{countryCode}/{cityName}
```

| Parameter | Type     | Is Required                | Description                |
| :-------- | :------- | :------------------------- | :----------- |
| `countryCode` | `string` of country code | **Yes**. | Country Code |
| `cityName` | `string` of City Name | **Yes**. | City Name |


Example request :
```http
  GET http://localhost:5114/weather/query/ID/Tangerang%20Selatan
```



This is sample body request (JSON) for this API
```http
{
  "location": {
    "lon": 106.7085,
    "lat": -6.2955,
    "countryId": "ID",
    "cityName": "Tangerang Selatan"
  },
  "time": 1698584569,
  "wind": {
    "speed": 5.75,
    "deg": 260,
    "gust": 0
  },
  "visibility": 5000,
  "skyConditions": {
    "id": 721,
    "main": "Haze",
    "description": "haze",
    "icon": "50n"
  },
  "temperatureFahrenheit": 87.58,
  "temperatureCelcius": 30.877777777777776,
  "dewPoint": 0,
  "relativeHumidity": 55,
  "pressure": 1009
}
```

## Screenshots

#### Web

![App Screenshot](/images/weather_app.png)

#### API (Swagger UI)

![App Screenshot](/images/weather_app_swagger.png)
## Tech Stack

**Client:** .NET Core Web (v3.1), Razor, jQuery, Bootstrap

**Server:** .NET Core Web API (v3.1), SqLite, Mediatr (CQRS)

**Automation Test:** Moq, xUnit, Shouldly


## References

- [Clean Architecture in ASP .NET Core Web API](https://juldhais.net/clean-architecture-in-asp-net-core-web-api-4e5ef0b96f99) by [@juldhais](https://www.github.com/juldhais)
- [ASP.NET Core - Clean Architecture - Full Course](https://www.youtube.com/watch?app=desktop&v=gGa7SLk1-0Q) by [@trevoirwilliams](https://www.github.com/trevoirwilliams)
- [Clean Architecture with .NET Core: Getting Started](https://jasontaylor.dev/clean-architecture-getting-started/) by [@jasontaylordev](https://www.github.com/jasontaylordev)
- [How To Mock HttpClient in C# Unit Tests](https://www.youtube.com/watch?v=bj5AUxK6Ov4) by [@T0shik](https://www.github.com/T0shik)

