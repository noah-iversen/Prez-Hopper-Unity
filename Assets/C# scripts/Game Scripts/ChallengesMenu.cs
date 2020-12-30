using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System;

public class ChallengesMenu : MonoBehaviour
{

    public GameObject challengePoints;
    private int currentDifficulty = 0;
    public GameObject[] difficulties;

    private RewardedAd rewardedAd;

    private void Start()
    {
        CreateAndLoadRewardedAd();
    }

    public void CreateAndLoadRewardedAd()
    {
#if UNITY_ANDROID
            string adUnitId = "ca-app-pub-4798898525992407/9753997715";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-4798898525992407/8440916043";
#else
            string adUnitId = "unexpected_platform";
#endif

        this.rewardedAd = new RewardedAd(adUnitId);

        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        PlayerPrefs.SetInt("Challenge-Points", PlayerPrefs.GetInt("Challenge-Points") + 1);
        challengePoints.GetComponent<Text>().text = PlayerPrefs.GetInt("Challenge-Points").ToString();
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        this.CreateAndLoadRewardedAd();
    }


    public void WatchVideo()
    {
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
    }

    public void LeftButton()
    {

        GameSystem.Instance.deathScreen.GetComponent<AudioSource>().Play();
        if (currentDifficulty > 0 && currentDifficulty <= 3)
        {

            currentDifficulty--;
        }
        else
        {
            currentDifficulty = 3;
        }
        UpdateDifficultyLabel();
    }

    public void RightButton()
    {
        GameSystem.Instance.deathScreen.GetComponent<AudioSource>().Play();
        if (currentDifficulty >= 0 && currentDifficulty < 3)
        {
            currentDifficulty++;
        }
        else
        {
            currentDifficulty = 0;
        }
        UpdateDifficultyLabel();

    }
    private void UpdateDifficultyLabel()
    {
        for (int i = 0; i < difficulties.Length; i++)
        {
            if (i != currentDifficulty)
            {
                difficulties[i].SetActive(false);
            }
            else
            {
                difficulties[i].SetActive(true);
            }
        }
    }
}