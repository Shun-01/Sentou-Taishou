using System;
using UnityEngine;

//----------------------------------------------------------------
//  �Q�[���V�[���̑S�̓I�Ȑi�s���Ǘ�����
//----------------------------------------------------------------
public class SceneGameManager : MonoBehaviour
{
    //�V���O���g���C���X�^���X
    private static SceneGameManager instance;

    //----------------------------------------------------------------
    //  �Q�[�����̏�ԃX�e�[�g
    //----------------------------------------------------------------
    public enum State
    {
        CountDown, // �J�E���g�_�E��
        Playing,      //�v���C��
        Finish,       //�I��
        TransitionTo, //���̃V�[���֑J��
    }

    //���݂̃X�e�[�g
    [SerializeField]
    public State state = 0;

    //�Q�[����������
    public const float MaxLimitTime = 40.0f;

    //�c��Q�[������
    public float RemainedTime = MaxLimitTime;

    private float oneSecCountDown;
    public float timeToWeatherChange;
    private float timeToWeatherChangeCountDown;
    public float timeToBathChange;
    private float timeToBathChangeCountDown;

    private BathChange bathChange;

    //���C�Ǘ��I�u�W�F�N�g
    private BathManager bathManager = null;

    //�V��I�u�W�F�N�g
    private Weather weather = null;

    //�l�ԃC���X�^���X
    [SerializeField]
    private Human human = null;


    public bool IsScoreUpdated;

    // Start is called before the first frame update
    private void Start()
    {
        instance = this;

        //�������ԏ�����
        RemainedTime = MaxLimitTime;

        //���C�Ǘ��I�u�W�F�N�g���擾
        TryGetComponent(out bathManager);

        //�V��I�u�W�F�N�g���擾
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

            case State.Playing:  //�v���C��

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

                //�^�C���A�b�v����
                if (RemainedTime <= 0.0f)
                {
                    //�X�R�A�̍X�V
                    UpdateScores();

                    //�Z�[�u
                    ApplicationManager.GetInstance().StoreSaveData();

                    ++state;
                    break;
                }

                //�c�莞�Ԍ���
                RemainedTime -= Time.deltaTime;

                //�l�Ԃւ̉e����^����
                human.temperature = bathManager.CurrentTemperature;

                if (oneSecCountDown > 0)
                {
                    oneSecCountDown -= Time.deltaTime;
                }
                else
                { 
                    //�V��ɂ�镗�C�ւ̉��x�e������
                    weather.AffectWeather();

                    oneSecCountDown = 1;
                }

                break;

            case State.Finish: //�^�C���A�b�v

                break;

            case State.TransitionTo: //�V�[���J�ڏ���

                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    //----------------------------------------------------------------
    //  �X�R�A�̍X�V���s��
    //----------------------------------------------------------------
    private void UpdateScores()
    {
        //�C���X�^���X�擾
        var application = ApplicationManager.GetInstance();

        //�X�R�A�����N�𔻒�
        application.JudgeRank(bathManager);

        //�Z�[�u�f�[�^�̎Q�Ƃ��擾
        ref var data = ref ApplicationManager.GetInstance().Data;
        
        //�V���������L���O�f�[�^��ǉ�
        data.RankingList.Add(application.LastData);

        //���x���������ɕ��ѕς���
        data.RankingList.Sort((lhs, rhs) =>
        {
            var lhsDiff = MathF.Abs(lhs.Temperature - bathManager.TargetTemeprature);
            var rhsDiff = MathF.Abs(rhs.Temperature - bathManager.TargetTemeprature);

            return lhsDiff < rhsDiff ? -1 : 1;
        });

        //MaxListNum�𒴂��Ă����ꍇ��ԃ_���ȃf�[�^��j������
        if (data.RankingList.Count > ApplicationManager.SaveData.MaxListNum)
        {
            data.RankingList.RemoveAt(data.RankingList.Count - 1);
        }

        IsScoreUpdated = true;
        Debug.Log("Score Updated!");
    }

    //----------------------------------------------------------------
    //  ���C�I�u�W�F�N�g���擾
    //----------------------------------------------------------------
    public BathManager GetBathManager() => bathManager;

    //----------------------------------------------------------------
    //  �V�C�I�u�W�F�N�g���擾
    //----------------------------------------------------------------
    public Weather GetWeather() => weather;

    public static SceneGameManager GetInstance() => instance;
}