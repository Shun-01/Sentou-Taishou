using System.Globalization;
using TMPro;
using UnityEngine;

//----------------------------------------------------------------
//  �v���t�@�u�@"ScorePresenter"�ɃA�^�b�`����X�N���v�g
//----------------------------------------------------------------
public class ScorePresenter : MonoBehaviour
{
    //�����N�\����TMP
    [SerializeField]
    private TMP_Text RankTMP = null;

    //�X�R�A�\����TMP
    [SerializeField]
    private TMP_Text ScoreTMP = null;

    // Start is called before the first frame update
    private void Start()
    {
        Debug.Assert(RankTMP != null, $"{name}��ScorePresenter�X�N���v�g�ɂ���RankTMP�v���p�e�B��null�ł���");
        Debug.Assert(ScoreTMP != null, $"{name}��ScorePresenter�X�N���v�g�ɂ���ScoreTMP�v���p�e�B��null�ł���");
    }

    // Update is called once per frame
    private void Update()
    {
    }

    //----------------------------------------------------------------
    //  �����N�\����TMP���擾
    //----------------------------------------------------------------
    public TMP_Text GetRankTMP() => RankTMP;

    //----------------------------------------------------------------
    //  �X�R�A�\����TMP���擾
    //----------------------------------------------------------------
    public TMP_Text GetScoreTMP() => ScoreTMP;

    //----------------------------------------------------------------
    //  RankingData���當�����ݒ肷��
    //  [ApplicationManager.RankingData] data : ���f�[�^
    //----------------------------------------------------------------
    public void SetTextFromScores(ApplicationManager.RankingData data)
    {
        GetRankTMP().text = data.Rank.ToString();
        GetScoreTMP().text = data.Temperature.ToString("F1");
    }

    //----------------------------------------------------------------
    //  �󔒃f�[�^��ݒ肷��
    //----------------------------------------------------------------
    public void SetNullText()
    {
        GetRankTMP().text = "-";
        GetScoreTMP().text = "---";
    }



}