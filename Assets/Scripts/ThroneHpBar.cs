using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThroneHpBar : MonoBehaviour
{
    [SerializeField] private int hp;
    private int totalHP;
    public Slider hpSlider;

    void Start()
    {
        totalHP = 10;
        hpSlider = GameObject.Find("HPSlider").GetComponent<Slider>();
    }

    void Update()
    {
        hp = ThroneStat.hp;
        hpSlider.value = (float)hp / totalHP;
    }
}
