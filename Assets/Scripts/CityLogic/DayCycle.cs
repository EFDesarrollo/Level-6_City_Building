using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    public float dayTime;
    public float curDayTime;
    public float cycleSpeed = 1.0f;
    public GameObject sun;
    public Gradient backgroundGradient;
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
        /*Time.timeScale = cycleSpeed;*/
        curDayTime += Time.deltaTime * cycleSpeed * 0.5f;
        hours = (int)curDayTime % 60;
        minutes = (int)(curDayTime * 60) % 60;
        if (curDayTime >= dayTime)
        {
            curDayTime = 0;
            days++;
            /*City.instance.EndTurn();*/
        }

        float rotation = 270;
        if (curDayTime < 6)
        {
            rotation = 360 + (curDayTime - 6) * 15;
        }
        else if (curDayTime >= 6 && curDayTime < 12)
        {
            rotation = 0 + (curDayTime - 6) * 15;
        }
        else if (curDayTime >= 12 && curDayTime < 18)
        {
            rotation = 90 + (curDayTime - 12) * 15;
        }
        else if (curDayTime >= 18)
        {
            rotation = 180 + (curDayTime - 18) * 15;
        }

        // Rotate the Light with the current day time / day time multiplied with 360 degrees
        sun.transform.rotation = Quaternion.Euler(rotation, 0f, 0f);

        // Move the Skybox with the current time
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * 2);

        // Update background color based on the current time of day
        Camera.main.backgroundColor = backgroundGradient.Evaluate(curDayTime / dayTime);

        UIDisplayManager.Instance.UpdateDisplay();
    }

    public void IncreaseCycleSpeed(float amount)
    {
        cycleSpeed += amount;
        cycleSpeed = (int)(cycleSpeed * 10);
        cycleSpeed = cycleSpeed / 10;
        if (cycleSpeed > 2)
            cycleSpeed = 2;
    }

    public void DecreaseCycleSpeed(float amount)
    {
        cycleSpeed -= amount;
        cycleSpeed = (int)(cycleSpeed * 10);
        cycleSpeed = cycleSpeed / 10;
        if (cycleSpeed < 0.1)
        {
            cycleSpeed = 0.1f;
        }
    }
}
