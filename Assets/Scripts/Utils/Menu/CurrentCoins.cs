using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrentCoins : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public SOInt soCoin;

    void Update()
    {
        coinText.SetText("" + soCoin.value);    
    }
}
