﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject coin;
    [SerializeField] private float minSpawnDelay;
    [SerializeField] private float maxSpawnDelay;
    [SerializeField] private int maxPoolCoin;

    private List<GameObject> poolCoins = new List<GameObject>();
    private int indexPool = 0;
    

    void Start()
    {
        for (int i = 0; i < maxPoolCoin; ++i)
        {
            poolCoins.Add(Instantiate(coin, transform.position, coin.transform.rotation));
            poolCoins[i].SetActive(false);
        }
        StartCoroutine(SpawnCoin());
    }

    private IEnumerator SpawnCoin()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            {
                if (playerTransform != null)
                {
                    poolCoins[indexPool].transform.position = playerTransform.position + SpawmPos();
                    poolCoins[indexPool].SetActive(true);
                    ++indexPool;
                    indexPool = indexPool == maxPoolCoin ? 0 : indexPool;
                }
            }
        }
    }


    private Vector3 SpawmPos()
    {
        int coordinateZ = 0;

        do
        {
            coordinateZ = (int)Random.Range(-10, 10);
        } while ((playerTransform.position.z + coordinateZ) > 9 || (playerTransform.position.z - coordinateZ) < -9);

        return new Vector3(10 + (int)Random.Range(0, 10), 0, coordinateZ);
    }
}
