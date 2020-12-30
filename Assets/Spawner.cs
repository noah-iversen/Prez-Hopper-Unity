using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] obstacles;
    public GameObject parent;
    public GameObject ground;

    public GameObject player;

    private bool shouldSpawnStack;
    private bool shouldSpawnHorizontal;
    private float timeBtwSpawn;

    private ArrayList unlockedPlayers = new ArrayList();

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject charButtons in CharacterSystem.Instance.unlockedCharacters)
        {
            foreach (GameObject obstacle in obstacles)
            {
                if (charButtons.name.Split(' ')[0].Equals(obstacle.name.Split('-')[0]))
                {
                    unlockedPlayers.Add(obstacle);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameSystem.Instance.gameEnded == false)
        {
            if (timeBtwSpawn <= 0)
            {
                SetShouldSpawnStack();
                GameObject newOb = Instantiate(obstacles[RandomObstacle()], transform.position, Quaternion.identity);
                newOb.transform.SetParent(parent.transform, false);
                newOb.transform.position = transform.position;
                if (shouldSpawnHorizontal || shouldSpawnStack)
                {
                    GameObject duplicate = Instantiate(obstacles[RandomObstacle()], transform.position, Quaternion.identity);
                    duplicate.transform.SetParent(parent.transform, false);
                    duplicate.transform.position = newOb.transform.position;
                    if (shouldSpawnStack)
                    {
                        duplicate.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                        float scaler = newOb.name.Contains("TrashCan-1") ? 0.5f : 0.7f;
                        duplicate.transform.position = new Vector2(duplicate.transform.position.x, duplicate.transform.position.y + (newOb.transform.lossyScale.y * scaler));
                    }
                    else
                    {
                        duplicate.transform.position = new Vector2(duplicate.transform.position.x + 1, duplicate.transform.position.y);
                    }
                }
                else if (!newOb.name.Contains("TrashCan"))
                {
                    if (GameSystem.Instance.speedScore >= 25)
                    {
                        if (Random.Range(1, 101) <= 25)
                        {
                            newOb.transform.localScale += new Vector3(200, 200, 200);
                        }
                    }
                }
                NewTimeBtwSpawn();
            }
            else
            {
                timeBtwSpawn -= Time.deltaTime;
            }
        }
    }
    public int RandomObstacle()
    {
        int index = Random.Range(0, obstacles.Length);
        if (player.name.Equals(obstacles[index].name.Split('-')[0]))
        {
            return RandomObstacle();
        }
        return index;
    }

    private void NewTimeBtwSpawn()
    {
        if (GameSystem.Instance.speedScore == 30)
        {
            timeBtwSpawn = Random.Range(0.55f, 0.85f);
        }
        else if (GameSystem.Instance.speedScore >= 20)
        {
            timeBtwSpawn = Random.Range(0.5f, 1.3f);
        }
        else if (GameSystem.Instance.speedScore < 20)
        {
            timeBtwSpawn = Random.Range(0.5f, 1.6f);
        }
    }
    private void SetShouldSpawnStack()
    {
        if (GameSystem.Instance.speedScore > 15)
        {
            int random = Random.Range(1, 101);
            bool hitRandom = GameSystem.Instance.speedScore > 20 ? random <= 30 : random <= 25;
            hitRandom = GameSystem.Instance.speedScore > 25 ? random <= 35 : hitRandom;
            if (hitRandom)
            {
                int dupPos = Random.Range(0, 2);
                if (dupPos == 0 || GameSystem.Instance.speedScore < 17)
                {
                    shouldSpawnHorizontal = true; return;
                }
                else
                {
                    shouldSpawnStack = true; return;
                }
            }
        }
        shouldSpawnStack = false;
        shouldSpawnHorizontal = false;
    }
}