using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    static public ScoreManager instance;
    private void Awake()
    {
        instance = this;
    }

    public int generalAmount, paperAmount, plasticAmount = 0;
    public int generalValue, paperValue, plasticValue = 0;
    public int goal;

    //UI
    public TextMeshProUGUI textHighScore;
    public TextMeshProUGUI current_score_Text;
    //점수
    int high_Score;
    int current_Score;

    public GameObject sceneChanger;

    #region 현재 점수 / 최고 점수-----------------
    public int CURRENT_SCORE
    {
        get { return current_Score; }
        set
        {
            current_Score = value;
            current_score_Text.text = "" + current_Score;
            if (current_Score > high_Score)
            {
                // 최고점수를 현재점수로 하고
                HIGH_SCORE = current_Score;
                // 저장하고 싶다
                PlayerPrefs.SetInt("HIGHSCORE", HIGH_SCORE);
            }
        }
    }
    public int HIGH_SCORE
    {
        get { return high_Score; }
        set
        {
            high_Score = value;
            textHighScore.text = "" + high_Score;
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        current_Score = 0;
        HIGH_SCORE = PlayerPrefs.GetInt("HIGHSCORE", 0);
    }

    // Update is called once per frame
    void Update()
    {
        goal = (paperAmount * paperValue) + (generalAmount * generalValue) + (plasticAmount * plasticValue);

        // 현재 점수가 목표 점수와 같고 Zone2 상태라면 엔딩 씬 로드한다
        if (current_Score == goal && Controller.instance.state == Controller.State.ZONE2)
        {
            sceneChanger.GetComponent<SceneChanger>().EndingLoad();
        }
    }
}
