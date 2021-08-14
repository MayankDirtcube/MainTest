/*
* Copyright (c) DeckOfDev
* Email deckofdev@gmail.com
*/

using UnityEngine;
#if ADMOB
using GoogleMobileAds.Api;
#endif
public class AdBanner : MonoBehaviour
{
#if ADMOB
    private string adUnitId;
    private AdRequest request;
    private static BannerView bannerView;

    public void Start()
    {
        if(PlayerPrefs.GetInt("DisableAds") == 0)
        {
            if(!Data.properties.ads.testMode)
            {
                #if UNITY_ANDROID
                    string appId = Data.properties.ads.androidAppID;
                #elif UNITY_IPHONE
                    string appId = Data.properties.ads.iphoneAppID;
                #else
                    string appId = "unexpected_platform";
                #endif

                // Initialize the Google Mobile Ads SDK.
                MobileAds.Initialize(appId);

                this.RequestBanner();            
            }
            else
            {
                #if UNITY_ANDROID
                    string appId = "ca-app-pub-3940256099942544/6300978111";
                #elif UNITY_IPHONE
                    string appId = "ca-app-pub-3940256099942544/2934735716";
                #else
                    string appId = "unexpected_platform";
                #endif            

                // Initialize the Google Mobile Ads SDK.
                MobileAds.Initialize(appId);

                this.RequestBanner();
            }
        }
    }

    private void RequestBanner()
    {
        if(!Data.properties.ads.testMode)
        {        
            #if UNITY_ANDROID
                string adUnitId = Data.properties.ads.androidUnitID;
            #elif UNITY_IPHONE
                string adUnitId = Data.properties.ads.iphoneUnitID;
            #else
                string adUnitId = "unexpected_platform";
            #endif

            // Create an empty ad request.
            request = new AdRequest.Builder().Build();

            // Create a 320x50 banner at the top of the screen.
            bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);

            // Load the banner with the request.
            bannerView.LoadAd(request);                
        }
        else
        {
            #if UNITY_ANDROID
                string adUnitId = "ca-app-pub-3940256099942544/6300978111";
            #elif UNITY_IPHONE
                string adUnitId = "ca-app-pub-3940256099942544/2934735716";
            #else
                string adUnitId = "unexpected_platform";
            #endif

            // Create an empty ad request.
            request = new AdRequest.Builder().Build();

            // Create a 320x50 banner at the top of the screen.
            bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);

            // Load the banner with the request.
            bannerView.LoadAd(request);    
        }    
    }

    public static void DestroyBanner()
    {
        bannerView.Destroy();
        bannerView.Hide();
        Debug.Log("Ad banner was destroyed.");
    }
#endif
}
