using UnityEngine;

public class RankingDisplay : MonoBehaviour
{
    //�Q�[�����̃X�R�A��\������I�u�W�F�N�g��ScorePresenter
    [SerializeField, Header("LastScore�Q�[���I�u�W�F�N�g��ݒ�")]
    private ScorePresenter LastScorePresenter = null;

    //RankingBoard�̃Q�[���I�u�W�F�N�g
    [SerializeField, Header("RankingBoard�Q�[���I�u�W�F�N�g")]
    private GameObject RankingBoardGameObject;

    //�v���t�@�u��ScorePresenter��ݒ肷��
    [SerializeField, Header("ScorePresenter�v���t�@�u")]
    private GameObject ScorePresenterPrefab;

    //�����L���O�X�R�A�̕\���J�n�ʒu�i����j
    [SerializeField, Header("�����L���O�X�R�A�̕\���J�n�ʒu�i����j")]
    private Vector2 RankingDisplayPosition;

    //�����L���O�X�R�A�̕\���I�t�Z�b�g
    [SerializeField, Header("�����L���O�X�R�A�̕\���I�t�Z�b�g")]
    private Vector2 RankingDisplayOffset;

    // Start is called before the first frame update
    private void Start()
    {
        //�O��̃Q�[���̃X�R�A��\��
        DisplayLastScores();

        //�����L���O�f�[�^��\��
        DisplayRanking();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    //----------------------------------------------------------------
    //  �O��̃Q�[���̃X�R�A��\������
    //----------------------------------------------------------------
    private void DisplayLastScores() => LastScorePresenter.SetTextFromScores(ApplicationManager.GetInstance().LastData);

    //----------------------------------------------------------------
    //  �����L���O�f�[�^��\������
    //----------------------------------------------------------------
    private void DisplayRanking()
    {
        //�Z�[�u�f�[�^�C���X�^���X�擾
        var data = ApplicationManager.GetInstance().Data;

        var max = ApplicationManager.SaveData.MaxListNum;

        //�\�����Ă���
        var numDisplayed = 0;

        //���݂��Ă��郉���L���O�f�[�^�̐�
        var numExists = data.RankingList.Count;

        //�L���f�[�^�̕\��
        for (var i = 0; i < numExists; ++i)
        {
            //�V�����I�u�W�F�N�g�𐶐�
            var sp = Instantiate(ScorePresenterPrefab);

            //RankingBoard�̎q�ɐݒ�
            sp.transform.parent = RankingBoardGameObject.transform;

            //���W�����炵�Ă���
            sp.transform.localPosition = RankingDisplayPosition + (RankingDisplayOffset * numDisplayed);

            //ScorePresenter�R���|�[�l���g���擾
            sp.TryGetComponent(out ScorePresenter presenter);
            presenter.SetTextFromScores(data.RankingList[i]);

            //�\�������X�V
            ++numDisplayed;
        }

        //�����f�[�^�̕\��
        for (var i = 0; i < max - numExists; ++i)
        {
            //�V�����I�u�W�F�N�g�𐶐�
            var sp = Instantiate(ScorePresenterPrefab);

            //RankingBoard�̎q�ɐݒ�
            sp.transform.parent = RankingBoardGameObject.transform;

            //���W�����炵�Ă���
            sp.transform.localPosition = RankingDisplayPosition + (RankingDisplayOffset * numDisplayed);

            //ScorePresenter�R���|�[�l���g���擾
            sp.TryGetComponent(out ScorePresenter presenter);
            presenter.SetNullText();

            //�\�������X�V
            ++numDisplayed;
        }
    }
}