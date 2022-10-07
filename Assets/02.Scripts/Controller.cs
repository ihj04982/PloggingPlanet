using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class Controller : MonoBehaviour
{
    public AudioSource Get;
    static public Controller instance;
    private void Awake()
    {
        instance = this;
    }

    bool isContact = false; //�����ߴ°�
    public bool isPick = false; //�Ծ��°�
    public bool isGrab = false; //��Ҵ°�
    GameObject grabObj;     //���� ��ü

    // �����⸦ ������ ����Ʈ
    public List<GameObject> trashList = new List<GameObject>();
    // �����Ⱑ ��ġ�� ��
    public Transform trashPool;
    public Transform grabPos;
    // ����
    public enum State { ZONE1, ZONE2 }
    public State state;
    //���� ������ ��������(�ð�ȭ)
    public LineRenderer lineRenderer;
    public float destroyDistance = 1f;

    public GameObject player;


    void Start()
    {
        isContact = false;
        isGrab = false;
        isPick = false;
        state = State.ZONE1;

        
    }
    void Update()
    {
        switch (state)
        {
            case State.ZONE1:
                isGrab = false;
                RayControl();
                break;

            case State.ZONE2:
                lineRenderer.enabled = false;
                isPick = false;
                GrabObject();
                ReleaseObject();
                PlayerMove.instance.tempSpeedup = 2;
                break;
        }
    }

    #region ���� -----------------------------------------------------
    // ���� �浹�Ѵٸ� ������ �߻��Ѵ�
    // ��ü�� ��� �ִ� ���¶�� ������ �߻����� �ʴ´�
    // �浹�� ���� �浹 �� ��ü�� ��´� (�� ��ü�� ���Եȴ�)
    private void OnTriggerEnter(Collider other)
    {
        // �±װ� Grabbable�̰�
        if (other.CompareTag("Grabbable") == true)
        {
            //�浹����
            isContact = true;
            //�浹�� ��ü�� grabObj
            grabObj = other.gameObject;
            //�浹�Ͽ����� ��ų� ���� �ʾҴٸ� 0.5�ʰ� ������ �߻��Ѵ�
            if (isGrab == false)
            {
                OVRInput.SetControllerVibration(0.5f, 0.5f, OVRInput.Controller.RTouch);
            }
        }
    }

    // �浹���� ����� ��ų� ���� �ʾҴ�
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Grabbable") == true && isGrab == false && isPick == false)
        {
            isContact = false;
            grabObj = null;
        }
    }
    #endregion

    #region ZONE 1 -----------------------------------------------------
    // state 1 
    // Raycast�� �浹�� �����ϰ� 
    // �±װ� Grabble�̶�� Ʈ���Ÿ� ���� ��ü�� �Դ´�
    // ���̾ UI��� ��ư�� OnClick�̺�Ʈ ���� 
    // ���� ��ü�� ������ ����Ʈ(ScoreManager.cs)�� �����Ѵ�
    public void RayControl()
    {
        Ray ray = new Ray(grabPos.position, grabPos.forward);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 15) == true) // Raycast �浹��        
        {

            grabObj = hitInfo.transform.gameObject;
            lineRenderer.SetPosition(1, new Vector3(0, 0, hitInfo.distance)); // lineRenderer ���� graObj������ ����

            // �ε��� ��ü�� �±װ� Grabbable �̶�� PickObject�� �ϰ� UI��� UICheck
            if (grabObj.tag == "Grabbable" && OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) == true)
            {
                //print("Grabbable");
                PickObject();
            }
            if (grabObj.layer == LayerMask.NameToLayer("UI")
                && OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) == true)
            {
                print("UI");
                grabObj.GetComponent<Button>().onClick.Invoke();
            }
        }
    }

    // ���� UI

    public void PickObject()
    {
        {
            // grabPos���� ��ü�� �����´�
            grabObj.GetComponent<Rigidbody>().isKinematic = true;
            Get.Play();

            grabObj.transform.position = Vector3.Lerp(grabObj.transform.position, grabPos.position, Time.deltaTime * 2f);
            float distance = Vector3.Distance(grabObj.transform.position, grabPos.position);

            // destroyDistance���� �Դٸ� ����Ʈ�� �ű��
            if (distance <= destroyDistance)
            {
                isPick = true;
                // ��ü�� �̸��� ���� ���ھ� �Ŵ������� ���� ���� 
                if(grabObj.name.Contains("Paper"))
                {
                    ScoreManager.instance.paperAmount++;
                }
                if (grabObj.name.Contains("General"))
                {
                    ScoreManager.instance.generalAmount++;
                }
                if (grabObj.name.Contains("Plastic"))
                {
                    ScoreManager.instance.plasticAmount++;
                }
                grabObj.SetActive(false);
                grabObj.transform.SetParent(trashPool);
                trashList.Add(grabObj);
                InventoryManager.instance.Trash();
            }
        }
    }

    #endregion

    #region ZONE 2 -----------------------------------------------------
    // state 2 
    // ���濡��(BagManager.cs) Ʈ���Ÿ� ���� ����� ��ü�� ������
    // Ʈ���Ÿ� ������ ��ü�� ��������
    // ������ ��ü�� �ٽ� ��� ���´�
    public void RemoveTrash()
    {
        // ���� ������ ����Ʈ �ȿ� �����Ⱑ �ִٸ�
        if (trashList.Count > 0)
        {
            // ������ ����Ʈ �� (����)������ �����⸦ Ȱ��ȭ ��Ű��
            grabObj = trashList[trashList.Count - 1];
            grabObj.SetActive(true);
            // ������ ��Ȱ��ȭ ����
            grabObj.GetComponent<Rigidbody>().isKinematic = true;
            // grabPos�� ��ġ��Ų��
            grabObj.transform.SetParent(grabPos);
            // grabObj.transform.position = new Vector3(0, 0, 0);
            grabObj.transform.position = grabPos.position;
            //trash.transform.position = grabPos.position;
            // ������ ������� ����Ʈ���� �����Ѵ�
            trashList.RemoveAt(trashList.Count - 1);
            isGrab = true;
        }
    }
    // �Դ� ���� �ƴ϶� �����ϰ� ���� ���� �ʾҴٸ�
    // ��ü�� ��� ��ġ��Ų��
    public void GrabObject()
    {
        if (isContact == true &&
             OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) == true)
        {
            grabObj.GetComponent<Rigidbody>().isKinematic = true;
            grabObj.transform.SetParent(grabPos);
            isGrab = true;
        }
    }
    // �԰ų� ���� ���¿��� Ʈ���� ��ư�� ������ ��ü�� ��������

    public void ReleaseObject()
    {

        //�ӵ� ���͸� �����
        //RController�� �ֵθ� �� �ӵ�
        if (isGrab == true)
        {
            if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) == true)
            {
                //grabObj = grabPos.GetChild(0).gameObject;
   
                    Vector3 objVelocity = OVRInput.GetLocalControllerAngularVelocity(OVRInput.Controller.RTouch);
                    grabObj.GetComponent<Rigidbody>().velocity = objVelocity * 1f;
                    grabObj.GetComponent<Rigidbody>().isKinematic = false;
                    grabObj.transform.SetParent(null);

                    isContact = false;
                    isGrab = false;
                    grabObj = null;
            }
        }
    }
}
#endregion





