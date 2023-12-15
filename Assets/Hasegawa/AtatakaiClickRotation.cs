using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtatakaiClickRotation : MonoBehaviour
{
    // �}�E�X��������Ă��邩������Ă��Ȃ���
    public bool _isAtatakaiPushed = false;
    public bool _IsAtatakaiPushed
    {
        get { return _isAtatakaiPushed; }
        set { _isAtatakaiPushed = value; }
    }
    // ���݂̃}�E�X�̃��[���h���W
    private Vector3 _nowMousePosi;
    private Vector3 _oldMousePosi;

    [SerializeField] float AtatakaiAngleMulVal;

    public float AtatakaiAngle;

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
        if (_isAtatakaiPushed)
        {
            Vector3 NowVec = (_nowMousePosi - transform.position).normalized;
            Vector3 OldVec = (_oldMousePosi - transform.position).normalized;
            AtatakaiAngle = Vector3.SignedAngle(OldVec, NowVec, Vector3.forward);

            if (AtatakaiAngle < 0.0f)
            {
                AtatakaiAngle /= Time.deltaTime;
                AtatakaiAngle *= AtatakaiAngleMulVal;

                transform.Rotate(0, 0, AtatakaiAngle);
            }
        }
        _oldMousePosi = _nowMousePosi;
    }
}
