using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Cutscene : MonoBehaviour
{
    public bool cutscene = true;

    [Header("Cameras")]
    public GameObject mainCam;
    public GameObject startCam;
    public GameObject busCam;
    [SerializeField] private CinemachineVirtualCamera endCam;


    void Awake()
    {
        if(cutscene) DisableCams();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(cutscene) 
            StartCoroutine(CutsceneStart());
        else
            StartGame();
    }

    void Update()
    {
        if(cutscene)
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
        StartGame();
    }

    void StartGame()
    {
        PlayerController.Instance.canMove = true;
        Settings.Instance.HideAndShowPauseButtonIcon(true);
    }

    //When win the game
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
