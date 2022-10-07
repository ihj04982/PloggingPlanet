using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperBin : MonoBehaviour
{
    GameObject wE;
    GameObject gE;
    public AudioSource Right;
    public AudioSource Error;
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
        if (other.gameObject.name.Contains("Paper"))
        {
            other.gameObject.SetActive(false);
            ScoreManager.instance.CURRENT_SCORE += ScoreManager.instance.paperValue;
            StartCoroutine("Paper_CMessage");
            Right.Play();
        }
        else if (other.gameObject.name.Contains("Plastic"))
        {
            StartCoroutine("Paper_WMessage");
            Error.Play();
        }
        else if (other.gameObject.name.Contains("General"))
        {
            StartCoroutine("Paper_WMessage");
            Error.Play();
        }
    }

    IEnumerator Paper_CMessage()
    {
        SystemManager.instance.ui_PGoodjob.gameObject.SetActive(true);
        gE = Instantiate(SystemManager.instance.gEffect);
        gE.transform.position = transform.position;
        yield return new WaitForSeconds(2f);
        Destroy(gE.gameObject);
        SystemManager.instance.ui_PGoodjob.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
    }

    IEnumerator Paper_WMessage()
    {
        SystemManager.instance.ui_PTryagain.gameObject.SetActive(true);
        wE = Instantiate(SystemManager.instance.wEffect);
        wE.transform.position = transform.position;
        yield return new WaitForSeconds(2f);
        Destroy(wE.gameObject);
        SystemManager.instance.ui_PTryagain.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
    }
}
