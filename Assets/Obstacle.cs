using System;using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obstacle : MonoBehaviour { 


    private Animator animator;
    private GameObject player;
    private GameObject score;

    public int soundIndex = -1;
    public GameObject[] obstacleSounds;

    public bool jumpedOver;

    private void Start()
    { 

        Animator animator = GetComponent<Animator>();
        if (animator != null)
        { 

            this.animator = animator;
        }
        player = GameObject.FindGameObjectWithTag("Player");
        score = GameObject.FindGameObjectWithTag("Score");
    }

    private void Update()
    { 

        if (!GameSystem.Instance.gameEnded)
        { 

            transform.Translate(Vector2.left * GameSystem.Instance.speedScore * Time.deltaTime);
            if (gameObject.transform.position.x < player.transform.position.x)
            { 

                if (!jumpedOver)
                { 

                    jumpedOver = true;

                    if (!GameSystem.Instance.obstacleNames.Contains(gameObject.name.Split('-')[0]))
                    { 

                        GameSystem.Instance.obstacleNames.Add(gameObject.name.Split('-')[0]);
                    }

                    foreach (GameObject obstacle in GameObject.FindGameObjectsWithTag("Obstacle"))
                    { 

                        if ((obstacle.transform.position.x - 3) < player.transform.position.x && !obstacle.GetComponent<Obstacle>().jumpedOver)
                        { 

                            GameSystem.Instance.playerScore += 2;
                            obstacle.GetComponent<Obstacle>().jumpedOver = true;
                            score.GetComponent<Text>().text = GameSystem.Instance.playerScore.ToString();

                            PlaySound();
                            Invoke("PlaySecondSound", 0.06f);


                            if (!GameSystem.Instance.obstacleNames.Contains(obstacle.name.Split('-')[0]))
                            { 

                                GameSystem.Instance.obstacleNames.Add(obstacle.name.Split('-')[0]);
                            }

                            if (Math.Abs(obstacle.transform.position.x - gameObject.transform.position.x) < 0.5)
                            {
                                PlayerPrefs.SetInt("Total-Double-Stacks", PlayerPrefs.GetInt("Total-Double-Stacks") + 1);
                                GameSystem.Instance.stacksInARow += 1;
                                GameSystem.Instance.giantsInARow = 0;
                                if (!GameSystem.Instance.obstacleTypes.Contains("Double-Stack"))
                                { 

                                    GameSystem.Instance.obstacleTypes.Add("Double-Stack");

                                }
                            }
                            else
                            { 

                                GameSystem.Instance.stacksInARow = 0;
                                GameSystem.Instance.giantsInARow = 0;
                                if (!GameSystem.Instance.obstacleTypes.Contains("Horizontal-Stack"))
                                { 

                                    GameSystem.Instance.obstacleTypes.Add("Horizontal-Stack");

                                }
                            }
                            GameSystem.Instance.GetComponent<JumpingChallenges>().CheckForChallenges();
                            GameSystem.Instance.GetComponent<PointsChallenges>().CheckForChallenges();
                            return;
                        }
                    }


                    if (transform.localScale.y == 235)
                    { 

                        GameSystem.Instance.playerScore += 1;
                        GameSystem.Instance.giantsInARow = 0;
                        GameSystem.Instance.stacksInARow = 0;
                        UpdateTotalPoints(1);
                        score.GetComponent<Text>().text = GameSystem.Instance.playerScore.ToString();
                        PlaySound();
                        if (!GameSystem.Instance.obstacleTypes.Contains("Single"))
                        { 

                            GameSystem.Instance.obstacleTypes.Add("Single");

                        }
                    }
                    else
                    { 

                        GameSystem.Instance.playerScore += 3;
                        GameSystem.Instance.giantsInARow += 1;
                        GameSystem.Instance.stacksInARow = 0;
                        UpdateTotalPoints(3);
                        PlayerPrefs.SetInt("Total-Giants", PlayerPrefs.GetInt("TotalGiants") + 1);
                        score.GetComponent<Text>().text = GameSystem.Instance.playerScore.ToString();
                        GameObject.FindGameObjectWithTag("BigObstacleSound").GetComponent<AudioSource>().Play();
                        if (!GameSystem.Instance.obstacleTypes.Contains("Giant"))
                        { 

                            GameSystem.Instance.obstacleTypes.Add("Giant");
                        }

                        if(!GameSystem.Instance.obstacleGiantNames.Contains(gameObject.name.Split('-')[0]))
                        {
                            GameSystem.Instance.obstacleGiantNames.Add(gameObject.name.Split('-')[0]);
                        }
                    }

                    GameSystem.Instance.GetComponent<JumpingChallenges>().CheckForChallenges();
                    GameSystem.Instance.GetComponent<PointsChallenges>().CheckForChallenges();


                }
            }
        }
        else
        { 

            if (animator != null && animator.enabled)
            { 

                animator.enabled = false;
            }
        }

    }

    private void PlaySound()
    { 

        GameObject.FindGameObjectWithTag("Score").GetComponent<AudioSource>().Play();
    }

    private void PlaySecondSound()
    { 

        GameObject.FindGameObjectWithTag("Ground").GetComponent<AudioSource>().Play();
    }

    private void UpdateTotalPoints(int score)
    { 

        PlayerPrefs.SetInt("Total-Points", PlayerPrefs.GetInt("Total-Points") + score);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 

        if (collision.CompareTag("Player"))
        { 

            if (!GameSystem.Instance.gameEnded)
            { 

                GameSystem.Instance.gameEnded = true;
                GameSystem.Instance.speedScore = 10;
                GameSystem.Instance.deathScreen.GetComponent<GameOver>().scoreboard.SetActive(false);
                GameSystem.Instance.deathScreen.SetActive(true);
                GameSystem.Instance.deathScreen.GetComponent<GameOver>().UpdateScoreText();
                GameSystem.Instance.obstacleNames.Clear();

                if (UnityEngine.Random.Range(0, 2) == 0 && soundIndex != -1)
                { 

                    GameSystem.Instance.obstacleSounds[soundIndex].GetComponent<AudioSource>().Play();
                }
                else
                { 

                    GameObject.FindGameObjectWithTag("GameOverSound").GetComponent<AudioSource>().Play();
                }

                int deathCount = PlayerPrefs.GetInt("DeathCount");
                if (deathCount < 4)
                { 

                    PlayerPrefs.SetInt("DeathCount", deathCount + 1);
                    PlayerPrefs.Save();
                    if(!CharacterSystem.Instance.interstitial.IsLoaded())
                    {
                        CharacterSystem.Instance.RequestInterstitial();
                    }
                }
                else
                { 

                    if (CharacterSystem.Instance.interstitial.IsLoaded())
                    {
                        CharacterSystem.Instance.interstitial.Show();
                    }
                    PlayerPrefs.SetInt("DeathCount", 0);

                }
            }

        }
    }
}
