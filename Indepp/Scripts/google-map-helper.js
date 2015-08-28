function drawMap(defaultLat, defaultLon) {
    var latlng = new google.maps.LatLng(defaultLat, defaultLon);
    var myOptions = {
        zoom: 1,
        center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        styles: mapStyles
    };
    map = new google.maps.Map(document.getElementById(MapCanvas), myOptions);
}

function createMarker(point, icon) {
    var marker = new google.maps.Marker({
        position: point,
        map: map,
        Icon: icon
    });
}

function DrawMapPoints() {
    for (i in placesData) {
        var point = new google.maps.LatLng(placesData[i].Latitude, placesData[i].Longitude);
        var marker = new google.maps.Marker({
            position: point,
            map: map,
            title: placesData[i].Name,
            icon: {
                path: fontawesome.markers.COFFEE,
                scale: 0.5,
                strokeWeight: 1,
                strokeColor: 'black',
                strokeOpacity: 1,
                fillColor: '#696969',
                fillOpacity: 0.85,
                anchor: new google.maps.Point(30, -30)
            }
        });

        marker.place = '<div class="placeInfoDiv">' + placesData[i].Name + '</div>';

        if (placesData[i].Category == "Coffee") { marker.icon.path = fontawesome.markers.COFFEE }
        if (placesData[i].Category == "Food") { marker.icon.path = fontawesome.markers.CUTLERY }
        if (placesData[i].Category == "Farms") { marker.icon.path = fontawesome.markers.PAGELINES }
        if (placesData[i].Category == "CraftShops") { marker.icon.path = fontawesome.markers.GEAR }

        markersArray.push(marker);

        google.maps.event.addListener(marker, 'click', function () {
            var infoWindow = new google.maps.InfoWindow({
                content: this.place,
                position: this.position
            });

            infoWindow.open(map);
            setTimeout(function () { infoWindow.close(); }, 15000);
        });
    }
}

function clearMapPoints() {
    if (markersArray) {
        for (i in markersArray) {
            markersArray[i].setMap(null);
        }
    }
}