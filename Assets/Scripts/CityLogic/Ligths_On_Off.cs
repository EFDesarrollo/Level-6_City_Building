using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ligths_On_Off : MonoBehaviour
{
    private Light light;
    private FireflyScript FireflyScript;
    private float intensity;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        FireflyScript = GetComponent<FireflyScript>();
        intensity = light.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        if (DayCycle.instance.curDayTime >= 6 && DayCycle.instance.curDayTime <= 18)
        {
            light.intensity = 0;
            if (FireflyScript != null)
                FireflyScript.DisableFireflies();
        }
        else
        {
            light.intensity = intensity;
            if (FireflyScript != null)
                FireflyScript.AbleFireflies();
        }
    }
}
