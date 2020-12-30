using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;
public class GameSystem : MonoBehaviour
{
    public bool gameEnded = false;
    public int speedScore;
    public int playerScore;
    public string character;

    private static GameSystem m_Instance;
    public static GameSystem Instance
    {
        get
        {
            return m_Instance;
        }
    }
    public GameObject deathScreen;
    public GameObject gameScreen;

    public GameObject[] obstacleSounds;

    public int stacksInARow = 0;
    public int giantsInARow = 0;

    public ArrayList obstacleTypes = new ArrayList();
    public ArrayList obstacleNames = new ArrayList();
    public ArrayList obstacleGiantNames = new ArrayList();

    public string[] easyChallenges;
    public string[] mediumChallenges;
    public string[] hardChallenges;
    public string[] veryHardChallenges;

    private void Awake()
    {
        m_Instance = this;
        InvokeRepeating("UpdateSpeedScore", 0, 3);
        Application.targetFrameRate = 60;
        if(!CharacterSystem.Instance.replay)
        {
            gameObject.SetActive(false);
        }
        else
        {
            CharacterSystem.Instance.replay = false;
        }
    }

    private void OnDestroy()
    {
        m_Instance = null;
    }
    private void Start()
    {
    }

    private void Update()
    {
    }

    private void UpdateSpeedScore()
    {
        if (!gameEnded)
        {
            speedScore++;
            if (speedScore >= 30)
            {
                CancelInvoke("UpdateSpeedScore");
            }
        }
        else
        {
            speedScore = 10;
        }
    }
}