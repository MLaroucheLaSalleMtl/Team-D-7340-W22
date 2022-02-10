using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class Loading : MonoBehaviour
{
    private AsyncOperation async;
    [SerializeField] private Image progressbar;
    [SerializeField] private Text txtPercent;

    [SerializeField] private bool waitForUserInput = false;
    [SerializeField] private GameObject txtWaitForUserInput;
    private bool ready = false;

    [SerializeField] private float delay = 0;

    // Start is called before the first frame update
    void Start()
    {
        InitParam();
        LoadScene();

    }

    public void Activate()
    {
        ready = true;
    }


    void InitParam()
    {
        Time.timeScale = 1.0f;
        Input.ResetInputAxes();
        System.GC.Collect();

    }

    void LoadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        async = SceneManager.LoadSceneAsync(currentScene.buildIndex + 1);

        async.allowSceneActivation = false;
        if (!waitForUserInput)
        {
            Invoke("Activate", delay);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (progressbar)
            progressbar.fillAmount = async.progress + 0.1f;

        if (txtPercent)
            txtPercent.text = ((async.progress + 0.1f) * 100).ToString("F2") + "%";

        if (async.progress > 0.89f && SplashScreen.isFinished && waitForUserInput)
        {
            if (txtWaitForUserInput)
                txtWaitForUserInput.SetActive(true);
            if (Input.anyKey)
            {
                ready = true;
            }
        }


        if (async .progress > 0.89f && SplashScreen.isFinished && ready)
        {
            async.allowSceneActivation = true;

        }
    }
}
