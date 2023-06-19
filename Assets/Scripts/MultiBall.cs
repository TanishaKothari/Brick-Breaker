using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBall : MonoBehaviour
{
    public Ball ball { get; private set; }
    public Paddle paddle { get; private set; }
    public GameObject Ball;

    private void Start()
    {
        ball = FindObjectOfType<Ball>();
        paddle = FindObjectOfType<Paddle>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Paddle"))
        {
            Destroy(gameObject);
            StartCoroutine(MultipleBalls());
        }
    }

    public IEnumerator MultipleBalls()
    {

        for (int i = 0; i < 2; i++)
        {
            Instantiate(Ball, new Vector3(0,0,0), Quaternion.identity);
        }
        ball.ballCount = 3;
        yield return new WaitForSeconds(10);
    }
   
}