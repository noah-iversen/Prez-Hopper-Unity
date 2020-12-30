using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmationPopup : MonoBehaviour
{
    public GameObject gameSelectSound;

    public void Yes()
    {
        CharacterSystem.Instance.unlockablesScreen.SetActive(true);
        gameSelectSound.GetComponent<AudioSource>().Play();
        var menu = CharacterSystem.Instance.unlockablesScreen.GetComponent<UnlockablesMenu>();
        var index = CharacterSystem.Instance.requestedCharacter;
        PlayerPrefs.SetInt("Unlocked-Character-" + index, 1);
        menu.lockedButtons[index].SetActive(false);
        menu.unlockedButtons[index].SetActive(true);
        CharacterSystem.Instance.unlockedCharacters.Add(menu.unlockedButtons[index]);
        PlayerPrefs.SetInt("Challenge-Points", PlayerPrefs.GetInt("Challenge-Points") - CharacterSystem.Instance.characterCost);
        CharacterSystem.Instance.challengePoints.GetComponent<Text>().text = PlayerPrefs.GetInt("Challenge-Points").ToString();
        CharacterSystem.Instance.requestedCharacter = -1;
        gameObject.SetActive(false);
    }

    public void No()
    {
        gameObject.SetActive(false);
        CharacterSystem.Instance.unlockablesScreen.SetActive(true);
        gameSelectSound.GetComponent<AudioSource>().Play();
        CharacterSystem.Instance.requestedCharacter = -1;
    }
}
