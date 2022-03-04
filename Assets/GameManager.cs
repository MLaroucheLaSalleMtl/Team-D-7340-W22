using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject endUI;
    public Text endMessage;

    public static GameManager Instance;
    private EnemySpawner enemySpawner;
    private void Awake()
    {
        Instance = this;
        enemySpawner = GetComponent<EnemySpawner>();
    }
    public void Win()
    {
        endUI.SetActive(true);
        endMessage.text = "Victory";
    }

    public void Defeat()
    {
        enemySpawner.Stop();
        endUI.SetActive(true);
        endMessage.text = "Defeat";

    }
}
