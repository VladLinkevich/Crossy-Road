using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainsGenerator : MonoBehaviour
{

    [SerializeField] private new GameObject trainObject = null;
    [SerializeField] private List<Transform> spawnPos = new List<Transform>();
    [SerializeField] private List<Light> lights = new List<Light>();
    [SerializeField] private AudioSource audio = null;
    [SerializeField] private float duration = 0;
    [SerializeField] private float minSpawnDelay = 0;
    [SerializeField] private float maxSpawnDelay = 0;

    private MovingObject movingObject = null;

    private GameObject train = null;
    private int indexPositionObject = 0;
   
    void Start()
    {
        indexPositionObject = Random.Range(0, spawnPos.Count);
        train = Instantiate(trainObject, spawnPos[indexPositionObject].position, spawnPos[indexPositionObject].rotation);
        movingObject = train.GetComponent<MovingObject>();
        movingObject.SetDuration(duration);
        
    }

    private void OnEnable()
    {
        StartCoroutine(SpawnTrain());
    }


    // включение фар на фонаре
    IEnumerator PreparetionTrain()
    {
        audio.Play();
    
        for (int i = 0; i < 12; ++i)
        {
            lights[0].range = 0;
            lights[1].range = 1;
            yield return new WaitForSeconds(0.2f);
            lights[1].range = 0;
            lights[0].range = 1;
            yield return new WaitForSeconds(0.2f);

        }
       
        lights[1].range = 0;
        lights[0].range = 0;
    }

    private IEnumerator SpawnTrain()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            StartCoroutine(PreparetionTrain());
            yield return new WaitForSeconds(3);
            train.transform.position = spawnPos[indexPositionObject].position;
            train.SetActive(true);

        }
    }

    private void OnDisable()
    {
        lights[1].range = 0;
        lights[0].range = 0;
    }
    
}
