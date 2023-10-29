/*
       https://jsfiddle.net/moshfeu/3hrupdn1/22/
       https://stackoverflow.com/questions/48009382/javascript-make-select-2-use-url-set-by-outside-source
       */

$(document).ready(function () {
    var selectedCountryCode = "NONE";
    $('#inputCountry').select2({
        placeholder: "Select Country...",
        templateSelection: function (state) {
            if (!state.id) {
                return state.text;
            }
            return $(`<span>${state.text}</span>`);
        },
        ajax: {
            type: "GET",
            dataType: 'json',
            url: BASE_API_URL + '/country',
            processResults: function (data) {
                var options = [];
                options.push({ id: null, text: "-- Please Select Country --", text2: "" });
                if (data) {
                    $.each(data, function (index, row) {
                        options.push({ id: row.code, text: row.name, text2: row.id });
                    });
                }
                return {
                    results: options,
                    more: false
                };
            }
        }
    });
    $('#inputCountry').on('select2:select', function (e) {
        var data = e.params.data;
        selectedCountryCode = data.id;
        $("#inputCity").val(null).trigger("change");
        selectedCityName = "";
    });

    var selectedCityName = "";

    $('#inputCity').select2({
        placeholder: "Select City...",
        templateSelection: function (state) {
            if (!state.id) {
                return state.text;
            }
            return $(`<span>${state.text}</span>`);
        },
        ajax: {
            type: "GET",
            dataType: 'json',
            url: function (params) {
                var url = BASE_API_URL + '/city/' + selectedCountryCode
                return url;
            },
            processResults: function (data) {
                var options = [];
                options.push({ id: null, text: "-- Please Select Ciy --", text2: "" });
                if (data) {
                    $.each(data, function (index, row) {
                        options.push({ id: row.name, text: row.name, text2: row.id });
                    });
                }
                return {
                    results: options,
                    more: false
                };
            }
        }
    });

    $('#inputCity').on('select2:select', function (e) {
        var data = e.params.data;
        selectedCityName = data.id;
        var url = BASE_API_URL + '/weather/query/' + selectedCountryCode + '/' + selectedCityName;
        $.getJSON(url, function (data) {
            console.log(data);
            $("#spanCountryId").html(data.location.countryId);
            $("#spanCityName").html(data.location.cityName);
            $("#spanLat").html(data.location.lat);
            $("#spanLon").html(data.location.lon);

            $("#spanTime").html(moment.unix(data.time).format("MMMM Do YYYY, h:mm:ss a"));
            $("#spanWindSpeed").html(data.wind.speed);
            $("#spanWindDeg").html(data.wind.deg);
            $("#spanWindGust").html(data.wind.gust);

            $("#spanVisibility").html(data.visibility);
            $("#spanSkyMain").html(data.skyConditions.main);
            $("#spanSkyDesc").html(data.skyConditions.description);

            $("#spanTempFahr").html(data.temperatureFahrenheit);
            $("#spanTempCelc").html(data.temperatureCelcius);
            $("#spanDewPoint").html(data.dewPoint);
            $("#spanRelativeHumidity").html(data.relativeHumidity);
            $("#spanPressure").html(data.pressure);

            $("#divDetailWeather").show();
        })
    });
});