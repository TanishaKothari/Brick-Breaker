using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowBall : MonoBehaviour
{
    public Ball ball { get; private set; }
    public Paddle paddle { get; private set; }

    private void Start()
    {
        ball = FindObjectOfType<Ball>();
        paddle = FindObjectOfType<Paddle>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Paddle"))
        {
            StartCoroutine(SlowDownBall());
        }
    }

    public IEnumerator SlowDownBall()
    {
        int original_speed = 10;
        ball.speed = 6;
        yield return new WaitForSeconds(30);
        ball.speed = original_speed;
    }
}
