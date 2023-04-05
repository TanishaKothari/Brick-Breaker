using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Ball ball { get; private set; }
    public Paddle paddle { get; private set; }
    public Brick[] bricks { get; private set; }
    public Text lifeCount { get; private set; }
    public Button nextLevel;
    public Button replay;

    const int NUM_LEVELS = 2;

    public int level = 1;
    public int score = 0;
    public int lives = 3;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        score = 0;
        lives = 3;

        LoadLevel(1);
    }

    private void LoadLevel(int level)
    {
        this.level = level;

        if (level > NUM_LEVELS)
        {
            LoadLevel(1);
            return;
        }

        SceneManager.LoadScene("Level" + level);
        lives = 3;
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Win Scene")
        {
            replay = GameObject.FindGameObjectWithTag("playAgain").GetComponent<Button>();
            replay.onClick.AddListener(() => NewGame());
        }
        else 
        {
            ball = FindObjectOfType<Ball>();
            paddle = FindObjectOfType<Paddle>();
            bricks = FindObjectsOfType<Brick>();
            lifeCount = FindObjectOfType<Text>();

            ball.speed += 2;
            nextLevel = GameObject.FindGameObjectWithTag("NextLevel").GetComponent<Button>();
            nextLevel.gameObject.SetActive(false);
        }
    }

    public void Miss()
    {
        lives--;
        if (lives == 3) {
            lifeCount.text = " Lives: 3";
        } else if (lives == 2) {
            lifeCount.text = " Lives: 2";
        } else if (lives == 1) {
            lifeCount.text = " Lives: 1";
        } else {
            lifeCount.text = " Lives: 0";
        }

        if (lives > 0) {
            ResetLevel();
        } else {
            GameOver();
        }
    }

    private void ResetLevel()
    {
        paddle.ResetPaddle();
        ball.ResetBall();
    }

    private void GameOver()
    {
        // Restart the level immediately
        LoadLevel(level);
    }

    public void Hit(Brick brick)
    {
        score += brick.points;

        if (Cleared()) {
            if (level+1 > NUM_LEVELS) {
                SceneManager.LoadScene(3);
                //replay.onClick.AddListener(() => SceneManager.LoadScene(0));
            } else {
                nextLevel.gameObject.SetActive(true);
                nextLevel.onClick.AddListener(() => LoadLevel(level + 1));
                ball.speed = 0;
            }
        }
    }

    private bool Cleared()
    {
        for (int i = 0; i < bricks.Length; i++)
        {
            if (bricks[i].gameObject.activeInHierarchy && !bricks[i].unbreakable) {
                return false;
            }
        }

        return true;
    }
}