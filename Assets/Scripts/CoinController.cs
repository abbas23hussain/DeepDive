using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    [SerializeField] private ParticleSystem coinParticleSystem;

    private void Start()
    {
        coinParticleSystem.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayCollectCoinAnimation();
        }
    }

    private void PlayCollectCoinAnimation()
    {
        coinParticleSystem.gameObject.SetActive(true);
        coinParticleSystem.Play(true);
        StartCoroutine(CoinCollectionCoroutine(0.5f));
    }

    private IEnumerator CoinCollectionCoroutine(float collectionTime)
    {
        yield return new WaitForSeconds(collectionTime);
        OnCoinCollected();
    }

    private void OnCoinCollected()
    {
        gameObject.SetActive(false);
        EventManager.onCoinCollected?.Invoke(this.gameObject);
        //Destroy(this);
    }

}
