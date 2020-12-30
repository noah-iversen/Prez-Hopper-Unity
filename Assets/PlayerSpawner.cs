using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private int regCharCount = 13;
    public GameObject[] players; 
    public GameObject parent;
    private ArrayList unlockedPlayers = new ArrayList();

    public GameObject spawner;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject charButtons in CharacterSystem.Instance.unlockedCharacters)
        {
            foreach(GameObject player in players)
            {
                if(charButtons.name.Split(' ')[0].Equals(player.name))
                {
                    unlockedPlayers.Add(player);
                }
            }
        }
        int playerIndex = PlayerPrefs.GetInt("Selection") == regCharCount ? Random.Range(0, regCharCount) : PlayerPrefs.GetInt("Selection");
        if (playerIndex == players.Length)
        {
            GameObject randomPlayer = (GameObject) unlockedPlayers[Random.Range(0, unlockedPlayers.Count)];
            GameObject player = Instantiate(randomPlayer, transform.position, Quaternion.identity);
            player.transform.SetParent(parent.transform, false);
            player.transform.position = transform.position;
            for (int i = 0; i < players.Length; i++)
            {
                if (randomPlayer.name == players[i].name)
                {
                    playerIndex = i;
                }
            }
        }
        else
        {
            GameObject player = Instantiate(players[playerIndex], transform.position, Quaternion.identity);
            player.transform.SetParent(parent.transform, false); player.transform.position = transform.position;
        }
        spawner.GetComponent<Spawner>().player = players[playerIndex];
        PlayerPrefs.SetInt("Selection", playerIndex);
        CharacterSystem.Instance.selectedCharacter = playerIndex;
        Destroy(gameObject);
    }
}