using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThroneStat: MonoBehaviour
{
    public static int hp;
    public int totalHp = 10;

    void Start()
    {
        hp = totalHp;
    }
}
