using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Terminal : MonoBehaviour
{
    public int hp = 10;
    private int totalHP;
    public Slider hpSlider;

    void Start()
    {
        totalHP = hp;
        hpSlider = GetComponentInChildren<Slider>();
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        //Let the throne hp bar working.
        hpSlider.value = (float)hp / totalHP;
        if (hp <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Destroy(this.gameObject);
        GameManager.Instance.Defeat();
    }
}
