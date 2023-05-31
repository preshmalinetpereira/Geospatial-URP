using System;
using Google.XR.ARCoreExtensions;
using Google.XR.ARCoreExtensions.Samples.Geospatial;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using TMPro;

namespace Umass_GeospatialAR
{
    public class InteractionManager : MonoBehaviour
    {
        const string APIKey = "AIzaSyAYJdjAF3hQivNs8jZklkAJs03CtPZVc1I";
        //Tracking information using GeospatialAPI
        [SerializeField] AREarthManager EarthManager;
        //Used for initializing ARCore and GeospatialAPI
        [SerializeField] VpsInitializer Initializer;
        //UI for displaying the tracking result
        [SerializeField] Text OutputText;
        //Heading accuracy (change the value in the Inspector)
        [SerializeField] double HeadingThreshold = 25;
        //Horizontal position accuracy (change the value in the Inspector)
        [SerializeField] double HorizontalThreshold = 20;
        public GameObject ResultsPanel;
        public TextMeshProUGUI ResultsText;
        public void GetNearbyRestaurants()
        {
            //Return if initialization failed or tracking is not available
            if (!Initializer.IsReady || EarthManager.EarthTrackingState != TrackingState.Tracking)
            {
                return;
            }
            //Tracking status to be displayed
            string status = "";
            //Get the tracking result
            GeospatialPose pose = EarthManager.CameraGeospatialPose;
            //The case where the tracking accuracy is worse than the threshold (the value is large)
            if (pose.OrientationYawAccuracy > HeadingThreshold ||
                 pose.HorizontalAccuracy > HorizontalThreshold)
            {
                status = "Low Tracking Accuracy： Please look arround.";
            }
            else //The case where the tracking accuracy is better than the threshold (the value is small)
            {
                status = "High Tracking Accuracy";
            }
            StartCoroutine(FetchData(pose));
        }

        public IEnumerator FetchData(GeospatialPose pose)
        {



            string URL = string.Format("https://maps.googleapis.com/maps/api/place/nearbysearch/json?keyword=restaurant" +
                                "&location={0},{1}" +
                                "&radius=1500" +
                                "&key={2}", pose.Latitude, pose.Longitude, APIKey);
            using (UnityWebRequest request = UnityWebRequest.Get(URL))
            {
                yield return request.SendWebRequest();
                if (request.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.Log(request.error);
                }
                else
                {
                    PlacesApiQueryResponse places = new PlacesApiQueryResponse();
                    var result = JsonUtility.FromJson<PlacesApiQueryResponse>(request.downloadHandler.text);
                    Debug.Log(request.downloadHandler.text);
                    yield return result.results;
                    DisplayPanel(result.results);
                }
                
            }
        }

        
        public void DisplayPanel(List<Result> places)
        {
            if (ResultsPanel != null)
            {
                bool isActive = ResultsPanel.activeSelf;
                ResultsPanel.SetActive(!isActive);
                string placesString = "";
                foreach (var place in places)
                {
                    placesString += place.name + ":" + place.rating.ToString() + "\n";
                }
                ResultsText.SetText(placesString);
            }
        }
    }

}