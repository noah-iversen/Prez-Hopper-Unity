using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public class StartMenu : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }    // Start is called before the first frame update
    void Start()
    {
        MobileAds.Initialize(initStatus =>
        {
        });
    }    // Update is called once per frame
    void Update()
    {
    }

    public void PlayGame()
    {
        CharacterSystem.Instance.gameObject.SetActive(true);
        CharacterSystem.Instance.GetComponent<AudioSource>().Play();
        CharacterSystem.Instance.RequestBanner();
        CharacterSystem.Instance.RequestInterstitial();
        transform.parent.gameObject.SetActive(false);
        SceneManager.UnloadSceneAsync(0);
    }
}