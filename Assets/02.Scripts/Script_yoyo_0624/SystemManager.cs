using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SystemManager : MonoBehaviour
{
    bool isGamestart; // 게임 시작 상태
    bool isZone2;     //Zone2 UI활성화 상태
    // 플레이 타임제한
    public TextMeshProUGUI playTime_Text;
    float currentTime;
    public Image ui_Timer;
    public static SystemManager instance;

    // 타임 오버&포탈
    public Canvas ui_Zone2_Tutorial;
    public GameObject portal01;
    public GameObject portal02;
    public TextMeshProUGUI text_GotoPortal1;
    public Canvas c_PloggingOver;
    public Image ui_PloggingOver;

    // 스코어
    //public Canvas ui_CurrentScore;
    //public int current_Score;
    //public TextMeshProUGUI current_score_Text;

    //Zone2 분리수거
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
        //타임
        currentTime = 50;        // 현재 시간 설정
        Time.timeScale = 1;     // 시간이 흐르게 한다.

        //타임오버
        c_PloggingOver.gameObject.SetActive(false);
        portal01.SetActive(false);
        ui_Zone2_Tutorial.gameObject.SetActive(false);

        //스코어
        //current_Score = 0;      //점수 초기화
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

    public void Start_Button_On() // 게임 시작 상태 on
    {
        isGamestart = true;
    }

    public void UI_Is_Zone2()
    {
        //Zone2의 UI 활성화
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
        if (isGamestart == true)            // 게임 시작 상태 일 때만, 작동한다.
        {
            playTime_Text.gameObject.SetActive(true);
            currentTime -= Time.deltaTime;  //남은 시간을 감소시킨다.
            int s = (int)(currentTime);

            if (playTime_Text != null)
            {
                playTime_Text.text = "" + s;
            }

            if (currentTime <= 0)
            {
                //타임오버
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

    // 타임오버 텍스트 끄기
    IEnumerator UI_OFF_TOText()
    {
        c_PloggingOver.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        ui_PloggingOver.gameObject.SetActive(false);
    }
}