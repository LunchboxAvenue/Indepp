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
            //Labs: '<div class="MapDiv">',
            icon: {
                path: fontawesome.markers.COFFEE,
                scale: 0.5,
                strokeWeight: 0.2,
                strokeColor: 'black',
                strokeOpacity: 1,
                fillColor: '#696969',
                fillOpacity: 0.85,
            },
        });
        markersArray.push(marker);

        //Just have a link to the site instead of all labs in the site
        //marker.Labs += placesData[i].Name + '</div>';

        if (placesData[i].Category == "Coffee") { marker.icon.path = fontawesome.markers.COFFEE }
        if (placesData[i].Category == "Food") { marker.icon.path = fontawesome.markers.CUTLERY }
        if (placesData[i].Category == "Farms") { marker.icon.path = fontawesome.markers.PAGELINES }
        if (placesData[i].Category == "CraftShops") { marker.icon.path = fontawesome.markers.COG }

        /*if (sitesData[i].TrialTrust) { marker.icon = iconBase + 'schools_maps.png'; }
        if (typeof (thisLab) != "undefined" && thisLab == sitesData[i].ID) { marker.icon = iconBase + 'schools_maps.png'; }*/

        /*google.maps.event.addListener(marker, 'click', function () {
            // When clicked, open an Info Window  
            var info = new google.maps.InfoWindow({
                content: this.Labs,
                position: this.position
            });

            info.open(map);
            setTimeout(function () { $('.MapDiv').parent().parent().css('z-index', 10000) }, 500);
        });*/
    }
}

function clearMapPoints() {
    if (markersArray) {
        for (i in markersArray) {
            markersArray[i].setMap(null);
        }
    }
}