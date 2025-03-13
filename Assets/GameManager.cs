using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private Spawner spawner;
    public GameObject title;
    private Vector2 screenBounds;

    public GameObject splash;

    [Header("Player")]
    public GameObject playerPrefab;
    private GameObject player;
    private bool gameStarted = false;

    [Header("Score")]
    public TMP_Text scoreText;
    public int pointsWorth = 1;
    private int score;

    private int bestScore;
    public TMP_Text bestScoreText;
    private bool beatBestScore;

    private bool smokeCleared = true;

    public float splashScreenDelayTime = 3f;

    private void Awake()
    {
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        scoreText.enabled = false;
        bestScoreText.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        spawner.active = false;
        title.SetActive(true);
        splash.SetActive(false);
        bestScore = PlayerPrefs.GetInt("BestScore");
        bestScoreText.text = "Best Score: " + bestScore.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStarted)
        {
            if (Input.anyKeyDown && smokeCleared)
            {
                smokeCleared = false;
                ResetGame();
            }
        }
        else
        {
            if (!player)
            {
                OnPlayerKilled();
            }
        }
        var nextBomb = GameObject.FindGameObjectsWithTag("Bomb");

        foreach (GameObject bombObject in nextBomb)
        {
            if (bombObject.transform.position.y < (-screenBounds.y - 12f))
            {
                if (gameStarted)
                {
                    score += pointsWorth;
                    scoreText.text = "Score: " + score.ToString();

                }
                Destroy(bombObject);
            }
        }
    }



    void ResetGame()
    {
        spawner.active = true;
        title.SetActive(false);
        player = Instantiate(playerPrefab, new Vector3(0, 0, 0), playerPrefab.transform.rotation);
        gameStarted = true;
        splash.SetActive(false);
        scoreText.enabled = true;
        score = 0;
        scoreText.text = "Score: " + score.ToString();
        beatBestScore = false;
        bestScoreText.enabled = true;
    }

    void OnPlayerKilled()
    {
        spawner.active = false;
        gameStarted = false;

        Invoke("SplashScreen", splashScreenDelayTime);
           

    }

    void SplashScreen()
    {
        smokeCleared = true;
        splash.SetActive(true);


        
        if(score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
            beatBestScore = true;
            bestScoreText.text = "Best Score: " + bestScore.ToString();
        }
    }
}
