using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestGeneration : MonoBehaviour
{
    [SerializeField] private List<GameObject> elements = null;
    [SerializeField] private int maxObjectOnPlace = 0; 

    private List<GameObject> poolObjects = new List<GameObject>();

    private GameObject element = null;

    private Vector3 leftOutside = new Vector3(0, 1, 10);
    private Vector3 rightOutside = new Vector3(0, 1, -10);
    private void Start()
    {
        // Создание объектов по бокам карты
        poolObjects.Add(Instantiate(elements[Random.Range(0, elements.Count)], transform.position + leftOutside, gameObject.transform.rotation));
        poolObjects.Add(Instantiate(elements[Random.Range(0, elements.Count)], transform.position + rightOutside, gameObject.transform.rotation));

        // Создание объектов в рандомном месте на карте
        for (int i = 0, end = Random.Range(0, maxObjectOnPlace); i < end; ++i)
        {
            poolObjects.Add(Instantiate(elements[Random.Range(0, elements.Count)],
                            transform.position + new Vector3(0, 1, СhoisePosirion()),
                            gameObject.transform.rotation));
        }


    }

    // Выбор рандомной позиций для респа
    private float СhoisePosirion()
    {
        int random = (int)(Random.Range(-10, 10));
        if (transform.position.x >=5)
        {
            return random;
        } else if (random == 0 || random == 1 || random == -1)
        {
            return 5f;
        } else { return random; }

    }

    // при выходе из спящего режима перетаскиваем все объекты на новое место и пробуждаем их
    private void OnEnable()
    {
        for (int i = 0, end = poolObjects.Count; i < end; ++i)
        {
            poolObjects[i].transform.position = new Vector3(transform.position.x,
                                                            poolObjects[i].transform.position.y,
                                                            poolObjects[i].transform.position.z);
            poolObjects[i].SetActive(true);
        }
    }

    // Устанавливаем все объекты в спящий режим
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
