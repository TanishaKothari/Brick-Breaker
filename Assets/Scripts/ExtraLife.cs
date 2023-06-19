using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLife : MonoBehaviour
{
    public Ball ball { get; private set; }
    public Paddle paddle { get; private set; }
    public GameManager gameManager { get; private set; }

    private void Start()
    {
        ball = FindObjectOfType<Ball>();
        paddle = FindObjectOfType<Paddle>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Paddle"))
        {
            Destroy(gameObject);
            StartCoroutine(AddNewLife());
        }
    }

    public IEnumerator AddNewLife()
    {
        if(gameManager.lives < 3)
        {
            int lives = gameManager.lives + 1;
            gameManager.lifeCount.text = " Lives: " + lives.ToString();
            gameManager.lives += 1;
        }
        yield return new WaitForSeconds(10.0f);
    }
}
