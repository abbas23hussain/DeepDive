using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SeaObjectFloat : MonoBehaviour
{
    
    void Start()
    {
        Invoke(nameof(UpDown), 0.5f);
    }

    
    void Update()
    {
        transform.Rotate(0, 100 * Time.deltaTime, 0);
    }

    public void UpDown()
    {
        transform.DOMoveY(transform.position.y + 1, 1).SetEase(Ease.OutCubic).SetLoops(-1,LoopType.Yoyo);
    }
}
