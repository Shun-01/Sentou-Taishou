using UnityEngine;

//----------------------------------------------------------------
//  オーディオ関連管理
//----------------------------------------------------------------
public class AudioManager : MonoBehaviour
{
    //シングルトンインスタンス
    private static AudioManager instance = null;

    //オーディオソース
    public AudioSource audioSource;

    [SerializeField, Header("Audio clips")]
    private AudioClip titleAudio;
    [SerializeField]
    private AudioClip gameSceneAudio;
    [SerializeField]
    private AudioClip resultAudio;

    //----------------------------------------------------------------
    //  開始した瞬間に実行
    //----------------------------------------------------------------
    private void Awake()
    {
        //シングルトン処理
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);

            //オーディオソース取得
            TryGetComponent(out audioSource);
        }
        else
        {
            //複数存在した場合は破棄する
            Destroy(this);
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }

    //----------------------------------------------------------------
    //  インスタンス取得
    //----------------------------------------------------------------
    public static AudioManager GetInstance() => instance;

    //----------------------------------------------------------------
    //  BGMを再生する
    //  [AudioClip] clip : 再生するクリップ
    //  [bool] forceChange : 強制的にBGMを再生する
    //----------------------------------------------------------------

    public void PlayTitleSceneBGM()
    {
        PlayBGM(titleAudio, true);
    }

    public void PlayResultSceneBGM()
    {
        PlayBGM(resultAudio, false);
    }


    public void PlayGameSceneBGM()
    {
        PlayBGM(gameSceneAudio, true);
    }

    public void PlayBGM(AudioClip clip, bool forceChange = false)
    {
        if (audioSource.clip != null)
        {
            if (forceChange || audioSource.clip.name != clip.name)
            {
                audioSource.clip = clip;
                audioSource.Play();
            }
        }
        else
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    //----------------------------------------------------------------
    //  SEをプレイする
    //  [AudioClip] clip : 再生するクリップ
    //----------------------------------------------------------------
    public void PlaySE(AudioClip clip)
    {
        audioSource.PlayOneShot(clip, 1.0f);
    }
}