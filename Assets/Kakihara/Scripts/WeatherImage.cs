using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeatherImage : MonoBehaviour
{
    private Image image;
    [SerializeField]
    private Weather weatherManager;
    public enum WEATHER_TYPE
    {
        VERY_HOT,
        SUNNY,
        CLOUDY,
        RAINY,
        SNOWY
    }

    private WEATHER_TYPE currentWeather;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        currentWeather = (WEATHER_TYPE)weatherManager.GetWeatherType();
        switch(currentWeather)
        {
            case WEATHER_TYPE.VERY_HOT:
                image.sprite = Resources.Load<Sprite>("very_hot");
                break;
            
            case WEATHER_TYPE.SUNNY:
                image.sprite = Resources.Load<Sprite>("sunny");
                break;
                
            case WEATHER_TYPE.CLOUDY:
                image.sprite = Resources.Load<Sprite>("cloudy");
                break;
                
            case WEATHER_TYPE.RAINY:
                image.sprite = Resources.Load<Sprite>("rainy");
                break;
                
            case WEATHER_TYPE.SNOWY:
                image.sprite = Resources.Load<Sprite>("snowy");
                break;
        }
    }
}
