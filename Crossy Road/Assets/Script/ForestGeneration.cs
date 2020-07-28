using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestGeneration : MonoBehaviour
{
    [SerializeField] private List<GameObject> elements;
    [SerializeField] private int maxObjectOnPlace; 

    private List<GameObject> poolObjects = new List<GameObject>();

    private GameObject element;

    private Vector3 leftOutside = new Vector3(0, 1, 10);
    private Vector3 rightOutside = new Vector3(0, 1, -10);
    private void Start()
    {
        
        poolObjects.Add(Instantiate(elements[Random.Range(0, elements.Count)], transform.position + leftOutside, gameObject.transform.rotation));
        poolObjects.Add(Instantiate(elements[Random.Range(0, elements.Count)], transform.position + rightOutside, gameObject.transform.rotation));
        for (int i = 0, end = Random.Range(0, maxObjectOnPlace); i < end; ++i)
        {
            poolObjects.Add(Instantiate(elements[Random.Range(0, elements.Count)],
                transform.position + new Vector3(0, 1, choisePosirion()),
                gameObject.transform.rotation));
        }


    }

    private float choisePosirion()
    {
        int random = (int)(Random.Range(-10, 10));
        if (transform.position.x >=5)
        {
            return random;
        } else if (random == 0)
        {
            return 1f;
        } else { return random; }

    }
    private void OnEnable()
    {
        for (int i = 0, end = poolObjects.Count; i < end; ++i)
        {
            poolObjects[i].transform.position = new Vector3(transform.position.x, poolObjects[i].transform.position.y, poolObjects[i].transform.position.z);
            poolObjects[i].SetActive(true);
        }
    }
    private void OnDisable()
    {
        for (int i = 0, end = poolObjects.Count; i < end; ++i)
        {
            if (poolObjects[i] != null)
            {
                poolObjects[i].SetActive(false);
            }
        }
    }
}
