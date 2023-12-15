using System;
using UnityEngine;

//----------------------------------------------------------------
//  ゲームシーンの全体的な進行を管理する
//----------------------------------------------------------------
public class SceneGameManager : MonoBehaviour
{
    //シングルトンインスタンス
    private static SceneGameManager instance;

    //----------------------------------------------------------------
    //  ゲーム中の状態ステート
    //----------------------------------------------------------------
    public enum State
    {
        CountDown, // カウントダウン
        Playing,      //プレイ中
        Finish,       //終了
        TransitionTo, //次のシーンへ遷移
    }

    //現在のステート
    [SerializeField]
    public State state = 0;

    //ゲーム制限時間
    public const float MaxLimitTime = 40.0f;

    //残りゲーム時間
    public float RemainedTime = MaxLimitTime;

    private float oneSecCountDown;
    public float timeToWeatherChange;
    private float timeToWeatherChangeCountDown;
    public float timeToBathChange;
    private float timeToBathChangeCountDown;

    private BathChange bathChange;

    //風呂管理オブジェクト
    private BathManager bathManager = null;

    //天候オブジェクト
    private Weather weather = null;

    //人間インスタンス
    [SerializeField]
    private Human human = null;


    public bool IsScoreUpdated;

    // Start is called before the first frame update
    private void Start()
    {
        instance = this;

        //制限時間初期化
        RemainedTime = MaxLimitTime;

        //風呂管理オブジェクトを取得
        TryGetComponent(out bathManager);

        //天候オブジェクトを取得
        TryGetComponent(out weather);

        TryGetComponent(out bathChange);

        timeToBathChangeCountDown = timeToBathChange;
        timeToWeatherChangeCountDown = timeToWeatherChange;

        state = State.CountDown;

        AudioManager.GetInstance().PlayGameSceneBGM();

        IsScoreUpdated = false;
    }

    // Update is called once per frame
    private void Update()
    {
        switch (state)
        {
            case State.CountDown:

                break;

            case State.Playing:  //プレイ中

                if (timeToBathChangeCountDown > 0)
                {
                    timeToBathChangeCountDown -= Time.deltaTime;
                }
                else
                {
                    bathChange.ChangeBathChangeType();
                    timeToBathChangeCountDown = timeToBathChange;
                }

                if (timeToWeatherChangeCountDown > 0)
                {
                    timeToWeatherChangeCountDown -= Time.deltaTime;
                }
                else
                {
                    weather.ChangeWeatherType();
                    timeToWeatherChangeCountDown = timeToWeatherChange;
                }

                //タイムアップ処理
                if (RemainedTime <= 0.0f)
                {
                    //スコアの更新
                    UpdateScores();

                    //セーブ
                    ApplicationManager.GetInstance().StoreSaveData();

                    ++state;
                    break;
                }

                //残り時間減少
                RemainedTime -= Time.deltaTime;

                //人間への影響を与える
                human.temperature = bathManager.CurrentTemperature;

                if (oneSecCountDown > 0)
                {
                    oneSecCountDown -= Time.deltaTime;
                }
                else
                { 
                    //天候による風呂への温度影響処理
                    weather.AffectWeather();

                    oneSecCountDown = 1;
                }

                break;

            case State.Finish: //タイムアップ

                break;

            case State.TransitionTo: //シーン遷移処理

                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    //----------------------------------------------------------------
    //  スコアの更新を行う
    //----------------------------------------------------------------
    private void UpdateScores()
    {
        //インスタンス取得
        var application = ApplicationManager.GetInstance();

        //スコアランクを判定
        application.JudgeRank(bathManager);

        //セーブデータの参照を取得
        ref var data = ref ApplicationManager.GetInstance().Data;
        
        //新しいランキングデータを追加
        data.RankingList.Add(application.LastData);

        //精度が高い順に並び変える
        data.RankingList.Sort((lhs, rhs) =>
        {
            var lhsDiff = MathF.Abs(lhs.Temperature - bathManager.TargetTemeprature);
            var rhsDiff = MathF.Abs(rhs.Temperature - bathManager.TargetTemeprature);

            return lhsDiff < rhsDiff ? -1 : 1;
        });

        //MaxListNumを超えていた場合一番ダメなデータを破棄する
        if (data.RankingList.Count > ApplicationManager.SaveData.MaxListNum)
        {
            data.RankingList.RemoveAt(data.RankingList.Count - 1);
        }

        IsScoreUpdated = true;
        Debug.Log("Score Updated!");
    }

    //----------------------------------------------------------------
    //  風呂オブジェクトを取得
    //----------------------------------------------------------------
    public BathManager GetBathManager() => bathManager;

    //----------------------------------------------------------------
    //  天気オブジェクトを取得
    //----------------------------------------------------------------
    public Weather GetWeather() => weather;

    public static SceneGameManager GetInstance() => instance;
}