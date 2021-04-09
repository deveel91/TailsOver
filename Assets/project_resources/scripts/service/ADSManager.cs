using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class ADSManager : Singleton<ADSManager>, IUnityAdsListener
{
    private string gameid = "3528536";
    private string placementVideo = "video";
    private string placementRewarded = "rewardedVideo";
    private string placementBanner = "banner";

    // Start is called before the first frame update
    void Start()
    {
        Advertisement.AddListener(this);
#if UNITY_EDITOR
        Advertisement.Initialize(gameid, true);     //testmode = true
#else
        Advertisement.Initialize(gameid, false);    //testmode = false
#endif
    }

    public void ShowVideo(){
        if(Advertisement.IsReady(placementVideo))
            Advertisement.Show(placementVideo);
    }

    public void ShowRewardedVideo(){
        if(Advertisement.IsReady(placementRewarded))
            Advertisement.Show(placementRewarded);
    }

    public void ShowBanner(){
        if (Advertisement.IsReady(placementBanner))
        {
            Advertisement.Banner.Show(placementBanner);
            Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        }
    }

    public void HideBanner()
    {
        if (Advertisement.Banner.isLoaded)
        {
            Advertisement.Banner.Hide();
        }
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult){
        if(placementId == placementRewarded && showResult == ShowResult.Finished){
            GameManager.Instance.Coins += 20;
            GameManager.Instance.SaveData();
        }
    }

    public void OnUnityAdsReady(string placementId){
        Debug.Log("Ready Ads:" + placementId);
        //if(placementId == placementVideo && showVideo == true){
        //    Advertisement.Show(placementVideo);
        //    showVideo = false;
        //}
        //else if(placementId == placementRewarded && showRewarded == true){
        //    Advertisement.Show(placementRewarded);
        //    showRewarded = false;
        //}
        if(placementId == placementBanner){
            Advertisement.Banner.Show(placementBanner);
        }
    }

    public void OnUnityAdsDidError(string message){

    }

    public void OnUnityAdsDidStart(string placementId){

    }
}
