using UnityEngine;

public class MizuOtiru : MonoBehaviour
{
    [SerializeField] private float downSpeed = 0.1f;
    private GameObject Oyu;

    [SerializeField]

    // Start is called before the first frame update
    private void Start()
    {
        Oyu = GameObject.Find("Oyu");
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 objPos = gameObject.transform.position;
        gameObject.transform.position = new Vector3(objPos.x, objPos.y - downSpeed, objPos.z);

        if (Oyu.transform.position.y + 1.5f >= gameObject.transform.position.y)
        {
            if (name == "AtatakaiMizu")
            {
                GameObject.Find("GameSceneManager").GetComponent<SceneGameManager>().GetBathManager().CurrentTemperature += 0.15f;
            }
            else
            {
                GameObject.Find("GameSceneManager").GetComponent<SceneGameManager>().GetBathManager().CurrentTemperature -= 0.15f;
            }


            Destroy(gameObject);
        }
    }
}