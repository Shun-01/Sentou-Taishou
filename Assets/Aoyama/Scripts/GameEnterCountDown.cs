using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameEnterCountDown : MonoBehaviour
{
    [SerializeField]
    private TMP_Text CountDownMesh = null;

    [SerializeField]
    private Image GlayoutImage = null;

    [SerializeField]
    private GameObject Owner = null;

    [SerializeField]
    private CountDownTimer GameTimer = null;

    [SerializeField]
    private float StartTime = 3.0f;

    [SerializeField]
    private float GlayAlpha = 0.8f;

    private float _CurTimer = 0.0f;
    private float _PrevTimer = 0.0f;

    private string _DispText = string.Empty;
    private float _ImageAlpha = 0.8f;

    private bool _IsActive = true;

    // Start is called before the first frame update
    void Start()
    {
        _CurTimer = _PrevTimer = StartTime;
        _DispText = string.Empty;
        _ImageAlpha = GlayAlpha;
        _IsActive = true;

        GameTimer.setIsActive(false);

        Owner.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_IsActive)
            return;

        _PrevTimer = _CurTimer;
        _CurTimer -= Time.deltaTime;

        int prevTimeInt = (int)(_PrevTimer);
        int curTimeInt = (int)(_CurTimer);

        if (_CurTimer > 0.0f)
        {
            if (prevTimeInt != curTimeInt)
            {
                var dispInt = curTimeInt + 1;
                if (dispInt > 0)
                {
                    _DispText = (dispInt).ToString();
                }
            }
        }
        else if (0.0f >= _CurTimer  && _CurTimer >= -1.0)
        {
            _DispText = "Start !!!";
        }
        else
        {
            setactive(false);
        }

        CountDownMesh.text = _DispText;
        var col = GlayoutImage.color;
        GlayoutImage.color = new Color(col.r, col.g, col.b, _ImageAlpha);
    }

    public void setactive(bool isActive)
    {
        _IsActive = isActive;
        if (_IsActive)
        {
            _ImageAlpha = GlayAlpha;
            GameTimer.setIsActive(false);
        }
        else 
        {

            SceneGameManager.GetInstance().state++;
            _DispText = string.Empty;
            _ImageAlpha = 0.0f;

            GameTimer.setIsActive(true);
            Owner.SetActive(false);
        }
    }
}
