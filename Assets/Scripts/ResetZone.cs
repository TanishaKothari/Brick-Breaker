using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ResetZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!(other.gameObject.tag == "SlowBall"))
        {
            other.gameObject.SetActive(false);
        } 
        else
        {
            other.gameObject.SetActive(true);
        }
        
        if (other.gameObject.tag != "Ball")
        {}
        else
        {
            FindObjectOfType<GameManager>().Miss();
        }
    }
}
