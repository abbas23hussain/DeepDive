using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FloatObject : MonoBehaviour
{
    public bool isRight;
    public bool jelyFih;
   
    void Start()
    {
        if (isRight && !jelyFih)
        {
            transform.DOLocalMoveX(3, 5f).
                OnComplete(() => transform.DOLocalMoveX(-3, 5f)).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.Linear);
            InvokeRepeating(nameof(FishScale), 5, 5);
        }
        else if(!isRight && !jelyFih)
        {
            transform.DOLocalMoveX(-7, 5f).
                OnComplete(() => transform.DOLocalMoveX(3, 5f)).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
            InvokeRepeating(nameof(FishScale), 5, 5);
        }

        if (jelyFih && !isRight)
        {
            //0.3979371 x
            //0.1133184 y
            //1 z
            //transform.DOScale(0.3969371f, 2f).OnComplete(() => transform.DOScale(0.3989371f, 2F)).SetLoops(-1,LoopType.Yoyo);
        }
        
    }

    public void FishScale()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
    
}
