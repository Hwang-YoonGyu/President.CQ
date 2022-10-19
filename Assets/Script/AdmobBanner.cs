using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdmobBanner : MonoBehaviour
{
    public bool isTestMode = true; //출시할 때 false하면 될듯
    public static AdmobBanner instance;

    BannerView bannerView;

    public static AdmobBanner Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);

        RequestBanner();
    }

    private void RequestBanner()
    {
        string adUnitTestId = "ca-app-pub-3940256099942544/6300978111";
        string adUnitId = "ca-app-pub-2905952082881818/9936773928";
        // Create a 320x50 banner at the top of the screen.
        //AdSize adSize = new AdSize(468, 60);
        //BannerView bannerView = new BannerView(isTestMode ? adUnitTestId : adUnitId, adSize, AdPosition.TopRight);
        BannerView bannerView = new BannerView(isTestMode ? adUnitTestId : adUnitId, AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth/3), AdPosition.Bottom);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.

        bannerView.LoadAd(request);
    }

    public void Load_ad()
    {
        Debug.Log("load_ad");
        bannerView.Show();
    }

    public void Exit_ad()
    {
        Debug.Log("exit_ad");
        bannerView.Hide();
    }
}
