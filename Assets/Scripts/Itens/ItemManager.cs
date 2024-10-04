using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using Save;

public class ItemManager : Singleton<ItemManager>
{ 
    private SaveManager save;

    public LoadScene scene;
    public TransitionScene transition;
    public TextMeshProUGUI coinText;
    public int inGameCoins;
    public TextMeshProUGUI inGameCoinsEndText;
    public TextMeshProUGUI inGameCoinsText;

    [Header("Coin Anim")]
    //Spawn Coins Anim
    public RectTransform inGameCoinsCase;
    public List<GameObject> coinsCase;
    public GameObject coinsHolder;
    public GameObject coinToSpawn;
    private int amountCoins = 30;
    //----------------
    public GameObject whereToSpawn;
    public GameObject whereToGo;
    public Ease ease = Ease.Linear;
    private float delayToSpawn = .1f;
    private int index;
    private bool showInGameCoins;
    private float scaleBounceAnim = 1.2f;
    private float menuCoinIn = 475;
    private float inGameEndCoinIn = 0;
    private bool canTransition;
    private bool jumpEndAnim = false;

    [Header("PWUP Firt Time Collect")]
    public GameObject pwupFirstCollect;
    public Image pwupIcon;
    public Image alphaColor;
    public TextMeshProUGUI pwupTxt;
    private bool canInteract;

    //Helpers
    private int one = 1;
    private int zero = 0;
    private int whiteColor = 255;
    private bool once;
    
    [Header("Audio")]
    public AudioManager sfx;
    public AudioMenu sfxUI;
    
    void Start()
    {
        save = SaveManager.Instance?.GetComponent<SaveManager>();
        
        coinsCase = new List<GameObject>();
        for(int i = 0; i < amountCoins; i++)
        {
            var obj = (GameObject)Instantiate(coinToSpawn);
            coinsCase.Add(obj);
            obj.SetActive(false);
            obj.transform.SetParent(coinsHolder.transform);
        }
    }

    void Update()
    {
        LimitCheck();
        CollectedCoins();

        if(RestartScreen.Instance.showCoins)
        {
            RestartScreen.Instance.showCoins = false;
            ShowEndCoinsAnim();
            Invoke(nameof(GetCoinsAnim), 1);
        }

        if(canTransition)
        {
            canTransition = false;
            StartCoroutine(WaitToClose());
        }

        if(Input.GetKeyDown(KeyCode.Mouse0) && canInteract && once)
        {
            once = false;
            HideFirstTimeCollect();
            sfx.ShowAndHideFirstTimeGetPWUPSFX(false);
        }

        if(Input.GetKeyDown(KeyCode.Mouse0) && jumpEndAnim && !RestartScreen.Instance.showCoins)
        {
            JumpEndCoinsAnim();
        }
    }

    public void AddCoins(int manager = 1)
    {
        inGameCoins += manager;
        CollectAnim();

        if(showInGameCoins)
        Bounce(inGameCoinsCase, scaleBounceAnim);
        
    }

    void EndCoinsAnim()
    {
        index++;
        if(index >= coinsCase.Count)
        index = zero;
        coinsCase[index].SetActive(true);
        Bounce(whereToSpawn.transform, .9f);
        sfxUI?.CoinClick();
        inGameCoins--;
        coinsCase[index].transform.position = whereToSpawn.transform.position + Vector3.left * 100;
        coinsCase[index].transform.DOMove(whereToGo.transform.position, .9f).SetEase(ease).OnComplete(
            delegate
            {
                if(jumpEndAnim)
                {
                    Bounce(whereToGo.transform, scaleBounceAnim);
                    save.saveLayout.coins.value++;
                    sfx.CoinSFX();
                }
            });                
    }

    void JumpEndCoinsAnim()
    {
        save.saveLayout.coins.value += inGameCoins;
        canTransition = true;
        jumpEndAnim = false;
    }

