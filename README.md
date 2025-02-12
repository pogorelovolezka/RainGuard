# 🌧️ RainGuard - Weather Monitoring App

RainGuard is an **ASP.NET Core MVC** application that allows users to get real-time weather forecasts, receive **daily rain alerts**, and save the last searched city for future use.

---

## Features
 - **Search for any city** and get its weather forecast.  
 - **See today's temperature** (min/max) and weather description.  
 - **Get a rain alert once per day** after **08:30 AM**.  
 - **Automatic weather updates every 5 minutes.**  
 - **Last searched city is saved locally.**  

---

## Technologies Used
- **Backend:** ASP.NET Core MVC (.NET 9)
- **Frontend:** Bootstrap, JavaScript (AJAX)
- **Data Storage:** LocalStorage (for saving last searched city)
- **Weather API:** OpenWeatherMap  

---

## Installation & Setup

### Download the Project
1. Go to the repository: **[GitHub Repository](https://github.com/pogorelovolezka/RainGuard)**
2. Click the **"Code"** button and select **"Download ZIP"**.
3. Extract the downloaded archive to any folder.

---

### Create Configuration Files (`appsettings.json`)
You need to create **two configuration files** in the project root:

**`appsettings.json`**
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "OpenWeatherMap": {
    "ApiKey": "YOUR_API_KEY"
  }
}
```
**`appsettings.Development.json`**
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```
### Restore Dependencies & Run the App
Run the following commands in the project directory:
 - dotnet restore
 - dotnet run
 - The app will be available at: https://localhost:7152/

### API Endpoints
Get weather forecast for a city
 - GET https://localhost:7152/api/weather/{city}
Example request:
 - GET https://localhost:7152/api/weather/Kyiv
Example API Response:
```json
{
  "city": "Kyiv",
  "temperature": -5.08,
  "minTemperature": -6.5,
  "maxTemperature": -4.2,
  "precipitation": 1,
  "description": "light rain"
}
```
### ⚠️ How Rain Alerts Work
 - Rain alerts are displayed once per day after 08:30 AM.
 - The last alert date is stored in LocalStorage to prevent duplicate alerts.
 - If it’s past 08:30 AM and rain is expected, the user receives a notification.
 - The user will also see a warning immediately if they search for a city where rain is expected

### 📬 Contact
 - **Developer**: Pohorelov Oleh
 - **Email**: pohorelov.oe@gmail.com
 - **Mobile**: +38 (097) 094 86-78
