using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SystemManager : MonoBehaviour
{
    bool isGamestart; // ���� ���� ����
    bool isZone2;     //Zone2 UIȰ��ȭ ����
    // �÷��� Ÿ������
    public TextMeshProUGUI playTime_Text;
    float currentTime;
    public Image ui_Timer;
    public static SystemManager instance;

    // Ÿ�� ����&��Ż
    public Canvas ui_Zone2_Tutorial;
    public GameObject portal01;
    public GameObject portal02;
    public TextMeshProUGUI text_GotoPortal1;
    public Canvas c_PloggingOver;
    public Image ui_PloggingOver;

    // ���ھ�
    //public Canvas ui_CurrentScore;
    //public int current_Score;
    //public TextMeshProUGUI current_score_Text;

    //Zone2 �и�����
    public GameObject ui_M_General;
    public GameObject ui_M_Plastic;
    public GameObject ui_M_Paper;
    public Image ui_GTryagain;
    public Image ui_GGoodjob;
    public Image ui_LTryagain;
    public Image ui_LGoodjob;
    public Image ui_PTryagain;
    public Image ui_PGoodjob;

    //VFX
    public GameObject wEffect;
    public GameObject gEffect;

    public GameObject lineRenderer;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Ÿ��
        currentTime = 50;        // ���� �ð� ����
        Time.timeScale = 1;     // �ð��� �帣�� �Ѵ�.

        //Ÿ�ӿ���
        c_PloggingOver.gameObject.SetActive(false);
        portal01.SetActive(false);
        ui_Zone2_Tutorial.gameObject.SetActive(false);

        //���ھ�
        //current_Score = 0;      //���� �ʱ�ȭ
        //current_score_Text.text = "0";
        //ui_CurrentScore.gameObject.SetActive(false);

        //UI_Zone2_message
        ui_GTryagain.gameObject.SetActive(false);
        ui_GGoodjob.gameObject.SetActive(false);
        ui_LTryagain.gameObject.SetActive(false);
        ui_LGoodjob.gameObject.SetActive(false);
        ui_PTryagain.gameObject.SetActive(false);
        ui_PGoodjob.gameObject.SetActive(false);


        //------------
        isGamestart = true;
        //------------ 
    }

    // Update is called once per frame
    void Update()
    {
        TimeCount();
        UI_Is_Zone2();
    }

    public float TIME
    {
        get { return currentTime; }
    }

    public void Start_Button_On() // ���� ���� ���� on
    {
        isGamestart = true;
    }

    public void UI_Is_Zone2()
    {
        //Zone2�� UI Ȱ��ȭ
        if ( Controller.instance.state == Controller.State.ZONE2 )
        {
           // ui_CurrentScore.gameObject.SetActive(true);
            ui_Zone2_Tutorial.gameObject.SetActive(true);
            ui_Timer.gameObject.SetActive(false);
            c_PloggingOver.gameObject.SetActive(false);
        }
    }

    #region TimeCount
    public void TimeCount()
    {
        if (isGamestart == true)            // ���� ���� ���� �� ����, �۵��Ѵ�.
        {
            playTime_Text.gameObject.SetActive(true);
            currentTime -= Time.deltaTime;  //���� �ð��� ���ҽ�Ų��.
            int s = (int)(currentTime);

            if (playTime_Text != null)
            {
                playTime_Text.text = "" + s;
            }

            if (currentTime <= 0)
            {
                //Ÿ�ӿ���
                currentTime = 0;
                isGamestart = false;
                playTime_Text.gameObject.SetActive(false);
                portal01.SetActive(true);
                ui_Timer.gameObject.SetActive(false);
                StartCoroutine("UI_OFF_TOText");
                lineRenderer.SetActive(false);

            }
        }
    }
    #endregion

    // Ÿ�ӿ��� �ؽ�Ʈ ����
    IEnumerator UI_OFF_TOText()
    {
        c_PloggingOver.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        ui_PloggingOver.gameObject.SetActive(false);
    }
}