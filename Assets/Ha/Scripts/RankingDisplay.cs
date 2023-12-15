using UnityEngine;

public class RankingDisplay : MonoBehaviour
{
    //ゲーム時のスコアを表示するオブジェクトのScorePresenter
    [SerializeField, Header("LastScoreゲームオブジェクトを設定")]
    private ScorePresenter LastScorePresenter = null;

    //RankingBoardのゲームオブジェクト
    [SerializeField, Header("RankingBoardゲームオブジェクト")]
    private GameObject RankingBoardGameObject;

    //プレファブのScorePresenterを設定する
    [SerializeField, Header("ScorePresenterプレファブ")]
    private GameObject ScorePresenterPrefab;

    //ランキングスコアの表示開始位置（左上）
    [SerializeField, Header("ランキングスコアの表示開始位置（左上）")]
    private Vector2 RankingDisplayPosition;

    //ランキングスコアの表示オフセット
    [SerializeField, Header("ランキングスコアの表示オフセット")]
    private Vector2 RankingDisplayOffset;

    // Start is called before the first frame update
    private void Start()
    {
        //前回のゲームのスコアを表示
        DisplayLastScores();

        //ランキングデータを表示
        DisplayRanking();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    //----------------------------------------------------------------
    //  前回のゲームのスコアを表示する
    //----------------------------------------------------------------
    private void DisplayLastScores() => LastScorePresenter.SetTextFromScores(ApplicationManager.GetInstance().LastData);

    //----------------------------------------------------------------
    //  ランキングデータを表示する
    //----------------------------------------------------------------
    private void DisplayRanking()
    {
        //セーブデータインスタンス取得
        var data = ApplicationManager.GetInstance().Data;

        var max = ApplicationManager.SaveData.MaxListNum;

        //表示してた数
        var numDisplayed = 0;

        //存在しているランキングデータの数
        var numExists = data.RankingList.Count;

        //有効データの表示
        for (var i = 0; i < numExists; ++i)
        {
            //新しいオブジェクトを生成
            var sp = Instantiate(ScorePresenterPrefab);

            //RankingBoardの子に設定
            sp.transform.parent = RankingBoardGameObject.transform;

            //座標をずらしていく
            sp.transform.localPosition = RankingDisplayPosition + (RankingDisplayOffset * numDisplayed);

            //ScorePresenterコンポーネントを取得
            sp.TryGetComponent(out ScorePresenter presenter);
            presenter.SetTextFromScores(data.RankingList[i]);

            //表示数を更新
            ++numDisplayed;
        }

        //無効データの表示
        for (var i = 0; i < max - numExists; ++i)
        {
            //新しいオブジェクトを生成
            var sp = Instantiate(ScorePresenterPrefab);

            //RankingBoardの子に設定
            sp.transform.parent = RankingBoardGameObject.transform;

            //座標をずらしていく
            sp.transform.localPosition = RankingDisplayPosition + (RankingDisplayOffset * numDisplayed);

            //ScorePresenterコンポーネントを取得
            sp.TryGetComponent(out ScorePresenter presenter);
            presenter.SetNullText();

            //表示数を更新
            ++numDisplayed;
        }
    }
}