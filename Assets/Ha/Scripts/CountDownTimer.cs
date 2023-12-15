using TMPro;
using UnityEngine;

public class CountDownTimer : MonoBehaviour
{
    public SceneChange sceneChangeManager;
    private TMP_Text timerTextCMP;
    private float countDownTime = 40f;
    private int displayTime;

    private bool _isActive = true;

    // Start is called before the first frame update
    private void Start()
    {
        timerTextCMP = gameObject.GetComponent<TMP_Text>();
        _isActive = false;

        countDownTime = 40.0f;
        timerTextCMP.text = "残り時間 " + countDownTime.ToString("F1") + "秒";
    }

    // Update is called once per frame
    private void Update()
    {
        if (_isActive == false)
        {
            countDownTime = 40.0f;
            return;
        }

        if (countDownTime > 10f)
        {
            countDownTime -= Time.deltaTime;
            timerTextCMP.text = "残り時間 " + countDownTime.ToString("F1") + "秒";
        }
        else if (countDownTime > 0f && countDownTime <= 10f)
        {
            countDownTime -= Time.deltaTime;
            timerTextCMP.text = "残り時間 " + $"<color=#ff0000>{countDownTime.ToString("F1")}</color>" + " 秒";
        }
        else
        {
            if (SceneGameManager.GetInstance().IsScoreUpdated == true)
            {
                sceneChangeManager.ToResult();
            }
        }
    }

    public void setIsActive(bool isActive)
    {
        _isActive = isActive;
    }
}