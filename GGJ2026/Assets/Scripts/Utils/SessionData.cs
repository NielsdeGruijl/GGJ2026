using System;
using System.Collections.Generic;
using UnityEngine;

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

    public string timeSurvived;

    private void Awake()
    {
        if (instance && instance != this)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
    }
}
