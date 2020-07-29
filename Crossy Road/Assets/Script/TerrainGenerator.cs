using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private int maxTerrainCount = 0;
    [SerializeField] private List<TerrainData> terrainDatas = new List<TerrainData>();


    private GameObject[,] poolTerrain = null;
    private int[] indexPool = null;
    private List<GameObject> currentTerrains = new List<GameObject>();

    private int selectTerrain = 0;
    private int terrainRepeat = 0;

    private GameObject terrain;
    public Vector3 currentPosition = new Vector3(0, 0, 0);



    void Start()
    {


        //Cоздание пула объектов
        indexPool = new int[terrainDatas.Count];
        for (int i = 0, end = terrainDatas.Count; i < end; ++i)
        {
            indexPool[i] = 0;
        }
        poolTerrain = new GameObject[terrainDatas.Count, maxTerrainCount];                                      
        for (int i = 0, firstEnd = poolTerrain.GetLength(0); i < firstEnd; ++i)
        {
            for (int j = 0, secondEnd = poolTerrain.GetLength(1); j < secondEnd; ++j)
            {
                poolTerrain[i, j] = Instantiate(terrainDatas[i].terrain, currentPosition, Quaternion.identity);
                poolTerrain[i, j].SetActive(false);
            }
        }


        // Чтобы при старте появлялись объекты terrainsDatas[0](в моем случае это земля) пять раз подряд. Что-то вроде начальной сайв зоны  
        selectTerrain = 0;                                                                                       
        terrainRepeat = 20;          


        // Начальная инициализация поля
        for (int i = 0; i < maxTerrainCount; i++)  
        {
            SpawnTerrain(true);
        }

    }

    // Добавляет объекты
    public void SpawnTerrain(bool startGenerator = false)
    {

        if (startGenerator != true) { RemoveTerrain(); }

        if (terrainRepeat <= 0)
        {
            selectTerrain = Random.Range(0, terrainDatas.Count);
            terrainRepeat = Random.Range(0, terrainDatas[selectTerrain].maxInSuccession);
        }

         
        currentTerrains.Add(SelectPassiveTerrain(selectTerrain));

        currentPosition.x++;
        terrainRepeat--;

        return;


    }

    // Выбор неактивного объукта если таких нет выбирает последний активированный объект 
    private GameObject SelectPassiveTerrain(int selectTerrain)
    {
        terrain = poolTerrain[selectTerrain, indexPool[selectTerrain]];

        terrain.transform.position = currentPosition;
        terrain.SetActive(true);
       
        ++indexPool[selectTerrain];
        indexPool[selectTerrain] = indexPool[selectTerrain] == maxTerrainCount ? 0 : indexPool[selectTerrain];

        return terrain;
    }

    public void RemoveTerrain()
    {

        currentTerrains[0].SetActive(false);
        currentTerrains.RemoveAt(0);

    }


}
