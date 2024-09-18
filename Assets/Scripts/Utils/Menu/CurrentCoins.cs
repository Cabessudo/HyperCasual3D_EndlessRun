using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Save;

public class CurrentCoins : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    private int _currCoins;
    private SaveManager save;
    private bool started = false;

    void Start()
    {
        save = SaveManager.Instance?.GetComponent<SaveManager>();
        Init();
    }

    void Init()
    {
        _currCoins = save.saveLayout.coins.value;
        coinText.SetText("" + _currCoins);
        started = true;
    }

    void Update()
    {
        if(_currCoins != save.saveLayout.coins.value && started)
        {
            if(_currCoins > save.saveLayout.coins.value)
                _currCoins -= (int)Time.deltaTime;
            else if(_currCoins < save.saveLayout.coins.value)
                _currCoins += (int)Time.deltaTime;
             
            coinText.SetText("" + save.saveLayout.coins.value);    
        }
    }
}