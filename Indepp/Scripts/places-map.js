var map;
var placesData;
var markersArray = [];
//var imagePath = '/content/images/';
var visitor_lat;
var visitor_lon;
var geocoder;
var ZoomLevel = 8;
var MapCanvas = 'placesMap';

$(function () {
    drawMap(54.5, -3.5);
    GetPlaceLocations();
    geocoder = new google.maps.Geocoder();
    map.setZoom(5);

    /*
    $('#FindLocation').click(function () {
        WhereAreYou();
    });

    $('#location').keyup(function (e) {
        if (e.keyCode == 13) {
            WhereAreYou();
        }
    });

    $('#CentreUK').click(function () {
        var latlng = new google.maps.LatLng(54.5, -3.5);
        map.setCenter(latlng);
        map.setZoom(6);
    });
    $('#Npex').click(function () {
        GetLabLocations();
    });
    $('#Trial').click(function () {
        GetLabLocations();
    });
    $('#NonNpex').click(function () {
        GetLabLocations();
    });
    */
});

function GetPlaceLocations() {
    clearMapPoints();

    var inputs = {
        //showNpex: $('#Npex').is(':checked'),
        //showTrial: $('#Trial').is(':checked'),
        //showNonNpex: $('#NonNpex').is(':checked')
        showCoffee: true,
        showFood: true,
        showFarm: true,
        showCraftShop: true
    };

    /*if (!$('#Trial').is(':checked'))
        $('.connecting').css('display', 'none');
    else
        $('.connecting').css('display', 'block');

    if (!$('#Npex').is(':checked'))
        $('.connected').css('display', 'none');
    else
        $('.connected').css('display', 'block');
    */

    $.post("/Home/GetPlaceLocations", inputs, function (data) {
        //data has loaded
        placesData = data;
        DrawMapPoints();
        //$('#SpinnerDiv').html('');
    });
    //data is loading
    //$('#SpinnerDiv').html('<img src="/content/images/ajax-loader3.jpg"/>');
}