
// Recommendation: I learn the code from Brackeys, but I change the some code for myself.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionController : MonoBehaviour
{
    public float speed = 1.0f;
    public float mouseSpeed = 2.0f;

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float mouse = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(new Vector3(h*speed, mouse*mouseSpeed, v*speed) * Time.deltaTime * speed, Space.World);
                
    }
}
