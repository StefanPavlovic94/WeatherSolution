var latitude; 
var longitude;
var map;
var myChart;
//gets data by api and use it for cards and chart
function getData(event) {
    event.preventDefault();
    var cityname = $("#searchbox").val();
    $.ajax({
        url: "/Weather/AsyncSearchCityName",
        type: "GET",
        data: { cityName: cityname },
        dataType: "text",
        success: function (data, status) {
            if (data !== "") {
                $(".hiddenDiv").show();
                myChart = initializeChart();
                var obj = JSON.parse(data);
                //Information about location(city)
                $("#name").html("City name: " + obj.city.name);
                $("#country").html("Country: " + obj.city.country);
                $("#population").html("Population: " + obj.city.population);
                //Main info extracted form list
                var i = 0;
                var pomDate;
                var date;
                $("#maindiv").empty();
                $("#map").remove();
                $("<div />", { id: "map", style: "width: 1100px; height: 400px; margin-top:0%; margin-left:3%; margin-right:0%" }).appendTo("#mapDiv");
                //Create cards and fill it with data
                $.each(obj.list, function (item) {
                    date = moment(this.dt_txt).format("LL");
                    if (date !== pomDate && i < 5) {
                        $("<div />", { class: "card", id: "subdiv" + i, onclick: "toggleCard(this)" }).appendTo("#maindiv");
                        $("<div />", { class: "cardrow", style: "margin-bottom:1%; font-weight:800;", text: date }).appendTo("#subdiv" + i);
                        $("<div />", { class: "cardrow", text: "Temp: ".concat(this.main.temp).concat(" °C") }).appendTo("#subdiv" + i);
                        $("<div />", { class: "cardrow", text: this.weather[0].description.charAt(0).toUpperCase() + this.weather[0].description.slice(1).toLowerCase() }).appendTo("#subdiv" + i);
                        $("<div />", { class: "cardrow", text: "Pressure: ".concat(this.main.pressure).concat(" Pa") }).appendTo("#subdiv" + i);
                        $("<div />", { class: "cardrow", text: "Humidity: ".concat(this.main.humidity).concat(" %") }).appendTo("#subdiv" + i);
                        $("<img />", { src: "http://openweathermap.org/img/w/".concat(this.weather[0].icon).concat(".png") }).appendTo("#subdiv" + i);
                        pomDate = date;
                        i++;
                    }
                });
                //var pomDateChart;
                //fill chart with data
                $.each(obj.list, function (item) {
                    var dateChart = moment(this.dt_txt).format("MM.DD. HH");
                    dateChart = dateChart.concat("h");
                    addData(myChart, dateChart, this.main.temp);
                });
                // })
                myChart.update();
                latitude = obj.city.coord.lat;
                longitude = obj.city.coord.lon;
                map = initializeMap();
            }
            else
            {
                window.location.reload(true);
                alert("City not found")
            }
        },                  
        failure: function (status) {
            alert("City not found");
            $("#demo").html("Failure");
        },
        error: function (status) {
            alert("Error occured");
            $("#demo").html("Error");
        }
    });
}
//return initialized chart
function initializeChart() {
    var ctx = document.getElementById("weatherChart").getContext('2d');
    var chart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: [],
            datasets: [{
                label: [],
                data: [],
                backgroundColor: [
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                    'rgba(255, 159, 64, 0.2)'
                ],
                borderColor: [
                    'rgba(255,99,132,1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(75, 192, 192, 1)',
                    'rgba(153, 102, 255, 1)',
                    'rgba(255, 159, 64, 1)'
                ],
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            }
        }
    });
    return chart;
}
//add labels and data to chart dataset
function addData(chart, label, data) {
    chart.data.labels.push(label);
    chart.data.datasets.forEach((dataset) => {
        dataset.data.push(data);
    });
    chart.update();
 }
//remove all data from charts datasets
function removeData(chart) {
    chart.data.labels.pop();
    chart.data.datasets.forEach((dataset) => {
        dataset.data.pop();
    });
    chart.update();
}
//return initalized map
function initializeMap(lat, lon) {
    var osm = L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 18,
        attribution: ''
    });

    var city = L.OWM.current({ showLegend: true, opacity: 0.5, appId: 'd49c3a5910fdc77e3a554cd0cd11681d' });
    var cloudscls = L.OWM.cloudsClassic({ showLegend: true, opacity: 0.5, appId: 'd49c3a5910fdc77e3a554cd0cd11681d' });
    var raincls = L.OWM.rainClassic({ showLegend: true, opacity: 0.5, appId: 'd49c3a5910fdc77e3a554cd0cd11681d' });
    var snow = L.OWM.snow({ showLegend: true, opacity: 0.5, appId: 'd49c3a5910fdc77e3a554cd0cd11681d' });
    var pressure = L.OWM.pressure({ showLegend: true, opacity: 0.5, appId: 'd49c3a5910fdc77e3a554cd0cd11681d' });
    var pressurecntr = L.OWM.pressureContour({ showLegend: true, opacity: 0.5, appId: 'd49c3a5910fdc77e3a554cd0cd11681d' });
    var temp = L.OWM.temperature({ showLegend: true, opacity: 0.5, appId: 'd49c3a5910fdc77e3a554cd0cd11681d' });
    var wind = L.OWM.wind({ showLegend: true, opacity: 0.5, appId: 'd49c3a5910fdc77e3a554cd0cd11681d' });
    

    var map = L.map('map', { center: new L.LatLng(latitude, longitude), zoom: 10, layers: [osm] });
    var baseMaps = { "OSM Standard": osm };
    var overlayMaps = {
        "Cities": city, "Clouds": cloudscls, "Rain": raincls, "Snow": snow,
        "Pressure": pressure, "Pressure contour": pressurecntr, "Temperature": temp, "Wind": wind
    };
    var layerControl = L.control.layers(baseMaps, overlayMaps).addTo(map); 
    return map;
}
//toggle effect on 5-day weather cards
function toggleCard(cardDiv) {
    var listOfCards = $("." + cardDiv.className).removeClass("acctiveCard");
    $("#" + cardDiv.id).toggleClass("acctiveCard");
    
}
//Hack for chart.js bug-showing last dataset when hover
var button;
$(document).ready(function () {
    button = document.getElementById("submitButton");
    button.addEventListener("click", function () {
        try {
            myChart.destroy();
        } catch (e){       
          console.log("YO", e)     
        }
    });
});
//Toggle password visibility
function tooglePass() {
    var x = document.getElementById("passInput");
    if (x.type === "password") {
        x.type = "text";
    } else {
        x.type = "password";
    }
}
