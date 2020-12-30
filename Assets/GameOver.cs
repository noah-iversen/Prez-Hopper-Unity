using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject score;
    public GameObject highScore;

    public GameObject challengePoints;

    public GameObject parentScreen;
    public GameObject deathScreen;
    public GameObject scoreboard;
    public GameObject challengesScreen;

    public GameObject score1;
    public GameObject score2;
    public GameObject score3;


    private void Start()
    {
    }

    IEnumerator RecordFrameAndShare()
    {
        yield return new WaitForEndOfFrame();
        var texture = ScreenCapture.CaptureScreenshotAsTexture();
        NativeShare shareScreen = new NativeShare();
        shareScreen.SetSubject("Prez Hopper");
        shareScreen.AddFile(texture, "score.png");
        shareScreen.SetText("Check out my score on Prez Hopper!");
        shareScreen.Share();        // cleanup
        Object.Destroy(texture);
    }

    public void Back()
    {
        deathScreen.SetActive(true);
        GameSystem.Instance.gameScreen.SetActive(true);
        scoreboard.SetActive(false);
        challengesScreen.SetActive(false);
        parentScreen.GetComponent<AudioSource>().Play();
    }

    public void ScoreBoard()
    {
        scoreboard.SetActive(true);
        deathScreen.SetActive(false);
        score1.GetComponent<Text>().text = PlayerPrefs.GetInt("Score-1").ToString();
        score2.GetComponent<Text>().text = PlayerPrefs.GetInt("Score-2").ToString();
        score3.GetComponent<Text>().text = PlayerPrefs.GetInt("Score-3").ToString();
        parentScreen.GetComponent<AudioSource>().Play();
    }

    public void PlayAgain()
    {
        CharacterSystem.Instance.replay = true;
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByBuildIndex(2));
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
        GameSystem.Instance.gameObject.SetActive(true);
        parentScreen.GetComponent<AudioSource>().Play();
    }

    public void ChooseCharacter()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByBuildIndex(2));
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
        CharacterSystem.Instance.gameObject.SetActive(true);
        CharacterSystem.Instance.ClearSelection();
        CharacterSystem.Instance.GetComponent<AudioSource>().Play();
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));
    }

    public void Challenges()
    {
        challengesScreen.SetActive(true);
        deathScreen.SetActive(false);
        GameSystem.Instance.gameScreen.SetActive(false);
        parentScreen.GetComponent<AudioSource>().Play();
        challengePoints.GetComponent<Text>().text = PlayerPrefs.GetInt("Challenge-Points").ToString();
    }

    public void UpdateScoreText()
    {
        score.GetComponent<Text>().text = GameSystem.Instance.playerScore.ToString();
        CalculateTopScore();
        highScore.GetComponent<Text>().text = PlayerPrefs.GetInt("Score-1").ToString();
    }

    public void Share()
    {
        StartCoroutine(RecordFrameAndShare());
    }

    public void CalculateTopScore()
    {
        SortedDictionary<int, int> scoresAndFaces = new SortedDictionary<int, int>();
        // Get all previous scores
        for (int i = 1; i <= 3; i++)
        {
            if (!scoresAndFaces.ContainsKey(PlayerPrefs.GetInt("Score-" + i)))
            {
                scoresAndFaces.Add(PlayerPrefs.GetInt("Score-" + i), PlayerPrefs.GetInt("Score-" + i + "-Face"));
            }
        }        // Add selection and update data
        if (GameSystem.Instance.playerScore > 0 && !scoresAndFaces.ContainsKey(GameSystem.Instance.playerScore))
        {
            scoresAndFaces.Add(GameSystem.Instance.playerScore, PlayerPrefs.GetInt("Selection"));
        }
        int count = scoresAndFaces.Count;
        foreach (KeyValuePair<int, int> scoreAndFace in scoresAndFaces)
        {
            if (count > 0 && count < 4)
            {
                PlayerPrefs.SetInt("Score-" + count, scoreAndFace.Key);
                PlayerPrefs.SetInt("Score-" + count + "-Face", scoreAndFace.Value);
            }
            count--;
        }
        PlayerPrefs.Save();
    }
}