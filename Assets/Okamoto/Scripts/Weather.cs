using System.Collections.Generic;
using UnityEngine;

public class Weather : MonoBehaviour
{
    //風呂の管理オブジェクト
    private BathManager bathManager = null;

    //天候タイプ列挙子
    public enum WeatherType
    {
        Heatwave, //猛暑
        Sunny,    //晴れ
        Cloudy,   //曇り
        Rainy,    //雨
        Snowy,    //雪
    }

    //天候による影響値リスト
    [SerializeField, Header("天候による温度への影響値を指定します")]
    private List<float> InfluencePowerList;

    //天候固定フラグ
    [SerializeField, Header("天候を曇りに固定にします")]
    private bool FixedWeatherActivation;

    //最初が曇りから始まるかを指定します
    [SerializeField, Header("開始時を曇りにします")]
    private bool BeginWeatherCloudy;

    //天候ランダムフラグ
    [SerializeField, Header("天候がランダムに変わるようにします")]
    private bool RandomWeatherActivation;

    //現在の天候
    [SerializeField]
    private WeatherType CurrentWeather = WeatherType.Cloudy;

    // Start is called before the first frame update
    private void Start()
    {
        //天候の数と影響値リストは一致している必要がある
        Debug.Assert(Utility.GetEnumeratorLength<WeatherType>() == InfluencePowerList.Count);

        //BathManager取得
        TryGetComponent(out bathManager);
        Debug.Assert(bathManager);
    }

    // Update is called once per frame
    private void Update()
    {
    }

    //----------------------------------------------------------------
    //  天候に応じた温度への影響を与える
    //----------------------------------------------------------------

    public int GetWeatherType()
    {
        return (int)CurrentWeather;
    }

    public void ChangeWeatherType()
    {
        WeatherType lastWeather = CurrentWeather;
        do
        {
            CurrentWeather = (WeatherType)Random.Range(0, System.Enum.GetNames(typeof(WeatherType)).Length);
        } while (CurrentWeather == lastWeather);        
        
    }

    public void AffectWeather()
    {
        //とりあえず今はインクリメント
        bathManager.CurrentTemperature += InfluencePowerList[(int)CurrentWeather];
    }

}