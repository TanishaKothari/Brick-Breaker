using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PowerUpSpawner : MonoBehaviour
{
    public MultiBall multiBallScript;
    public PaddleExtender paddleExtenderScript;
    public SlowBall slowBallScript;
    public ExtraLife extraLifeScript;
    public GameManager gameManager { get; private set; }
    public Ball ball { get; private set; }
    int spawnedPowerup = 0;

    [SerializeField] 
    private GameObject[] _powerupPrefabs = null;

    public enum PowerupType
    {
        None = 0,
        MultiBall = 1,
        PaddleExtender = 2,
        SlowBall = 3,
        ExtraLife = 4
    }
    
    private void Start()
    {
        multiBallScript = GetComponent<MultiBall>();
        paddleExtenderScript = GetComponent<PaddleExtender>();
        slowBallScript = GetComponent<SlowBall>();
        extraLifeScript = GetComponent<ExtraLife>();
        gameManager = FindObjectOfType<GameManager>();
        ball = FindObjectOfType<Ball>();
    }
    
    public IEnumerator SpawnPowerUpRoutine()
    {
        float randomX = Random.Range(-11, 11);
        Vector3 randomXposition = new Vector3(randomX, 8, 0);
        int randomPowerup = Random.Range(0, _powerupPrefabs.Length);
        spawnedPowerup = randomPowerup;
        if (randomPowerup > 0 || randomPowerup < 0)
        {
            Instantiate(_powerupPrefabs[randomPowerup], randomXposition, Quaternion.identity);
        }
        else
        {
            if(ball.ballCount < 1 || ball.ballCount > 1)
            {
                randomPowerup = Random.Range(1, _powerupPrefabs.Length);
                spawnedPowerup = randomPowerup;
                Instantiate(_powerupPrefabs[randomPowerup], randomXposition, Quaternion.identity);
            }       
            else
            {
                Instantiate(_powerupPrefabs[randomPowerup], randomXposition, Quaternion.identity);
            }
        }
        yield return new WaitForSeconds(30);

        switch (spawnedPowerup)
        {
            case 1:
                multiBallScript.enabled = true;
                break;
            case 2:
                paddleExtenderScript.enabled = true;
                break;
            case 3:
                slowBallScript.enabled = true;
                break;
            case 4:
                extraLifeScript.enabled = true;
                break;
        }
    }
}