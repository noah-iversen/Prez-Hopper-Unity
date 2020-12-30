using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsChallenges : MonoBehaviour
{
    private Dictionary<int, int> pointChallenges = new Dictionary<int, int>();
    private Dictionary<int, int> totalPointChallenges = new Dictionary<int, int>();

    private string recentlyScored = "";

    public GameObject completeSound;
    public GameObject challengeNotification;

    private void Awake()
    {
        InitPointChallenges();
        InitTotalPointChallenges();
    }


    public void CheckForChallenges()
    {
        int score = GameSystem.Instance.playerScore;
        int totalPoints = PlayerPrefs.GetInt("Total-Points");
        foreach(int pointIndex in pointChallenges.Keys)
        {
            if(score >= pointIndex)
            {
                if(CanScorePointChallenge(pointIndex))
                {
                    ScorePointChallenge(pointIndex);
                    AddPoints(pointChallenges[pointIndex]);
                }
            }
        }

        foreach(int totalPointIndex in totalPointChallenges.Keys)
        {
            if(totalPoints >= totalPointIndex)
            {
                if(CanScoreTotalPointChallenge(totalPointIndex))
                {
                    ScoreTotalPointChallenge(totalPointIndex);
                    AddPoints(totalPointChallenges[totalPointIndex]);
                }
            }
        }
    }

    private void InitPointChallenges()
    {
        pointChallenges.Add(25, 1);
        pointChallenges.Add(50, 1);
        pointChallenges.Add(75, 2);
        pointChallenges.Add(100, 2);
        pointChallenges.Add(125, 3);
        pointChallenges.Add(150, 5);
        pointChallenges.Add(200, 10);
    }
    private void InitTotalPointChallenges()
    {
        totalPointChallenges.Add(100, 1);
        totalPointChallenges.Add(250, 1);
        totalPointChallenges.Add(500, 2);
        totalPointChallenges.Add(750, 2);
        totalPointChallenges.Add(1000, 3);
        totalPointChallenges.Add(1500, 3);
        totalPointChallenges.Add(2000, 5);
        totalPointChallenges.Add(3000, 5);
    }

    private void AddPoints(int points)
    {
        PlayerPrefs.SetInt("Challenge-Points", PlayerPrefs.GetInt("Challenge-Points") + points);
        completeSound.GetComponent<AudioSource>().Play();
        string[][] allChallenges = { GameSystem.Instance.easyChallenges, GameSystem.Instance.mediumChallenges,
            GameSystem.Instance.hardChallenges, GameSystem.Instance.veryHardChallenges };
        string[] difficulties = { "Easy", "Medium", "Hard", "Very Hard" };
        for (int d = 0; d < allChallenges.Length; d++)
        {
            for (int c = 0; c < allChallenges[d].Length; c++)
            {
                if (recentlyScored.Equals(allChallenges[d][c]))
                {
                    challengeNotification.GetComponent<Animator>().SetTrigger(difficulties[d] + " Challenge");
                }

            }
        }
    }


    private bool CanScorePointChallenge(int score)
    {
        return PlayerPrefs.GetInt("Point-Challenge-" + score) == 0;
    }

    private void ScorePointChallenge(int score)
    {
        PlayerPrefs.SetInt("Point-Challenge-" + score, 1);
        recentlyScored = "Point-Challenge-" + score;
    }

    private bool CanScoreTotalPointChallenge(int score)
    {
        return PlayerPrefs.GetInt("Total-Point-Challenge-" + score) == 0;
    }
    private void ScoreTotalPointChallenge(int score)
    {
        PlayerPrefs.SetInt("Total-Point-Challenge-" + score, 1);
        recentlyScored = "Total-Point-Challenge-" + score;
    }
}