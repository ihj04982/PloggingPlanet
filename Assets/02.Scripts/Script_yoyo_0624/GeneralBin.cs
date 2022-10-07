using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralBin : MonoBehaviour
{
    GameObject wE;
    GameObject gE;

    public AudioSource Right;
    public AudioSource Error;
    public static GeneralBin instance;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name.Contains("General"))
        {
            other.gameObject.SetActive(false);
            ScoreManager.instance.CURRENT_SCORE+= ScoreManager.instance.generalValue;
            StartCoroutine("General_CMessage");
            Right.Play();

        }
        else if (other.gameObject.name.Contains("Plastic"))
        {
            StartCoroutine("General_WMessage");
            Error.Play();

        }
        else if (other.gameObject.name.Contains("Paper"))
        {
            StartCoroutine("General_WMessage");
            Error.Play();
        }
    }

    IEnumerator General_CMessage()
    {

        SystemManager.instance.ui_GGoodjob.gameObject.SetActive(true);
        gE = Instantiate(SystemManager.instance.gEffect);
        gE.transform.position = transform.position;
        yield return new WaitForSeconds(2f);
        Destroy(gE.gameObject);
        SystemManager.instance.ui_GGoodjob.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
    }

    IEnumerator General_WMessage()
    {
        SystemManager.instance.ui_GTryagain.gameObject.SetActive(true);
        wE = Instantiate(SystemManager.instance.wEffect);
        wE.transform.position = transform.position;
        yield return new WaitForSeconds(2f);
        Destroy(wE.gameObject);
        SystemManager.instance.ui_GTryagain.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
    }
}
