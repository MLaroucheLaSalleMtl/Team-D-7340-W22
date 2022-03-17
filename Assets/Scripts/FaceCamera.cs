using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Camera myCam;

    void Start()
    {
        myCam = Camera.main;
    }
    // Update is called once per frame
    void LateUpdate() //Use LateUpdate() instead of Update() to fix the rotation in a flash every time the object rotates
    {
        transform.LookAt(transform.position + myCam.transform.rotation * Vector3.back, myCam.transform.rotation*Vector3.down);
    }
}
