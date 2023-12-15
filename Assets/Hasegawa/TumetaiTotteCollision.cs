using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems; // usingを追加
using UnityEngine;

public class TumetaiTotteCollision : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private GameObject objTumetaiMotite;
    ClickRotation clickRotationScript;

    // Start is called before the first frame update
    void Start()
    {
        objTumetaiMotite = GameObject.Find("TumetaiMotite");
        clickRotationScript = objTumetaiMotite.GetComponent<ClickRotation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // 押下開始　フラグを立てる
        clickRotationScript._isPushed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // 押下終了　フラグを落とす
        clickRotationScript._isPushed = false;
    }
}
