using UnityEngine;
using DG.Tweening;
using TMPro;

public class FloatText : MonoBehaviour
{
    public RectTransform holdText;

    void Start()
    {
        holdText.DOScale(1.2f, 0.5f).OnComplete(() => holdText.DOScale(0.9f, 0.5f)).SetLoops(-1,LoopType.Yoyo);
    }

   
}
