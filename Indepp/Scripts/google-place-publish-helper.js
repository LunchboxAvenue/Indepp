$(function() {
    GetPlaceDetails();
})

function GetPlaceDetails() {
    var obj = $('<div>').appendTo('body');
    var service = new google.maps.places.PlacesService(obj.get(0));

    var googlePlaceId = $('#GooglePlaceId').val();

    if (googlePlaceId) {
        service.getDetails({
            placeId: googlePlaceId
        }, function (place, status) {
            if (status === google.maps.places.PlacesServiceStatus.OK) {
                var types = place.types.join(", ");
                var publishHelper = "Google suggest following types for this place: <br /><strong>" + types + '</strong><br />';
                $('#googlePlacePublishHelper').append(publishHelper);
            }
        });
    }
}