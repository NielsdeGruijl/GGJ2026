using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public struct MaskData
{
    public string maskName;
    public Sprite maskSprite;
    public float damageDealt;
    public int maskCount;
}

public class SessionData : MonoBehaviour
{
    public static SessionData instance;
    
    public List<MaskData> maskData = new();

    [ReadOnly] public string timeSurvived;

    private void Awake()
    {
        if (instance && instance != this)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        SceneManager.sceneLoaded += ResetData;
    }

    private void ResetData(Scene tempScene, LoadSceneMode mode)
    {
        if (tempScene == SceneManager.GetSceneByBuildIndex(1))
        {
            maskData = new();
            timeSurvived = "";
        }
    }
}
