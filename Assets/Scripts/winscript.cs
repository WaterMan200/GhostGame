using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class winscript : MonoBehaviour
{
    public Material skyDay;
    public Material skyNight;
    public GameObject sun;
    public GameObject enemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RenderSettings.skybox = skyNight;
        sun.SetActive(false);
        enemy.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setDay()
    {
        RenderSettings.skybox = skyDay;
        sun.SetActive(true);
        enemy.SetActive(false);
    }



}
