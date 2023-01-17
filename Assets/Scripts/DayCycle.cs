using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    public float dayTime;
    public float curDayTime;
    public GameObject sun;
    //[HideInInspector]
    public int days = 0, hours = 0, minutes = 0;
    public static DayCycle instance;
    private void Start()
    {
        instance = this;
    }
    private void FixedUpdate()
    {
        DayCicle();
    }

    private void DayCicle()
    {
        curDayTime += Time.deltaTime;
        hours= (int)(curDayTime / ((dayTime / 24)/60));
        minutes = (int)(curDayTime % ((dayTime / 24) / 60));
        if (curDayTime >= dayTime)
        {
            curDayTime = 0;
            days++;
            City.instance.EndTurn();
        }

        // Rotate the Light with the current day time / day time multiplied with 360 degrees
        sun.transform.rotation = Quaternion.Euler((curDayTime / dayTime) * 360, 0f, 0f);

        // Move the Skybox with the current time
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * 2);
    }
}
