using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ����ϸ� �ٸ� ������ �̵��ϰ� �ʹ�
// ���踦 ȹ���ϰ� Door2�� �浹�� ��� level2Pos�� �̵��ϰ�ʹ�

public class Portal : MonoBehaviour
{
    public Transform portal02;

    Vector3 portal02_;

    public AudioSource BGM1;
    public AudioSource BGM2;

    private void Start()
    {
        portal02_ = portal02.position;
        BGM1.enabled = true;
        BGM2.enabled = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name.Contains("Portal01"))
        {
            transform.position = new Vector3(portal02_.x, 1, portal02_.z);
            Controller.instance.state = Controller.State.ZONE2;
            BGM1.enabled = false;
            BGM2.enabled = true;
        }
    }
}
