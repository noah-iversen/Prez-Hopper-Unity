using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public class CharacterSystem : MonoBehaviour
{
    private BannerView bannerView;
    private static CharacterSystem m_Instance;
    public static CharacterSystem Instance
    {
        get
        {
            return m_Instance;
        }
    }

    public GameObject unlockablesScreen;
    public GameObject mainScreen;
    public GameObject dialogPopup;
    public GameObject lowPointsPopup;

    public ArrayList unlockedCharacters = new ArrayList();

    public int selectedCharacter = -1;
    public int requestedCharacter = -1;

    public int characterCost = 10;
    public GameObject challengePoints;

    public InterstitialAd interstitial;

    public bool replay = false;

    public void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-4798898525992407/3475474954";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-4798898525992407/9638061260";
#else
        string adUnitId = "unexpected_platform";
#endif        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }

    public void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-4798898525992407/6840004895";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-4798898525992407/8769818252";
#else
        string adUnitId = "unexpected_platform";
#endif

        if (interstitial != null)
        {
            interstitial.Destroy();
        }
        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }

    void Awake()    { 
        m_Instance = this;
        Application.targetFrameRate = 60;
        gameObject.SetActive(false);
        SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
    }

    public void ClearSelection()
    {
        foreach (GameObject button in GameObject.FindGameObjectsWithTag("CharacterSelect"))
        {
            Color color = button.GetComponent<SpriteRenderer>().color;
            color.a = 1.0f;
            button.GetComponent<SpriteRenderer>().color = color;
        }
        PlayerPrefs.SetInt("Selection", -1); selectedCharacter = -1; unlockablesScreen.SetActive(false); mainScreen.SetActive(true);
    }

    public void RequestCharacter(int selection)
    {
        mainScreen.GetComponentInParent<AudioSource>().Play();
        if (PlayerPrefs.GetInt("Challenge-Points") >= characterCost)
        {
            requestedCharacter = selection;
            dialogPopup.SetActive(true);
            unlockablesScreen.SetActive(false);
        }
        else
        {
            unlockablesScreen.SetActive(false);
            lowPointsPopup.SetActive(true);
        }
    }

    void OnDestroy()
    {
        m_Instance = null;
    }
    void OnGui()
    {
        // common GUI code goes here
    }
}