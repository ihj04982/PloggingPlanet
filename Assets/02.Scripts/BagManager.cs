using UnityEngine;

public class BagManager : MonoBehaviour
{
    // ����� ��Ʈ�ѷ��� �浹�ϰ� Ʈ���Ÿ� �����ٸ� ����Ʈ���� �����ȴ�

    private void OnTriggerStay(Collider other)
    {
        if (other.name.Contains("RightControllerAnchor"))
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) == true )
            {
                // (��Ȱ��ȭ �� �����⸦ Ȱ��ȭ ��Ű�� ��Ʈ�ѷ��� grabPos�� ��ġ��Ų��)
                Controller.instance.RemoveTrash();
            }
        }
    }
}
