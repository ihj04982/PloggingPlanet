using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public Animator animator;
    private int SceneToLoad;
    //private UnityAction action;

    public void FadeToScene(int SceneIndex)
    {
        SceneToLoad = SceneIndex;
        animator.SetTrigger("Fade_Out");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(SceneToLoad);
    }
    public void OnButton0Click()
    {
        FadeToScene(0);
    }

    public void OnButton1Click()
    {
        FadeToScene(1);
        //SceneManager.LoadScene("Play", LoadSceneMode.Additive);
    }

    public void EndingLoad()
    {
        FadeToScene(2);
    }

    public void OnQuitClick()
    {
        print("Quit");
        Application.Quit();
    }
}
