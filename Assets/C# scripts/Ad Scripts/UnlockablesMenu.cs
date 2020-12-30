using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockablesMenu : MonoBehaviour
{
    public GameObject[] lockedButtons;
    public GameObject[] unlockedButtons;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < lockedButtons.Length; i++)
        {
            if (PlayerPrefs.GetInt("Unlocked-Character-" + i) == 1)
            {
                lockedButtons[i].SetActive(false);
                unlockedButtons[i].SetActive(true);
                CharacterSystem.Instance.unlockedCharacters.Add(unlockedButtons[i]);
            }
        }

        if (CharacterSystem.Instance.selectedCharacter != -1 && CharacterSystem.Instance.selectedCharacter < 14)
        {
            foreach (GameObject button in GameObject.FindGameObjectsWithTag("CharacterSelect"))
            {
                Color clearColor = button.GetComponent<SpriteRenderer>().color;
                clearColor.a = 0.8f;
                button.GetComponent<SpriteRenderer>().color = clearColor;
            }
        }
        gameObject.SetActive(false);
    }



    public void Back()
    {
        gameObject.SetActive(false);
        transform.parent.gameObject.GetComponent<AudioSource>().Play();
        CharacterSystem.Instance.mainScreen.SetActive(true);
        if (CharacterSystem.Instance.selectedCharacter >= 14)
        {
            foreach (GameObject button in GameObject.FindGameObjectsWithTag("CharacterSelect"))
            {
                Color clearColor = button.GetComponent<SpriteRenderer>().color; clearColor.a = 0.8f;
                button.GetComponent<SpriteRenderer>().color = clearColor;
            }
        }
    }    
}