using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinsManager : MonoBehaviour
{
    [SerializeField] private Transform coinsParent;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Transform startPosition;

    private int queueSize = 10;
    private int step = 15;
    private float Lpos = 248.5f;
    private float Rpos = 251.5f;

    private Queue<GameObject> coins;
    
    private void Awake()
    {
        BindEvents();
    }

    private void OnDestroy()
    {
        UnbindEvents();
    }

    private void BindEvents()
    {
        EventManager.onCoinCollected += OnCoinCollected;
        EventManager.onCoinMissed += ReAddCoin;
    }

    private void UnbindEvents()
    {
        EventManager.onCoinCollected -= OnCoinCollected;
        EventManager.onCoinMissed -= ReAddCoin;
    }

    private void Start()
    {
        coins = new Queue<GameObject>();
        InitCoins();

    }

    private void InitCoins()
    {
        for (int i = 0; i < queueSize; i++)
        {
            GameObject coin = Instantiate(coinPrefab, coinsParent);
            coin.transform.position = startPosition.transform.position;
            coin.transform.position += (i + 1) * step * Vector3.down;
            float xPos = Random.Range(Lpos, Rpos);
            coin.transform.position = new Vector3(xPos, coin.transform.position.y, coin.transform.position.z);
            coins.Enqueue(coin);
        }
    }

    private void OnCoinCollected(GameObject coin)
    {
        RemoveAndReAdd(coin);
    }

    private void ReAddCoin()
    {
        var coin = coins.Dequeue();
        coin.gameObject.SetActive(true);
        Vector3 lastCoinPos = coins.Last().transform.position;
        coin.transform.position = lastCoinPos + step * Vector3.down;
        float xPos = Random.Range(Lpos, Rpos);
        coin.transform.position = new Vector3(xPos, coin.transform.position.y, coin.transform.position.z);
        coins.Enqueue(coin);
    }

    private void RemoveAndReAdd(GameObject coin)
    {
        coins = new Queue<GameObject>(coins.Where(x => x!=coin));
        coin.SetActive(true);
        Vector3 lastCoinPos = coins.Last().transform.position;
        coin.transform.position = lastCoinPos + step * Vector3.down;
        float xPos = Random.Range(Lpos, Rpos);
        coin.transform.position = new Vector3(xPos, coin.transform.position.y, coin.transform.position.z);
        coins.Enqueue(coin);
    }
    
    
    
    
}
