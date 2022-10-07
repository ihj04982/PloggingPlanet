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

        //    text.text = "저 더러운게 내가 맡은 지구??";
        //    buttonCount++;
        //}
        //if (buttonCount == 1)
        //{
        //    text.text = "으악 끔찍해";
        //    buttonCount++;
        //}
        //if (buttonCount == 2)
        //{
        //    text.text = "그래도 전문 요정이 되려면 견뎌야지!";
        //    buttonCount++;
        //}
        //if (buttonCount == 3)
        //{
        //    text.text = "난 꼭 최고의 환경요정이 될거야";
        //    buttonCount++;
        //}
        //if (buttonCount == 3)
        //{
        //    this.gameObject.SetActive(false);
        //}
    }

    }
    


