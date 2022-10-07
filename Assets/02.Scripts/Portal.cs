using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 문을 통과하면 다른 문으로 이동하고 싶다
// 열쇠를 획득하고 Door2와 충돌할 경우 level2Pos로 이동하고싶다

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
