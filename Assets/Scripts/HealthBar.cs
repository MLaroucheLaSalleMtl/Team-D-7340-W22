using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Image img;
    // Use this for initialization
    void Start()
    {
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //decrease health
        if (Input.GetKeyDown("a"))
        {
            img.fillAmount -= 0.1f;
        }
        //increase health
        if (Input.GetKeyDown("d"))
        {
            img.fillAmount += 0.1f;
        }
    }
}
