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

    // 인벤토리 관련
    int trashQuantity;
    public TextMeshProUGUI trashQuantity_Text;
    public TextMeshProUGUI Name_Text;
    public Image inventoryLightOn;
    //float alphaLightOn;
    //float alphaLgithOff;

    public void Trash()
    {
        // 인벤토리+1 할 때마다, UI반짝이게하기
     
            //  trashQuantity_Text.text = "" + trashQuantity;
            StartCoroutine("UI_LightOnStart");
            StartCoroutine("UI_LightOffStart");
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //trashQuantity = 0;
        trashQuantity_Text.text = "비어있음";
    }

    // Update is called once per frame
    void Update()
    {
        trashQuantity = Controller.instance.trashList.Count;
        trashQuantity_Text.text = "" + Controller.instance.trashList.Count;
        if(Controller.instance.state == Controller.State.ZONE2)
        {
            Name_Text.text = "남은 쓰레기 수";
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

    //페이드 인
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
