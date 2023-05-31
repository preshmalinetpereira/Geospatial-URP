using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Umass_GeospatialAR
{
    [System.Serializable]
    public class Location
    {
        public double lat ;
        public double lng ;

        public Location(double lat, double lng)
        {
            this.lat = lat;
            this.lng = lng;
        }
    }

    [System.Serializable]
    public class Geometry
    {
        public Location location ;

        public Geometry(Location location)
        {
            this.location = location;
        }
    }

    [System.Serializable]
    public class OpeningHours
    {
        public bool open_now ;
        public List<object> weekday_text ;

        public OpeningHours(bool open_now, List<object> weekday_text)
        {
            this.open_now = open_now;
            this.weekday_text = weekday_text;
        }

    }

    [System.Serializable]
    public class Photo
    {
        public int height ;
        public List<string> html_attributions ;
        public string photo_reference ;
        public int width ;

        public Photo(int height, List<string> html_attributions, string photo_reference, int width)
        {
            this.height = height;
            this.html_attributions = html_attributions;
            this.photo_reference = photo_reference;
            this.width = width;
        }
    }

    [System.Serializable]
    public class Result
    {
        public Geometry geometry ;
        public string icon ;
        public string id ;
        public string name ;
        public OpeningHours opening_hours ;
        public List<Photo> photos ;
        public string place_id ;
        public double rating ;
        public string reference ;
        public string scope ;
        public List<string> types ;
        public string vicinity ;

        public Result(Geometry geometry, string icon, string id, string name, OpeningHours opening_hours, List<Photo> photos, string place_id, double rating, string reference, string scope, List<string> types, string vicinity)
        {
            this.geometry = geometry;
            this.icon = icon;
            this.id = id;
            this.name = name;
            this.opening_hours = opening_hours;
            this.photos = photos;
            this.place_id = place_id;
            this.rating = rating;
            this.reference = reference;
            this.scope = scope;
            this.types = types;
            this.vicinity = vicinity;
        }
    }


    [System.Serializable]
    public class PlacesApiQueryResponse
    {
        public List<object> html_attributions ;
        public List<Result> results ;
        public string status ;

    }

}
