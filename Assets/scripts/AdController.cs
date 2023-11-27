using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

public class AdController : MonoBehaviour
{
    public BannerView bannerView;

    public static AdController Current;

    // Start is called before the first frame update
    public void InitializeAds()
    {
        Current = this;
        MobileAds.Initialize(initStatus => { });
        this.RequestBanner();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    
    private void RequestBanner()
    {
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-8700622788118475/5133647882";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }


}
