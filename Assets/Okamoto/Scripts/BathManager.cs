using System;
using UnityEngine;

//----------------------------------------------------------------
//  風呂管理
//----------------------------------------------------------------
public class BathManager : MonoBehaviour
{
    //ゲーム開始時にランダムに決まる最大の温度
    [SerializeField]
    private float BeginTemperatureMax = 30.0f;

    //ゲーム開始時にランダムに決まる最小の温度
    [SerializeField]
    private float BeginTemperatureMin = 50.0f;

    //目標温度(40.0f定数)
    public readonly float TargetTemeprature = 40.0f;

    //現在の温度
    [SerializeField]
    public float CurrentTemperature;

    // Start is called before the first frame update
    private void Start()
    {
        //開始時の温度を決定
        CurrentTemperature = UnityEngine.Random.Range(BeginTemperatureMin, BeginTemperatureMax);
    }

    // Update is called once per frame
    private void Update()
    {
        //CurrentTemperature = GetRoundedCurrentTemperature();
    }

    //----------------------------------------------------------------
    //  少数第１位で四捨五入をした温度を取得
    //----------------------------------------------------------------
    public float GetRoundedCurrentTemperature() => MathF.Round(CurrentTemperature, 1, MidpointRounding.AwayFromZero);
}