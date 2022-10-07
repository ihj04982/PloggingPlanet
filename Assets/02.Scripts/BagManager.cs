using UnityEngine;

public class BagManager : MonoBehaviour
{
    // 가방과 컨트롤러가 충돌하고 트리거를 누른다면 리스트에서 삭제된다

    private void OnTriggerStay(Collider other)
    {
        if (other.name.Contains("RightControllerAnchor"))
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) == true )
            {
                // (비활성화 된 쓰레기를 활성화 시키고 컨트롤러의 grabPos에 위치시킨다)
                Controller.instance.RemoveTrash();
            }
        }
    }
}
