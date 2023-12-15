using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JagutiTumetaiMizu : MonoBehaviour
{
    private int time = 0;
    [SerializeField] int tumetaiInterval;

    private GameObject TumetaiMotite;
    ClickRotation clickRotation;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        TumetaiMotite = GameObject.Find("TumetaiMotite");
        clickRotation = TumetaiMotite.GetComponent<ClickRotation>();
    }

    // Update is called once per frame
    void Update()
    {
        ++time;
        if (clickRotation._isPushed && time > tumetaiInterval && clickRotation.TumetaAngle < 0.0f)
        {
            GameObject targetPrefab = (GameObject)Resources.Load("TumetaiMizu");
            Vector3 SyashutuPos = gameObject.transform.position;
            SyashutuPos.y = SyashutuPos.y - 0.5f;
            GameObject objTumetaiMizu = Instantiate(targetPrefab, SyashutuPos, Quaternion.identity);
            objTumetaiMizu.name = "TumetaiMizu";
            time = 0;
        }
    }
}
