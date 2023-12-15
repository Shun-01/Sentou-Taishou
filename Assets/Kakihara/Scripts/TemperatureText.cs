using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TemperatureText : MonoBehaviour
{
    [SerializeField]
    private BathManager bathManager;

    public float currentTemperature;

    private TMP_Text temperatureText;

    void Start()
    {
        temperatureText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTemperature = SceneGameManager.GetInstance().GetBathManager().CurrentTemperature;

        if (currentTemperature <= 38f)
        {
            temperatureText.text = string.Format($"今の温度 <color=#5e7aff>{currentTemperature.ToString("F1")}</color> ℃");
        }   
        else if (currentTemperature >= 42f)
        {
            temperatureText.text = string.Format($"今の温度 <color=#ff5c5c>{currentTemperature.ToString("F1")}</color> ℃");
        }
        else
        {
            temperatureText.text = string.Format($"今の温度 <color=#ffffff>{currentTemperature.ToString("F1")}</color> ℃");
        }
    }
}
