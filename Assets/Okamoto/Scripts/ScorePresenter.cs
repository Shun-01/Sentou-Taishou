using System.Globalization;
using TMPro;
using UnityEngine;

//----------------------------------------------------------------
//  プレファブ　"ScorePresenter"にアタッチするスクリプト
//----------------------------------------------------------------
public class ScorePresenter : MonoBehaviour
{
    //ランク表示のTMP
    [SerializeField]
    private TMP_Text RankTMP = null;

    //スコア表示のTMP
    [SerializeField]
    private TMP_Text ScoreTMP = null;

    // Start is called before the first frame update
    private void Start()
    {
        Debug.Assert(RankTMP != null, $"{name}のScorePresenterスクリプトにあるRankTMPプロパティがnullでした");
        Debug.Assert(ScoreTMP != null, $"{name}のScorePresenterスクリプトにあるScoreTMPプロパティがnullでした");
    }

    // Update is called once per frame
    private void Update()
    {
    }

    //----------------------------------------------------------------
    //  ランク表示のTMPを取得
    //----------------------------------------------------------------
    public TMP_Text GetRankTMP() => RankTMP;

    //----------------------------------------------------------------
    //  スコア表示のTMPを取得
    //----------------------------------------------------------------
    public TMP_Text GetScoreTMP() => ScoreTMP;

    //----------------------------------------------------------------
    //  RankingDataから文字列を設定する
    //  [ApplicationManager.RankingData] data : 元データ
    //----------------------------------------------------------------
    public void SetTextFromScores(ApplicationManager.RankingData data)
    {
        GetRankTMP().text = data.Rank.ToString();
        GetScoreTMP().text = data.Temperature.ToString("F1");
    }

    //----------------------------------------------------------------
    //  空白データを設定する
    //----------------------------------------------------------------
    public void SetNullText()
    {
        GetRankTMP().text = "-";
        GetScoreTMP().text = "---";
    }



}