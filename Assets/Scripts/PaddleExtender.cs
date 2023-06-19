using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleExtender : MonoBehaviour
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
            Destroy(gameObject);
            StartCoroutine(BiggerPaddle());
        }
    }
   
    public IEnumerator BiggerPaddle()
    {
        paddle.transform.localScale = new Vector3(1.5f,1.5f,0);
        yield return new WaitForSeconds(10.0f);
    }
}
