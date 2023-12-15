using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems; // usingを追加
using UnityEngine;

public class AtatakaiTotteCollision : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private GameObject objAtatakaiMotite;
    AtatakaiClickRotation AtatakaiClickRotationScript;

    // Start is called before the first frame update
    void Start()
    {
        objAtatakaiMotite = GameObject.Find("AtatakaiMotite");
        AtatakaiClickRotationScript = objAtatakaiMotite.GetComponent<AtatakaiClickRotation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // 押下開始　フラグを立てる
        AtatakaiClickRotationScript._isAtatakaiPushed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // 押下終了　フラグを落とす
        AtatakaiClickRotationScript._isAtatakaiPushed = false;
    }
}
