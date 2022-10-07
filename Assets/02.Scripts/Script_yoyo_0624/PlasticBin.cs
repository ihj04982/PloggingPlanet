using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticBin : MonoBehaviour
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
        if (other.gameObject.name.Contains("Plastic"))
        {
            other.gameObject.SetActive(false);
            ScoreManager.instance.CURRENT_SCORE += ScoreManager.instance.plasticValue;
            StartCoroutine("Plastic_CMessage");
            Right.Play();
        }
        else if (other.gameObject.name.Contains("General"))
        {
            StartCoroutine("Plastic_WMessage");
            Error.Play();

        }
        else if (other.gameObject.name.Contains("Paper"))
        {
            StartCoroutine("Plastic_WMessage");
            Error.Play();
        }
    }

    IEnumerator Plastic_CMessage()
    {
        SystemManager.instance.ui_LGoodjob.gameObject.SetActive(true);
        gE = Instantiate(SystemManager.instance.gEffect);
        gE.transform.position = transform.position;
        yield return new WaitForSeconds(2f);
        Destroy(gE.gameObject);
        SystemManager.instance.ui_LGoodjob.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
    }

    IEnumerator Plastic_WMessage()
    {
        SystemManager.instance.ui_LTryagain.gameObject.SetActive(true);
        wE = Instantiate(SystemManager.instance.wEffect);
        wE.transform.position = transform.position;
        yield return new WaitForSeconds(2f);
        Destroy(wE.gameObject);
        SystemManager.instance.ui_LTryagain.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
    }


}
