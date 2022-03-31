using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject endUI;
    public Text endMessage;

    public static GameManager instance;
    private EnemySpawner enemySpawner;

    public Button BtnNextLevel;

    void Update()
    {
        if (ThroneStat.hp <= 0)
        {
            Defeat();
        }

    }

    private void Awake()
    {
        //Singleton pattern
        instance = this;
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        enemySpawner = GetComponent<EnemySpawner>();
    }
    public void Win()
    {
        enemySpawner.Stop();
        endUI.SetActive(true);
        endMessage.color = Color.green;
        endMessage.text = "Victory";
    }

    public void Defeat()
    {
        enemySpawner.Stop();
        endUI.SetActive(true);
        BtnNextLevel.gameObject.SetActive(false);//when game defeat, you can not go to  next level.
        endMessage.text = "Defeat";
    }
}
