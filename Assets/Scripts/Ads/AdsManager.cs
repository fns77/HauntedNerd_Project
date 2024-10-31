using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gley.MobileAds;
using UnityEngine.UI;
using UnityEngine.Events;

public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance {  get; private set; }

    
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            API.Initialize();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool IntersialAvailable()
    {
        return API.IsInterstitialAvailable();
    }


    public void ShowInterstitial(UnityAction _event)
    {
        API.ShowInterstitial(_event);
    }

    public bool RewardAvailable()
    {
        return API.IsRewardedVideoAvailable();
    }

    public void ShowBanner()
    {
        API.ShowBanner(BannerPosition.Bottom ,BannerType.Adaptive);
    }

    public void HideBanner()
    {
        API.HideBanner();
    }   

    public void RewardVideo( UnityAction<bool> _events) 
    {
        API.ShowRewardedVideo(_events);
    }


}