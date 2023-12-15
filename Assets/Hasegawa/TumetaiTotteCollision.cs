using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems; // using��ǉ�
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
        // �����J�n�@�t���O�𗧂Ă�
        clickRotationScript._isPushed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // �����I���@�t���O�𗎂Ƃ�
        clickRotationScript._isPushed = false;
    }
}
