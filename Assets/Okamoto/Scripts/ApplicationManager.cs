using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using UnityEngine;

//----------------------------------------------------------------
//  ゲームアプリケーション全体を管理するオブジェクトスクリプト
//----------------------------------------------------------------
public class ApplicationManager : MonoBehaviour
{
    //シングルトンインスタンス
    private static ApplicationManager instance = null;

    //固定フレームレート値
    [SerializeField, Header("固定するフレームレートの値です")]
    private int TargetFrameRate = 60;

    //スコアランク列挙子
    public enum Rank
    {
        S,
        A,
        B,
        C,
    }

    //リザルト画面でのランク決めようの誤差値リスト
    [SerializeField, Header("リザルト画面でのランクを判定する目標温度からの許容範囲です")]
    private List<float> ToleranceList;

    //ランキングデータ構造体
    public struct RankingData
    {
        //ランク
        public Rank Rank;

        //最終温度
        public float Temperature;

        //----------------------------------------------------------------
        //  コンストラクタ
        //----------------------------------------------------------------
        private RankingData(Rank rank, float temperature)
        {
            Temperature = temperature;
            Rank = rank;
        }
    }

    //リザルト画面でのスコアデータ
    public RankingData LastData = new RankingData();

    //セーブデータ構造体
    [DataContract]
    public struct SaveData
    {
        //保存先のパス
        public static readonly string StorePath = "SaveData.xml";

        //ランキングを最大保持数
        public static readonly int MaxListNum = 5;

        //ランキング配列
        [DataMember]
        public List<RankingData> RankingList;
    }

    //ゲームのセーブデータ
    [SerializeField]
    public SaveData Data;

    //----------------------------------------------------------------
    //  開始した瞬間に実行
    //----------------------------------------------------------------
    private void Awake()
    {
        if (instance == null)
        {
            //Rankの数とランク変動範囲値の配列は一致している必要がある
            Debug.Assert(Utility.GetEnumeratorLength<Rank>() == (ToleranceList.Count + 1));

            //絶対の唯一存在として設定
            instance = this;

            //非破棄オブジェクトに設定
            DontDestroyOnLoad(this);

            //FPS固定
            Application.targetFrameRate = TargetFrameRate;

            //セーブデータのロード
            Data = new SaveData();
            Data.RankingList = new List<RankingData>();

            LoadSaveData();
        }
        else
        {
            //複数存在していた場合は破棄
            Destroy(this);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        //Escapeキーでのゲーム終了処理
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //保存を行う
            StoreSaveData();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }

    //----------------------------------------------------------------
    //  セーブデータを保存する
    //----------------------------------------------------------------
    public void StoreSaveData()
    {
        Utility.Serialize(Data, SaveData.StorePath);
        Debug.Log("Save data stored");

        //最新版に設定
        LoadSaveData();
    }

    //----------------------------------------------------------------
    //  セーブデータを読み込む
    //----------------------------------------------------------------
    public void LoadSaveData()
    {
        //もしファイルが存在しなかったらセーブして生成する
        if (!File.Exists(SaveData.StorePath))
        {
            StoreSaveData();
        }

        Data = Utility.Deserialize<SaveData>(SaveData.StorePath);

        Debug.Log("Save data loaded");
    }

    //----------------------------------------------------------------
    //  スコアオブジェクトを生成する
    //  [BathManager] bathManager : 風呂のオブジェクト
    //----------------------------------------------------------------
    public void JudgeRank(BathManager bathManager)
    {
        //絶対値で判定
        float difference = Mathf.Abs(bathManager.CurrentTemperature - bathManager.TargetTemeprature);

        //Sから格下げして計算する
        LastData.Rank = Rank.S;

        //スコアランクを計算
        foreach (var _ in ToleranceList.TakeWhile(tolerance => !(tolerance > difference)))
        {
            LastData.Rank++;
        }

        //温度を保存(切り捨てる)
        LastData.Temperature = bathManager.GetRoundedCurrentTemperature();
    }

    //----------------------------------------------------------------
    //  インスタンス取得
    //----------------------------------------------------------------
    public static ApplicationManager GetInstance() => instance;
}