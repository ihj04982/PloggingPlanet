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

    bool isContact = false; //접촉했는가
    public bool isPick = false; //먹었는가
    public bool isGrab = false; //잡았는가
    GameObject grabObj;     //잡은 객체

    // 쓰레기를 저장할 리스트
    public List<GameObject> trashList = new List<GameObject>();
    // 쓰레기가 위치할 곳
    public Transform trashPool;
    public Transform grabPos;
    // 상태
    public enum State { ZONE1, ZONE2 }
    public State state;
    //라인 렌더러 가져오기(시각화)
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

    #region 공통 -----------------------------------------------------
    // 만약 충돌한다면 진동이 발생한다
    // 객체를 잡고 있는 상태라면 진동이 발생하지 않는다
    // 충돌이 나면 충돌 난 객체를 잡는다 (내 객체로 편입된다)
    private void OnTriggerEnter(Collider other)
    {
        // 태그가 Grabbable이고
        if (other.CompareTag("Grabbable") == true)
        {
            //충돌했음
            isContact = true;
            //충돌한 물체는 grabObj
            grabObj = other.gameObject;
            //충돌하였으나 잡거나 먹지 않았다면 0.5초간 진동이 발생한다
            if (isGrab == false)
            {
                OVRInput.SetControllerVibration(0.5f, 0.5f, OVRInput.Controller.RTouch);
            }
        }
    }

    // 충돌에서 벗어나고 잡거나 먹지 않았다
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
    // Raycast로 충돌을 감지하고 
    // 태그가 Grabble이라면 트리거를 눌러 객체를 먹는다
    // 레이어가 UI라면 버튼의 OnClick이벤트 실행 
    // 먹은 객체는 쓰레기 리스트(ScoreManager.cs)에 저장한다
    public void RayControl()
    {
        Ray ray = new Ray(grabPos.position, grabPos.forward);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 15) == true) // Raycast 충돌시        
        {

            grabObj = hitInfo.transform.gameObject;
            lineRenderer.SetPosition(1, new Vector3(0, 0, hitInfo.distance)); // lineRenderer 길이 graObj까지로 설정

            // 부딪힌 객체의 태그가 Grabbable 이라면 PickObject를 하고 UI라면 UICheck
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

    // 만일 UI

    public void PickObject()
    {
        {
            // grabPos까지 객체를 데려온다
            grabObj.GetComponent<Rigidbody>().isKinematic = true;
            Get.Play();

            grabObj.transform.position = Vector3.Lerp(grabObj.transform.position, grabPos.position, Time.deltaTime * 2f);
            float distance = Vector3.Distance(grabObj.transform.position, grabPos.position);

            // destroyDistance까지 왔다면 리스트로 옮긴다
            if (distance <= destroyDistance)
            {
                isPick = true;
                // 객체의 이름에 따라 스코어 매니저에서 개수 구분 
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
    // 가방에서(BagManager.cs) 트리거를 눌러 저장된 객체를 꺼낸다
    // 트리거를 놓으면 객체가 떨어진다
    // 떨어진 객체를 다시 잡고 놓는다
    public void RemoveTrash()
    {
        // 만약 쓰레기 리스트 안에 쓰레기가 있다면
        if (trashList.Count > 0)
        {
            // 쓰레기 리스트 중 (임의)마지막 쓰레기를 활성화 시키고
            grabObj = trashList[trashList.Count - 1];
            grabObj.SetActive(true);
            // 물리를 비활성화 시켜
            grabObj.GetComponent<Rigidbody>().isKinematic = true;
            // grabPos에 위치시킨다
            grabObj.transform.SetParent(grabPos);
            // grabObj.transform.position = new Vector3(0, 0, 0);
            grabObj.transform.position = grabPos.position;
            //trash.transform.position = grabPos.position;
            // 꺼내진 쓰레기는 리스트에서 삭제한다
            trashList.RemoveAt(trashList.Count - 1);
            isGrab = true;
        }
    }
    // 먹는 것이 아니라 접촉하고 아직 잡지 않았다면
    // 객체를 잡아 위치시킨다
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
    // 먹거나 잡은 상태에서 트리거 버튼을 놓으면 물체가 놓아진다

    public void ReleaseObject()
    {

        //속도 벡터를 만든다
        //RController를 휘두를 때 속도
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





