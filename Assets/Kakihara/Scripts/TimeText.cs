using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeText : MonoBehaviour
{
    public float currentTime;

    private TMP_Text timeText;

    void Start()
    {
        timeText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        timeText.text = "残り時間 " + currentTime + " 秒";
    }
}