    void LimitCheck()
    {
        if(inGameCoins <= zero)
        inGameCoins = zero;
    }
    
    //Update Coins Texts
    public void CollectedCoins()
    {
        inGameCoinsEndText.SetText("x" + inGameCoins);
        coinText.SetText("" + save.saveLayout.coins.value);
        inGameCoinsText.SetText("" + inGameCoins);
    }

    [NaughtyAttributes.Button]
    public void ShowEndCoinsAnim()
    {
        sfxUI.Appear();
        whereToGo.transform.DOLocalMoveX(menuCoinIn, .7f).SetEase(ease);
        whereToSpawn.transform.DOLocalMoveX(inGameEndCoinIn, .7f).SetEase(ease);
    }

    void GetCoinsAnim()
    {
        StartCoroutine(UpdateCoins());
    }

    IEnumerator WaitToClose()
    {
        yield return new WaitForSeconds(one);
        transition.CloseScene();
        yield return new WaitForSeconds(one);
        scene.Load(0);
    }

    public IEnumerator UpdateCoins()
    {
        jumpEndAnim = true;

        for(int i = inGameCoins; i > zero; i--)
        {
            EndCoinsAnim();
            yield return new WaitForSeconds(delayToSpawn); 
        }
        
        if(inGameCoins <= zero)
        canTransition = true;
        yield return null;

    }

    #region Collect Coins In Game Anim
    public void CollectAnim()
    {
        StopAllCoroutines();

        if(!showInGameCoins)
        {
            inGameCoinsCase.DOMoveY(-140, .1f).SetEase(ease).SetRelative();
            showInGameCoins = true;

            StartCoroutine(HideCoinsCollectRoutine());
        }
    }
    
    public void HideCoinsInGameText()
    {
        inGameCoinsCase.DOMoveY(140, .1f).SetEase(ease).SetRelative();
    }

    void Bounce(Transform t, float scale)
    {
        t.localScale = Vector3.one;
        t.DOScale(scale, .05f).SetEase(ease).SetLoops(2, LoopType.Yoyo);
    }

    IEnumerator HideCoinsCollectRoutine()
    {
        yield return new WaitForSeconds(15);
        HideCoinsInGameText();
        showInGameCoins = false;
    }
    #endregion

    #region FirstTimeCollectPWUP
    public void FirstTimeCollect(Sprite sprite, string txt, float z = 0)
    { 
        once = true;
        Settings.Instance.HideAndShowPauseButtonIcon();
        Settings.Instance.backGround.SetActive(true);
        pwupTxt.SetText(txt);
        pwupIcon.sprite = sprite;
        sfx.ShowAndHideFirstTimeGetPWUPSFX(true);
        pwupIcon.transform.DORotate(new Vector3(zero,zero,z), .2f).SetEase(ease);
        pwupFirstCollect.transform.DOScale(one, .7f).SetEase(Ease.OutBack).OnComplete(
            delegate
            {
                alphaColor.DOColor(new Color(whiteColor,whiteColor,whiteColor,zero), .2f).SetEase(ease).OnComplete(
                    delegate
                    { 
                        Time.timeScale = zero;
                        canInteract = true;
                    });
            });
    }

    public void HideFirstTimeCollect()
    {
        if(canInteract)
        {
            Settings.Instance.HideAndShowPauseButtonIcon(true);
            Time.timeScale = one;
            Settings.Instance.backGround.SetActive(false);
            pwupFirstCollect.transform.DOScale(zero, .7f).SetEase(Ease.OutBack);
            canInteract = false;
        }
    }
    #endregion

    void OnDestroy()
    {
        if(pwupFirstCollect != null)pwupFirstCollect.transform.DOKill();
        alphaColor?.DOKill();
        pwupIcon?.transform.DOKill();
        inGameCoinsCase?.DOKill();
        whereToGo?.transform.DOKill();
        whereToSpawn?.transform.DOKill();
    }
}
