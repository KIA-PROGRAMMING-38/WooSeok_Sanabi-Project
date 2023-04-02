using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SettingSceneController : MonoBehaviour
{
    public bool isFullScreen = true;

    public enum UIType
    {
        NormalText,
        Button,
        Slider
    }

    public float volume = 1f;

    private void Awake()
    {
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
