document.addEventListener("DOMContentLoaded", function () {
    const ALERT_TIME = "08:30";
    const CHECK_INTERVAL = 5 * 60 * 1000;

    const searchForm = document.getElementById("searchForm");
    const searchCity = document.getElementById("searchCity");
    const weatherResult = document.getElementById("weatherResult");

    function fetchWeather(city, isAutoCheck = false) {
        fetch(`/api/weather/${city}`)
            .then(response => response.json())
            .then(data => {
                console.log("Weather API response:", data);

                if (!data || !data.city) {
                    weatherResult.innerHTML = `<p class="text-danger">Weather data not available for ${city}.</p>`;
                    return;
                }

                weatherResult.innerHTML = `
                    <h3>Weather in ${data.city}</h3>
                    <p>Temperature: ${data.temperature}°C</p>
                    <p>Min: ${data.minTemperature}°C, Max: ${data.maxTemperature}°C</p>
                    <p>Condition: ${data.description}</p>
                `;

                localStorage.setItem("lastSearchedCity", data.city);

                checkAndShowRainAlert(data.city, data.precipitation, isAutoCheck);
            })
            .catch(error => console.error("Error fetching weather:", error));
    }

    function checkAndShowRainAlert(city, precipitation, isAutoCheck) {
        const today = new Date().toISOString().split("T")[0];
        let lastAlertDate = localStorage.getItem("rainAlertDate");

        if (precipitation > 0) {
            if (lastAlertDate !== today && isAfterAlertTime()) {
                console.log(`Showing alert: Last alert was on ${lastAlertDate}, today is ${today}`);
                showRainAlert();
                localStorage.setItem("rainAlertDate", today);
            }
        }
    }

    function isAfterAlertTime() {
        const now = new Date();
        const alertTime = new Date();
        const [hours, minutes] = ALERT_TIME.split(":").map(Number);
        alertTime.setHours(hours, minutes, 0, 0);

        return now >= alertTime;
    }

    function showRainAlert() {
        const modalElement = document.getElementById("weatherAlertModal");
        const modal = new bootstrap.Modal(modalElement);

        modalElement.addEventListener("hidden.bs.modal", function () {
            document.title = "Weather App";
        });

        document.title = `⚠️ Rain Alert`;
        modal.show();
    }

    searchForm.addEventListener("submit", function (e) {
        e.preventDefault();
        fetchWeather(searchCity.value);
    });

    window.onload = function () {
        const lastCity = localStorage.getItem("lastSearchedCity");
        if (lastCity) {
            fetchWeather(lastCity, true);
        }
    };

    setInterval(() => {
        const lastCity = localStorage.getItem("lastSearchedCity");
        if (lastCity) {
            console.log("Auto-checking weather for", lastCity);
            fetchWeather(lastCity, true);
        }
    }, CHECK_INTERVAL);
});