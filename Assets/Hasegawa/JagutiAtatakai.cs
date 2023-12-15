using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JagutiAtatakai : MonoBehaviour
{
    private int time = 0;
    [SerializeField] int interval;

    private GameObject AtatakaiMotite;
    AtatakaiClickRotation atatakaiClickRotation;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        AtatakaiMotite = GameObject.Find("AtatakaiMotite");
        atatakaiClickRotation = AtatakaiMotite.GetComponent<AtatakaiClickRotation>();
    }

    // Update is called once per frame
    void Update()
    {
        ++time;
        if (atatakaiClickRotation._isAtatakaiPushed && time > interval && atatakaiClickRotation.AtatakaiAngle < 0.0f)
        {
            GameObject targetPrefab = (GameObject)Resources.Load("AtatakaiMizu");
            Vector3 SyashutuPos = gameObject.transform.position;
            SyashutuPos.y = SyashutuPos.y - 0.5f;
            GameObject objTumetaiMizu = Instantiate(targetPrefab, SyashutuPos, Quaternion.identity);
            objTumetaiMizu.name = "AtatakaiMizu";
            time = 0;
        }
    }
}
