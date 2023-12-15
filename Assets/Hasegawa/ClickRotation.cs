using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickRotation : MonoBehaviour
{
    // �}�E�X��������Ă��邩������Ă��Ȃ���
    public bool _isPushed = false;
    public bool _IsPushed
    {
        get { return _isPushed; }
        set { _isPushed = value; }
    }
    // ���݂̃}�E�X�̃��[���h���W
    private Vector3 _nowMousePosi;
    private Vector3 _oldMousePosi;

    [SerializeField] float TumetaiAngleMulVal;

    public float TumetaAngle;

    // Start is called before the first frame update
    void Start()
    {
        _oldMousePosi = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    // Update is called once per frame
    void Update()
    {
        _nowMousePosi = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // �}�E�X��������
        if (_isPushed)
        {
           
            Vector3 NowVec = (_nowMousePosi - transform.position).normalized;
            Vector3 OldVec = (_oldMousePosi - transform.position).normalized;
            TumetaAngle = Vector3.SignedAngle(OldVec, NowVec, Vector3.forward);
            Debug.Log(TumetaAngle + "�p�x");
            if (TumetaAngle < 0.0f)
            {
                TumetaAngle /= Time.deltaTime;
                TumetaAngle *= TumetaiAngleMulVal;
                transform.Rotate(0, 0, TumetaAngle);
            }
        }
        _oldMousePosi = _nowMousePosi;
    }
}
