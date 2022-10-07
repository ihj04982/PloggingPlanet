using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAction : MonoBehaviour
{
    private int SceneToLoad;
    public LineRenderer ln;
    RaycastHit hit;
    public GameObject sceneChanger;

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, 2) == true)
        {
            ln.SetPosition(1, new Vector3(0, 0, hit.distance));
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) == true)
                if (hit.transform.tag == "SB") //ºÎµúÈù°Ô UI¶ó¸é
                {
                    sceneChanger.GetComponent<SceneChanger>().FadeToScene(0);
                }
        }
    }

    public void FadeToScene(int SceneIndex)
    {
        SceneToLoad = SceneIndex;
    }
}

