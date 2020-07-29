using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMovingObject : MonoBehaviour
{

    [SerializeField] private new GameObject gameObject = null;
    [SerializeField] private List<Transform> spawnPos = new List<Transform>();
    [SerializeField] private float minObjectDuration = 0;
    [SerializeField] private float maxObjectDuration = 0;
    [SerializeField] private float minSpawnDelay = 0;
    [SerializeField] private float maxSpawnDelay = 0;
    [SerializeField] private int poolCount = 0;


    private MovingObject movingObject = null;
    private GameObject obj = null;
    private List<GameObject> poolObjects = new List<GameObject>();
    private float duration = 0;
    private int indexPositionObject = 0;
    private int poolIndex = 0;

    
    void Start()
    {

        // для каждой дороге у нас свое значение скорости
        duration = Random.Range(minObjectDuration, maxObjectDuration);
        // выбираем место где будем респить объекты
        indexPositionObject = Random.Range(0, spawnPos.Count);

        // Создаем пул объектов
        for (int i = 0; i < poolCount; ++i)
        {  
            obj = Instantiate(gameObject, spawnPos[indexPositionObject].position, spawnPos[indexPositionObject].rotation);
            movingObject = obj.GetComponent<MovingObject>();
            movingObject.SetDuration(duration);
            poolObjects.Add(obj);
            
        }

        
    }

    // Когда наша дорога активируется запускаем респ объектов
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
