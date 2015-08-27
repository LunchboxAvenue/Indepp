function drawMap(defaultLat, defaultLon) {
    var latlng = new google.maps.LatLng(defaultLat, defaultLon);
    var myOptions = {
        zoom: 1,
        center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP
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

/*
function DrawMapRoutes() {
    for (i in sitesData) {
        for (l in sitesData[i].Routes) {
                var pts = [];
                pts[0] = new google.maps.LatLng(parseFloat(sitesData[i].Latitude),parseFloat(sitesData[i].Longitude));
                pts[1] = new google.maps.LatLng(parseFloat(sitesData[i].Routes[l].Latitude),parseFloat(sitesData[i].Routes[l].Longitude));
                var route = new google.maps.Polyline({
                    path: pts,
                    strokeColor: "blue",
                    strokeOpacity: 0.5,
                    strokeWeight: 4
                });
                arrowHead(pts);
                route.setMap(map);
        }
    }
}

function WhereAreYou() {
    geocoder.geocode({ 'address': $('#location').val() }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            $('#results').html('');
            var a = results[0].geometry.location;
            map.setCenter(a);
            map.setZoom(10);
            $('#MapError').html('');
        }
        else {
            $('#MapError').html('Location not found');
        }
    });
}

function apiKey() {
    if (map.zoom > 6) {
        if (Markera == null) {
            var point = new google.maps.LatLng(37.242902, -115.819359);
            Markera = new google.maps.Marker({
                position: point,
                map: map,
                icon: imagePath + "purple-dot.png",
                title: "Area 51",
                Labs: '<div class="MapDiv" style="height:50px !important">Performs multiple tests on aliens</div>'
            });
            google.maps.event.addListener(Markera, 'click', function () {
                // When clicked, open an Info Window  
                var info = new google.maps.InfoWindow({
                    content: this.Labs,
                    position: this.position
                });
                info.open(map);
                setTimeout(function () { $('.MapDiv').parent().parent().css('z-index', 10000) }, 500);
            });
        }
        else
            Markera.setMap(map);
    }
    else
        if (Markera != null)
            Markera.setMap(null);
}

// === Returns the bearing in degrees between two points. ===
// North = 0, East = 90, South = 180, West = 270.
function bearing(from, to) {
    var latDiff = to.lat() - from.lat();
    var lngDiff = to.lng() - from.lng();
    var angle = Math.atan((to.lng() - from.lng()) / (to.lat() - from.lat())) * (180.0 / Math.PI) + 360;
    if (latDiff < 0) {
        angle -= 180;
    }
    return angle;
}

function arrowHead(points) {
    // == obtain the bearing between the last two points
    var from = points[0];
    var to = points[1];
    var midPoint = new google.maps.LatLng((from.lat() + to.lat()) / 2.0, (from.lng() + to.lng()) / 2.0);
    var dir = bearing(from, to);
    // == round it to a multiple of 3 and cast out 120s
    var dir = Math.round(dir / 3) * 3;
    while (dir >= 120) { dir -= 120; }
    // == use the corresponding triangle marker
    var arrowURL = "http://www.google.com/intl/en_ALL/mapfiles/dir_" + dir + ".png";
    var arrowImage = new google.maps.MarkerImage(arrowURL,
                                            new google.maps.Size(24, 24),
                                            new google.maps.Point(0, 0),
                                            new google.maps.Point(12, 12));
    createMarker(to, arrowImage);
    createMarker(midPoint, arrowImage);
}*/