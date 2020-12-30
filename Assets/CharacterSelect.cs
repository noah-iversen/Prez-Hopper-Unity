using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    public GameObject gameSelectSound;

    // Start is called before the first frame update
    public void SelectCharacter(int character)
    {
        if (character == 20 && CharacterSystem.Instance.unlockedCharacters.Count == 0) return;
        CharacterSystem.Instance.selectedCharacter = character;
        gameSelectSound.GetComponent<AudioSource>().Play();
        foreach (GameObject button in GameObject.FindGameObjectsWithTag("CharacterSelect"))
        {
            if (button != transform.parent.gameObject)
            {
                Color clearColor = button.GetComponent<SpriteRenderer>().color;
                clearColor.a = 0.8f; button.GetComponent<SpriteRenderer>().color = clearColor;
                Color color = GetComponent<SpriteRenderer>().color; color.a = 1.0f;
                GetComponent<SpriteRenderer>().color = color;
            }
        }
    }

    public void SelectUnlockables()
    {
        transform.parent.gameObject.SetActive(false);
        CharacterSystem.Instance.unlockablesScreen.SetActive(true);
        CharacterSystem.Instance.challengePoints.GetComponent<Text>().text = PlayerPrefs.GetInt("Challenge-Points").ToString();
        gameSelectSound.GetComponent<AudioSource>().Play();
        if (CharacterSystem.Instance.selectedCharacter != -1 && CharacterSystem.Instance.selectedCharacter < 14)
        {
            foreach (GameObject button in GameObject.FindGameObjectsWithTag("CharacterSelect"))
            {
                Color clearColor = button.GetComponent<SpriteRenderer>().color; clearColor.a = 0.8f; button.GetComponent<SpriteRenderer>().color = clearColor;
            }
        }
    }
    public void SelectPlay()
    {
        if (CharacterSystem.Instance.selectedCharacter != -1)
        {
            GameSystem.Instance.gameObject.SetActive(true);
            PlayerPrefs.SetInt("Selection", CharacterSystem.Instance.selectedCharacter);
            GameSystem.Instance.character = PlayerPrefs.GetString("Selection");
            GameSystem.Instance.gameObject.GetComponent<AudioSource>().Play();
            CharacterSystem.Instance.gameObject.SetActive(false);
        }
    }
}