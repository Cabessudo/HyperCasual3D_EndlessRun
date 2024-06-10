using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Cutscene : MonoBehaviour
{
    [Header("Cameras")]
    public GameObject mainCam;
    public GameObject startCam;
    public GameObject busCam;
    [SerializeField] private CinemachineVirtualCamera endCam;


    void Awake()
    {
        DisableCams();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CutsceneStart());
    }

    void Update()
    {
        CutsceneEnd();
    }

    void DisableCams()
    {
        mainCam.SetActive(false);
        startCam.SetActive(false);
        busCam.SetActive(false);
    }

    IEnumerator CutsceneStart()
    {
        busCam.SetActive(true);
        yield return new WaitForSeconds(3);
        busCam.SetActive(false);
        startCam.SetActive(true);
        yield return new WaitForSeconds(2);
        startCam.SetActive(false);
        mainCam.SetActive(true);
        yield return new WaitForSeconds(1);
        PlayerController.Instance.canMove = true;
        Settings.Instance.HideAndShowPauseButtonIcon(true);
        ItemManager.Instance.ShowTutorial();
    }

    void CutsceneEnd()
    {
        endCam = GameObject.FindGameObjectWithTag("End Cam").GetComponent<CinemachineVirtualCamera>();

        if(PlayerController.Instance.cutscene)
        {
            Settings.Instance.HideAndShowPauseButtonIcon();
            mainCam.SetActive(false);
            endCam.enabled = true;
        }
    }
}
