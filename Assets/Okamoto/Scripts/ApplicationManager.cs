using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using UnityEngine;

//----------------------------------------------------------------
//  �Q�[���A�v���P�[�V�����S�̂��Ǘ�����I�u�W�F�N�g�X�N���v�g
//----------------------------------------------------------------
public class ApplicationManager : MonoBehaviour
{
    //�V���O���g���C���X�^���X
    private static ApplicationManager instance = null;

    //�Œ�t���[�����[�g�l
    [SerializeField, Header("�Œ肷��t���[�����[�g�̒l�ł�")]
    private int TargetFrameRate = 60;

    //�X�R�A�����N�񋓎q
    public enum Rank
    {
        S,
        A,
        B,
        C,
    }

    //���U���g��ʂł̃����N���߂悤�̌덷�l���X�g
    [SerializeField, Header("���U���g��ʂł̃����N�𔻒肷��ڕW���x����̋��e�͈͂ł�")]
    private List<float> ToleranceList;

    //�����L���O�f�[�^�\����
    public struct RankingData
    {
        //�����N
        public Rank Rank;

        //�ŏI���x
        public float Temperature;

        //----------------------------------------------------------------
        //  �R���X�g���N�^
        //----------------------------------------------------------------
        private RankingData(Rank rank, float temperature)
        {
            Temperature = temperature;
            Rank = rank;
        }
    }

    //���U���g��ʂł̃X�R�A�f�[�^
    public RankingData LastData = new RankingData();

    //�Z�[�u�f�[�^�\����
    [DataContract]
    public struct SaveData
    {
        //�ۑ���̃p�X
        public static readonly string StorePath = "SaveData.xml";

        //�����L���O���ő�ێ���
        public static readonly int MaxListNum = 5;

        //�����L���O�z��
        [DataMember]
        public List<RankingData> RankingList;
    }

    //�Q�[���̃Z�[�u�f�[�^
    [SerializeField]
    public SaveData Data;

    //----------------------------------------------------------------
    //  �J�n�����u�ԂɎ��s
    //----------------------------------------------------------------
    private void Awake()
    {
        if (instance == null)
        {
            //Rank�̐��ƃ����N�ϓ��͈͒l�̔z��͈�v���Ă���K�v������
            Debug.Assert(Utility.GetEnumeratorLength<Rank>() == (ToleranceList.Count + 1));

            //��΂̗B�ꑶ�݂Ƃ��Đݒ�
            instance = this;

            //��j���I�u�W�F�N�g�ɐݒ�
            DontDestroyOnLoad(this);

            //FPS�Œ�
            Application.targetFrameRate = TargetFrameRate;

            //�Z�[�u�f�[�^�̃��[�h
            Data = new SaveData();
            Data.RankingList = new List<RankingData>();

            LoadSaveData();
        }
        else
        {
            //�������݂��Ă����ꍇ�͔j��
            Destroy(this);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        //Escape�L�[�ł̃Q�[���I������
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //�ۑ����s��
            StoreSaveData();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }

    //----------------------------------------------------------------
    //  �Z�[�u�f�[�^��ۑ�����
    //----------------------------------------------------------------
    public void StoreSaveData()
    {
        Utility.Serialize(Data, SaveData.StorePath);
        Debug.Log("Save data stored");

        //�ŐV�łɐݒ�
        LoadSaveData();
    }

    //----------------------------------------------------------------
    //  �Z�[�u�f�[�^��ǂݍ���
    //----------------------------------------------------------------
    public void LoadSaveData()
    {
        //�����t�@�C�������݂��Ȃ�������Z�[�u���Đ�������
        if (!File.Exists(SaveData.StorePath))
        {
            StoreSaveData();
        }

        Data = Utility.Deserialize<SaveData>(SaveData.StorePath);

        Debug.Log("Save data loaded");
    }

    //----------------------------------------------------------------
    //  �X�R�A�I�u�W�F�N�g�𐶐�����
    //  [BathManager] bathManager : ���C�̃I�u�W�F�N�g
    //----------------------------------------------------------------
    public void JudgeRank(BathManager bathManager)
    {
        //��Βl�Ŕ���
        float difference = Mathf.Abs(bathManager.CurrentTemperature - bathManager.TargetTemeprature);

        //S����i�������Čv�Z����
        LastData.Rank = Rank.S;

        //�X�R�A�����N���v�Z
        foreach (var _ in ToleranceList.TakeWhile(tolerance => !(tolerance > difference)))
        {
            LastData.Rank++;
        }

        //���x��ۑ�(�؂�̂Ă�)
        LastData.Temperature = bathManager.GetRoundedCurrentTemperature();
    }

    //----------------------------------------------------------------
    //  �C���X�^���X�擾
    //----------------------------------------------------------------
    public static ApplicationManager GetInstance() => instance;
}