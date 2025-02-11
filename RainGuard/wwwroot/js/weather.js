document.addEventListener("DOMContentLoaded", function () {
    const searchForm = document.getElementById("searchForm");
    const searchCity = document.getElementById("searchCity");
    const notificationCity = document.getElementById("notificationCity");
    const saveCityButton = document.getElementById("saveCity");
    const weatherResult = document.getElementById("weatherResult");

    function fetchWeather(city) {
        fetch(`/api/weather/${city}`)
            .then(response => response.json())
            .then(data => {
                weatherResult.innerHTML = `
                    <h3>Weather in ${data.city}</h3>
                    <p>Temperature: ${data.temperature}°C</p>
                    <p>Min: ${data.minTemperature}°C, Max: ${data.maxTemperature}°C</p>
                    <p>Condition: ${data.description}</p>
                `;

                if (data.precipitation > 0) {
                    document.title = `⚠️ Rain in ${data.city}`;
                    showRainAlert();
                }
            })
            .catch(error => console.error("Error fetching weather:", error));
    }

    searchForm.addEventListener("submit", function (e) {
        e.preventDefault();
        fetchWeather(searchCity.value);
    });

    saveCityButton.addEventListener("click", function () {
        localStorage.setItem("notificationCity", notificationCity.value);
    });

    setInterval(() => {
        const savedCity = localStorage.getItem("notificationCity");
        if (savedCity) fetchWeather(savedCity);
    }, 300000);
});
