using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.GetInstance().PlayTitleSceneBGM();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
