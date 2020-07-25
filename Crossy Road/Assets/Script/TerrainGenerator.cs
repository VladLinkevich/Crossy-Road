using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private int maxTerrainCount;
    [SerializeField] private List<TerrainData> terrainDatas = new List<TerrainData>();

    private List<GameObject> currentTerrains = new List<GameObject>();


    private int selectTerrain = 0;
    private int terrainRepeat = 0;

    public Vector3 currentPosition = new Vector3(0, 0, 0);



    void Start()
    {
        for (int i = 0; i < maxTerrainCount; i++)
        {
            SpawnTerrain(true);
        }

    }

    void Update()
    {
    }
    public void SpawnTerrain(bool startGenerator = false)
    {

        if (startGenerator != true) { RemoveTerrain(); }

        if (terrainRepeat <= 0)
        {
            selectTerrain = Random.Range(0, terrainDatas.Count);
            terrainRepeat = Random.Range(0, terrainDatas[selectTerrain].maxInSuccession);
        }

        GameObject terrain = Instantiate(terrainDatas[selectTerrain].terrain, currentPosition, Quaternion.identity);

        currentTerrains.Add(terrain);


        currentPosition.x++;
        terrainRepeat--;
        return;


    }

    public void RemoveTerrain()
    {

        Destroy(currentTerrains[0]);
        currentTerrains.RemoveAt(0);

    }


}
