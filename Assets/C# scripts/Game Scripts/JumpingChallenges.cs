using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpingChallenges : MonoBehaviour
{
    private Dictionary<int, int> totalDoubleStacks = new Dictionary<int, int>();
    private Dictionary<int, int> totalGiants = new Dictionary<int, int>();
    private Dictionary<int, int> giantsInARow = new Dictionary<int, int>();
    private Dictionary<int, int> doubleStacksInARow = new Dictionary<int, int>();

    private string recentlyScored = "";

    public GameObject completeSound;
    public GameObject challengeNotification;

    private void Awake()
    {
        InitTotalDoubleStacks();
        InitTotalGiants();
        InitGiantsInARow();
        InitDoubleStacksInARow();
    }

    public void CheckForChallenges()
    {
        int stackCounter = PlayerPrefs.GetInt("Total-Double-Stacks");
        int giantCounter = PlayerPrefs.GetInt("Total-Giants");
        int giantsInARowCounter = GameSystem.Instance.giantsInARow;
        int stacksInARowCounter = GameSystem.Instance.stacksInARow;

        if (totalDoubleStacks.ContainsKey(stackCounter) && CanScoreTotalDoubleStackChallenge(stackCounter))
        {
            ScoreTotalDoubleStackChallenge(stackCounter);
            AddPoints(totalDoubleStacks[stackCounter]);
        }

        if (totalGiants.ContainsKey(giantCounter) && CanScoreTotalGiantChallenge(giantCounter))
        {
            ScoreTotalGiantChallenge(giantCounter);
            AddPoints(totalGiants[giantCounter]);
        }

        if (giantsInARow.ContainsKey(giantsInARowCounter) && CanScoreGiantsInARowChallenge(giantsInARowCounter))
        {
            ScoreGiantsInARowChallenge(giantsInARowCounter);
            AddPoints(giantsInARow[giantsInARowCounter]);
        }
        if (doubleStacksInARow.ContainsKey(stacksInARowCounter) && CanScoreDoubleStacksInARowChallenge(stacksInARowCounter))
        {
            ScoreDoubleStacksInARowChallenge(stacksInARowCounter);
            AddPoints(doubleStacksInARow[stacksInARowCounter]);
        }
        /*if (GameSystem.Instance.obstacleTypes.Count == 4 && CanScoreObstacleTypeChallenge())
        {
            ScoreObstacleTypeChallenge();
            AddPoints(2);
        }
        */
        if (GameSystem.Instance.obstacleNames.Count >= 13 && CanScoreEveryCharacterChallenge())
        {
            ScoreEveryCharacterChallenge();
            AddPoints(2);
        }
        if (GameSystem.Instance.obstacleGiantNames.Count >= 13 && CanScoreEveryGiantChallenge())
        {
            ScoreEveryGiantChallenge();
            AddPoints(10);
        }
    }
    private void InitTotalDoubleStacks()
    {
        totalDoubleStacks.Add(10, 1);
        totalDoubleStacks.Add(25, 1);
        totalDoubleStacks.Add(50, 2);
        totalDoubleStacks.Add(100, 2);

    }
    private void InitTotalGiants()
    {
        totalGiants.Add(1, 2);
        totalGiants.Add(25, 3);
        totalGiants.Add(50, 3);
    }

    private void InitDoubleStacksInARow()
    {
        doubleStacksInARow.Add(2, 2);
        doubleStacksInARow.Add(3, 3);
    }

    private void InitGiantsInARow()
    {
        giantsInARow.Add(2, 2);
    }

    private void AddPoints(int points)
    {
        PlayerPrefs.SetInt("Challenge-Points", PlayerPrefs.GetInt("Challenge-Points") + points); 
        completeSound.GetComponent<AudioSource>().Play();
        string[][] allChallenges = { GameSystem.Instance.easyChallenges, GameSystem.Instance.mediumChallenges,
            GameSystem.Instance.hardChallenges, GameSystem.Instance.veryHardChallenges };
        string[] difficulties = { "Easy", "Medium", "Hard", "Very Hard" };
        for(int d = 0; d < allChallenges.Length; d++)
        {
            for(int c = 0; c < allChallenges[d].Length; c++)
            {
                if(recentlyScored.Equals(allChallenges[d][c]))
                {
                    challengeNotification.GetComponent<Animator>().SetTrigger(difficulties[d] + " Challenge");
                }
            }
        }
    }

    // Total Double Stacks
    private bool CanScoreTotalDoubleStackChallenge(int count)
    {
        return PlayerPrefs.GetInt("Total-Double-Stack-Challenge-" + count) == 0;
    }
    private void ScoreTotalDoubleStackChallenge(int count)
    {
        PlayerPrefs.SetInt("Total-Double-Stack-Challenge-" + count, 1);
        recentlyScored = "Total-Double-Stack-Challenge-" + count;
    }

    // Total Giants
    private bool CanScoreTotalGiantChallenge(int count)
    {
        return PlayerPrefs.GetInt("Total-Giant-Challenge-" + count) == 0;
    }
    private void ScoreTotalGiantChallenge(int count)
    {
        PlayerPrefs.SetInt("Total-Giant-Challenge-" + count, 1);
        recentlyScored = "Total-Giant-Challenge-" + count;
    }

    // Giants In A Row
    private bool CanScoreGiantsInARowChallenge(int count)
    {
        return PlayerPrefs.GetInt("GiantsInARow-Challenge-" + count) == 0;
    }
    private void ScoreGiantsInARowChallenge(int count)
    {
        PlayerPrefs.SetInt("GiantsInARow-Challenge-" + count, 1);
        recentlyScored = "GiantsInARow-Challenge-" + count;
    }

    // Double Stacks In A Row
    private bool CanScoreDoubleStacksInARowChallenge(int count)
    {
        return PlayerPrefs.GetInt("DoubleStacksInARow-Challenge-" + count) == 0;
    }
    private void ScoreDoubleStacksInARowChallenge(int count)
    {
        PlayerPrefs.SetInt("DoubleStacksInARow-Challenge-" + count, 1);
        recentlyScored = "DoubleStacksInARow-Challenge-" + count;
    }

    // Every Obstacle Type
    /*private bool CanScoreObstacleTypeChallenge()
    {
        return PlayerPrefs.GetInt("ObstacleType-Challenge") == 0;
    }
    private void ScoreObstacleTypeChallenge()
    {
        PlayerPrefs.SetInt("ObstacleType-Challenge", 1);
        recentlyScored = "ObstacleType-Challenge";
    }
    */

    // Every Character
    private bool CanScoreEveryCharacterChallenge()
    {
        return PlayerPrefs.GetInt("EveryCharacter-Challenge") == 0;
    }
    private void ScoreEveryCharacterChallenge()
    {
        PlayerPrefs.SetInt("EveryCharacter-Challenge", 1);
        recentlyScored = "EveryCharacter-Challenge";
    }

    private bool CanScoreEveryGiantChallenge()
    {
        return PlayerPrefs.GetInt("EveryGiant-Challenge") == 0;
    }
    private void ScoreEveryGiantChallenge()
    {
        PlayerPrefs.SetInt("EveryGiant-Challenge", 1);
        recentlyScored = "EveryGiant-Challenge";
    }
}