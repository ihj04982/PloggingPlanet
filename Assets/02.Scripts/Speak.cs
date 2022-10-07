using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Speak : MonoBehaviour
{
    public GameObject talk;
    public TextMeshProUGUI text;
    public string[] words;
    int index;
    public SphereCollider eartButton;

    // Start is called before the first frame update
    //int buttonCount = 0;
    private void Start()
    {
        
        text.text = words[0];
        index = 0;
        eartButton.enabled = false;
    }
    // Update is called once per frame
    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        { ChangeText(); }
    }
    public void ChangeText()
    {
        if (index < words.Length - 1)
        {
            index++;
        }
        text.text = words[index];
        if(index == words.Length-1)
        {
            eartButton.enabled = true;
            talk.SetActive(false);
        }
        //if (buttonCount == 0)
        //{

        //    text.text = "�� ������� ���� ���� ����??";
        //    buttonCount++;
        //}
        //if (buttonCount == 1)
        //{
        //    text.text = "���� ������";
        //    buttonCount++;
        //}
        //if (buttonCount == 2)
        //{
        //    text.text = "�׷��� ���� ������ �Ƿ��� �ߵ�����!";
        //    buttonCount++;
        //}
        //if (buttonCount == 3)
        //{
        //    text.text = "�� �� �ְ��� ȯ������� �ɰž�";
        //    buttonCount++;
        //}
        //if (buttonCount == 3)
        //{
        //    this.gameObject.SetActive(false);
        //}
    }

    }
    


