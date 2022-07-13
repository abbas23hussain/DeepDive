using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class CoinText : MonoBehaviour
{
    public TextMeshPro coinText;
    
    void Start()
    {
        coinText.text = "+"+(DataSaveController.instance.money.Coins + 2).ToString();
        transform.DOMoveX(transform.position.x + 2, 0.5f).OnComplete(() => Destroy(gameObject));
        EventManager.onCoinCollected?.Invoke(this.gameObject);
    }

    
}
