function drawMap(defaultLat, defaultLon) {
    var latlng = new google.maps.LatLng(defaultLat, defaultLon);
    var myOptions = {
        zoom: 1,
        center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        styles: mapStyles
    };
    map = new google.maps.Map(document.getElementById(MapCanvas), myOptions);

    /*google.maps.event.addListener(map, 'zoom_changed', function () {
        apiKey();
    });*/
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
        var iconBase = 'https://maps.google.com/mapfiles/kml/shapes/';
        var marker = new google.maps.Marker({
            position: point,
            map: map,
            title: placesData[i].Name,
            //Labs: '<div class="MapDiv">',
            icon: iconBase + 'info-i_maps.png'
        });
        markersArray.push(marker);

        //Just have a link to the site instead of all labs in the site
        //marker.Labs += placesData[i].Name + '</div>';
        //marker.icon = iconBase + 'schools_maps.png';

        /*if (sitesData[i].NPExTrust) { marker.icon = iconBase + 'schools_maps.png'; }
        if (sitesData[i].TrialTrust) { marker.icon = iconBase + 'schools_maps.png'; }
        if (typeof (thisLab) != "undefined" && thisLab == sitesData[i].ID) { marker.icon = iconBase + 'schools_maps.png'; }

        google.maps.event.addListener(marker, 'click', function () {
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