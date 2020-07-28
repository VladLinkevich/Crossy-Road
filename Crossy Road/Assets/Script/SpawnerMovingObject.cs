using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMovingObject : MonoBehaviour
{

    [SerializeField] private new GameObject gameObject;
    [SerializeField] private List<Transform> spawnPos = new List<Transform>();
    [SerializeField] private float minObjectDuration;
    [SerializeField] private float maxObjectDuration;
    [SerializeField] private float minSpawnDelay;
    [SerializeField] private float maxSpawnDelay;
    [SerializeField] private int poolCount;


    private MovingObject movingObject;
    private GameObject obj;
    private List<GameObject> poolObjects = new List<GameObject>();
    private float duration;
    private int indexPositionObject = 0;
    private int poolIndex = 0;

    
    void Start()
    {


        duration = Random.Range(minObjectDuration, maxObjectDuration);

        indexPositionObject = Random.Range(0, spawnPos.Count);
        for (int i = 0; i < poolCount; ++i)
        {  
            obj = Instantiate(gameObject, spawnPos[indexPositionObject].position, spawnPos[indexPositionObject].rotation);
            movingObject = obj.GetComponent<MovingObject>();
            movingObject.SetDuration(duration);
            poolObjects.Add(obj);
            
        }

        
    }

    public void OnEnable()
    {
        StartCoroutine(SpawnVehicle());
    }

    private IEnumerator SpawnVehicle()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            poolObjects[poolIndex].transform.position = spawnPos[indexPositionObject].position;
            poolObjects[poolIndex].SetActive(true);
            ++poolIndex;
            poolIndex = poolIndex == poolObjects.Count ? 0 : poolIndex;
        }
    }


}
