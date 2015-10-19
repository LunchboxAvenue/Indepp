var componentForm = {
    street_number: 'Address_Address1',
    route: 'Address_Address2',
    locality: 'Address_City',
    postal_town: 'Address_City',
    country: 'Address_Country',
    postal_code: 'Address_PostCode'
};

var DayEnum = {
    0: "Sunday",
    1: "Monday",
    2: "Tuesday",
    3: "Wednesday",
    4: "Thursday",
    5: "Friday",
    6: "Saturday"
};

var days = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];

function initialize() {
    var input = document.getElementById('googleSearchInput');
    var autocomplete = new google.maps.places.Autocomplete(input);

    google.maps.event.addListener(autocomplete, 'place_changed', function () {
        var place = autocomplete.getPlace();

        resetAll();

        $('#Name').val(place.name);
        $('#Website').val(place.website);
        $('#Telephone').val(place.international_phone_number);

        for (var i = 0; i < place.address_components.length; i++) {
            var addressType = place.address_components[i].types[0];
            if (componentForm[addressType]) {
                var val = place.address_components[i].long_name;
                document.getElementById(componentForm[addressType]).value = val;
            }
        }

        $('#Address_Latitude').val(place.geometry.location.lat());
        $('#Address_Longitude').val(place.geometry.location.lng());

        if (place.opening_hours) {
            for (var i = 0; i < place.opening_hours.periods.length; i++) {
                var opening = place.opening_hours.periods[i].open;
                var closing = place.opening_hours.periods[i].close;

                var openTime = opening.time.substr(0, 2) + ':' + opening.time.substr(2, 2);
                var closeTime = closing.time.substr(0, 2) + ':' + closing.time.substr(2, 2);

                document.getElementById(DayEnum[opening.day] + '.Open').value = openTime;
                document.getElementById(DayEnum[closing.day] + '.Close').value = closeTime;
            }
        }
    });
}

function resetAll() {
    $('#Name, #Website, #Telephone, #Address_Address1, #Address_Address2, #Address_City, #Address_Country, #Address_PostCode, #Address_Latitude, #Address_Longitude').val("");
    
    for (var i = 0; i < days.length; i++) {
        document.getElementById(days[i] + '.Open').value = "";
        document.getElementById(days[i] + '.Close').value = "";
    };
}

// Run the initialize function when the window has finished loading.
google.maps.event.addDomListener(window, 'load', initialize);

$('#resetAllButton').click(function () {
    resetAll();
});