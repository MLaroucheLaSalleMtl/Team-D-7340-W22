using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //Variable for singleton pattern
    public static EnemySpawner instance = null;

    //Variables for spawner
    public static int countAliveEnemy = 0;
    public Wave[] waves;
    private int waveCount = 0;
    public Transform start;
    [SerializeField] private float waveRate = 0.1f; //Interval between waves

    //Game manager
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.instance; //Cache the game manager
        StartCoroutine("SpawnEnemy");
    }

    public void Stop()
    {
        StopCoroutine("SpawnEnemy");
    }
    IEnumerator SpawnEnemy()
    {
        foreach (Wave wave in waves)
        {
            for (int i = 0; i < wave.count; i++)
            {
                GameObject.Instantiate(wave.enemy, start.position, Quaternion.identity);
                countAliveEnemy++;
                if (i != wave.count - 1)
                    yield return new WaitForSeconds(wave.rate);
            }
            while (countAliveEnemy > 0)
            {
                yield return 0;
            }
            yield return new WaitForSeconds(waveRate);
            waveCount++;
        }
        //Game winner condition
        if (waveCount == waves.Length)
            gameManager.Win();
    }

    //Singleton pattern
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }


}
