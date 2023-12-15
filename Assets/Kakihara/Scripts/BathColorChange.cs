using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BathColorChange : MonoBehaviour
{
    private Material bathMat;

    void Start()
    {
        bathMat = this.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {

        float currentTemperature = SceneGameManager.GetInstance().GetBathManager().CurrentTemperature;

        if (currentTemperature < 35f)
        {
            bathMat.color = new Color32(70, 70, 255, 255);
        }
        else if (35f <= currentTemperature && currentTemperature < 38f)
        {
            bathMat.color = new Color32(110, 150, 255, 255);
        }
        else if (42f < currentTemperature && currentTemperature <= 45f)
        {
            bathMat.color = new Color32(255, 160, 160, 255);
        }
        else if (currentTemperature > 45f)
        {
            bathMat.color = new Color32(255, 50, 50, 255);
        }
        else
        {
            bathMat.color = new Color32(206, 226, 250, 255);
        }
    }
}
