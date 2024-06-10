using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCam : MonoBehaviour
{
    public GameObject defaultCam;
    public GameObject camPWUPLvls;

    public void ChangeCam(bool main)
    {
        if(main)
        {
            defaultCam.SetActive(true);
            camPWUPLvls.SetActive(false);
        }
        else
        {
            defaultCam.SetActive(false);
            camPWUPLvls.SetActive(true);
        }
    }
}
