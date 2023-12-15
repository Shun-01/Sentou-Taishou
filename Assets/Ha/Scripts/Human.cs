using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private BathManager bathManager;
    public float temperature;
    private float hightemperature = 42f;
    private float lowtemperature = 38f;
    private Emotion emotion;
    private enum Emotion
    {
        Normal,
        Hot,
        Cold
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // temperature = ... // Insert water temperature here
        temperature = bathManager.CurrentTemperature;
        if(temperature >= hightemperature)
        {
            emotion = Emotion.Hot;
            spriteRenderer.sprite = Resources.Load<Sprite>("hot_man");
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);

        }
        else if(temperature <= lowtemperature)
        {
            emotion = Emotion.Cold;
            spriteRenderer.sprite = Resources.Load<Sprite>("cold_man");
            gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
        else
        {
            emotion = Emotion.Normal;
            spriteRenderer.sprite = Resources.Load<Sprite>("normal_man");
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
