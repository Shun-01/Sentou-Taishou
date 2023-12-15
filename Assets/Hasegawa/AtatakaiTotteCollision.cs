using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems; // using��ǉ�
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
        // �����J�n�@�t���O�𗧂Ă�
        AtatakaiClickRotationScript._isAtatakaiPushed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // �����I���@�t���O�𗎂Ƃ�
        AtatakaiClickRotationScript._isAtatakaiPushed = false;
    }
}
