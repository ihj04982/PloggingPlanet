using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    private void Awake()
    {
        instance = this;
    }

    // �κ��丮 ����
    int trashQuantity;
    public TextMeshProUGUI trashQuantity_Text;
    public TextMeshProUGUI Name_Text;
    public Image inventoryLightOn;
    //float alphaLightOn;
    //float alphaLgithOff;

    public void Trash()
    {
        // �κ��丮+1 �� ������, UI��¦�̰��ϱ�
     
            //  trashQuantity_Text.text = "" + trashQuantity;
            StartCoroutine("UI_LightOnStart");
            StartCoroutine("UI_LightOffStart");
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //trashQuantity = 0;
        trashQuantity_Text.text = "�������";
    }

    // Update is called once per frame
    void Update()
    {
        trashQuantity = Controller.instance.trashList.Count;
        trashQuantity_Text.text = "" + Controller.instance.trashList.Count;
        if(Controller.instance.state == Controller.State.ZONE2)
        {
            Name_Text.text = "���� ������ ��";
        }
    }

    #region UI_LightOnOff_Coroutine
    public IEnumerator UI_LightOffStart()
    {
        for (float f = 1f; f > 0; f -= 0.02f)
        {
            Color c = inventoryLightOn.color;
            c.a = f;
            inventoryLightOn.color = c;
            yield return null;
        }
        yield return new WaitForSeconds(1);
    }

    //���̵� ��
    public IEnumerator UI_LightOnStart()
    {
        for (float f = 0f; f < 1; f += 0.02f)
        {
            Color c = inventoryLightOn.color;
            c.a = f;
            inventoryLightOn.color = c;
            yield return null;
        }
    }
    #endregion

}
