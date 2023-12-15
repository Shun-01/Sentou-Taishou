using System;
using UnityEngine;

//----------------------------------------------------------------
//  ���C�Ǘ�
//----------------------------------------------------------------
public class BathManager : MonoBehaviour
{
    //�Q�[���J�n���Ƀ����_���Ɍ��܂�ő�̉��x
    [SerializeField]
    private float BeginTemperatureMax = 30.0f;

    //�Q�[���J�n���Ƀ����_���Ɍ��܂�ŏ��̉��x
    [SerializeField]
    private float BeginTemperatureMin = 50.0f;

    //�ڕW���x(40.0f�萔)
    public readonly float TargetTemeprature = 40.0f;

    //���݂̉��x
    [SerializeField]
    public float CurrentTemperature;

    // Start is called before the first frame update
    private void Start()
    {
        //�J�n���̉��x������
        CurrentTemperature = UnityEngine.Random.Range(BeginTemperatureMin, BeginTemperatureMax);
    }

    // Update is called once per frame
    private void Update()
    {
        //CurrentTemperature = GetRoundedCurrentTemperature();
    }

    //----------------------------------------------------------------
    //  ������P�ʂŎl�̌ܓ����������x���擾
    //----------------------------------------------------------------
    public float GetRoundedCurrentTemperature() => MathF.Round(CurrentTemperature, 1, MidpointRounding.AwayFromZero);
}