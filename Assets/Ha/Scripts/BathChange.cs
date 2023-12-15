using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathChange : MonoBehaviour
{
    [SerializeField]
    private GameObject bathChangeObject;
    [SerializeField]
    private BathManager bathManager;
    [SerializeField]
    private List<float> bathChangeInfluence;
    [SerializeField]
    private List<GameObject> bathChangePrefabs;
    private GameObject bathChangePrefab;
    [SerializeField]
    private int timeToDestroyPrefab;

    private IEnumerator coroutine;

    public enum BathChangeType
    {
        Ice,
        Fire
    }

    [SerializeField]
    private BathChangeType bathChanger;

    public void Start()
    {
        TryGetComponent(out bathManager);
    }

    public void ChangeBathChangeType()
    {
        BathChangeType lastBathChanger = bathChanger;
        do
        {
            bathChanger = (BathChangeType)Random.Range(0, System.Enum.GetNames(typeof(BathChangeType)).Length);
        } while (bathChanger == lastBathChanger);

        bathChangePrefab = Instantiate(bathChangePrefabs[(int)bathChanger], bathChangeObject.transform);
        ChangeBathTemp();

        coroutine = WaitAndDestroyPrefab(timeToDestroyPrefab, bathChangePrefab);
        StartCoroutine(coroutine);

    }

    IEnumerator WaitAndDestroyPrefab(int seconds, GameObject prefab)
    {
        yield return new WaitForSeconds(seconds);

        Destroy(prefab);
    }

    public void ChangeBathTemp()
    {
        bathManager.CurrentTemperature += bathChangeInfluence[(int)bathChanger];

    }
}
