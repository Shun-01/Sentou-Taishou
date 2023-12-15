using System.Collections.Generic;
using UnityEngine;

public class Weather : MonoBehaviour
{
    //���C�̊Ǘ��I�u�W�F�N�g
    private BathManager bathManager = null;

    //�V��^�C�v�񋓎q
    public enum WeatherType
    {
        Heatwave, //�ҏ�
        Sunny,    //����
        Cloudy,   //�܂�
        Rainy,    //�J
        Snowy,    //��
    }

    //�V��ɂ��e���l���X�g
    [SerializeField, Header("�V��ɂ�鉷�x�ւ̉e���l���w�肵�܂�")]
    private List<float> InfluencePowerList;

    //�V��Œ�t���O
    [SerializeField, Header("�V���܂�ɌŒ�ɂ��܂�")]
    private bool FixedWeatherActivation;

    //�ŏ����܂肩��n�܂邩���w�肵�܂�
    [SerializeField, Header("�J�n����܂�ɂ��܂�")]
    private bool BeginWeatherCloudy;

    //�V�󃉃��_���t���O
    [SerializeField, Header("�V�󂪃����_���ɕς��悤�ɂ��܂�")]
    private bool RandomWeatherActivation;

    //���݂̓V��
    [SerializeField]
    private WeatherType CurrentWeather = WeatherType.Cloudy;

    // Start is called before the first frame update
    private void Start()
    {
        //�V��̐��Ɖe���l���X�g�͈�v���Ă���K�v������
        Debug.Assert(Utility.GetEnumeratorLength<WeatherType>() == InfluencePowerList.Count);

        //BathManager�擾
        TryGetComponent(out bathManager);
        Debug.Assert(bathManager);
    }

    // Update is called once per frame
    private void Update()
    {
    }

    //----------------------------------------------------------------
    //  �V��ɉ��������x�ւ̉e����^����
    //----------------------------------------------------------------

    public int GetWeatherType()
    {
        return (int)CurrentWeather;
    }

    public void ChangeWeatherType()
    {
        WeatherType lastWeather = CurrentWeather;
        do
        {
            CurrentWeather = (WeatherType)Random.Range(0, System.Enum.GetNames(typeof(WeatherType)).Length);
        } while (CurrentWeather == lastWeather);        
        
    }

    public void AffectWeather()
    {
        //�Ƃ肠�������̓C���N�������g
        bathManager.CurrentTemperature += InfluencePowerList[(int)CurrentWeather];
    }

}