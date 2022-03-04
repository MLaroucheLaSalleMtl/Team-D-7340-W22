using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    private AsyncOperation async;

    //For BtnNext
    public void Load()
    {
        if (async == null) //To avoid freezing problem
        {
            Scene currentScene = SceneManager.GetActiveScene();            
            async = SceneManager.LoadSceneAsync(currentScene.buildIndex + 1);
        }
    }
    //For switch by scene No.
    public void Load(int index)
    {
        if (async == null) //To avoid freezing problem
        {
            async = SceneManager.LoadSceneAsync(index);
        }
    }
    //For switch by scene name
    public void Load(string name)
    {
        if (async == null) //To avoid freezing problem
        {
            async = SceneManager.LoadSceneAsync(name);
        }
    }
}
