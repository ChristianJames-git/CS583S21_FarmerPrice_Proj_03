using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject buildAreaModel, openAreaModel, WallNS, WallWE, Ground;
    public Transform BuildPlatforms, OpenPaths, BaseReferences;
    private static int mapX = 8, mapZ = 16;
    private GameObject[,] areaBlocks = new GameObject[mapX,mapZ];

    // Start is called before the first frame update
    void Start()
    {
        //Generate build/move areas
        InstantiateBuildAreas();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InstantiateBuildAreas()
    {
        //Set Ground Size/Position
        Ground.transform.localScale = new Vector3(mapX, 0.2f, mapZ);
        Ground.transform.localPosition = new Vector3(mapX / 2 - 0.5f, -0.6f, mapZ / 2 - 0.5f);
        //Set WallNS
        WallNS.transform.localScale = new Vector3(mapX, 2, 0.2f);
        WallNS.transform.localPosition = new Vector3(mapX / 2 - 0.5f, 0.3f, -0.6f);
        Instantiate(WallNS, BaseReferences).transform.localPosition = new Vector3(mapX / 2 - 0.5f, 0.3f, -0.6f + mapZ);
        //Set WallWE
        WallWE.transform.localScale = new Vector3(0.2f, 2, mapZ + 0.4f);
        WallWE.transform.localPosition = new Vector3(-0.6f, 0.3f, mapZ/2 - 0.5f);
        Instantiate(WallWE, BaseReferences).transform.localPosition = new Vector3(-0.6f + mapX, 0.3f, mapZ / 2 - 0.5f);
        //Set build/open areas
        for (int i = 0; i < mapZ; i+=2)
        {
            if (i % 4 == 0)
            {
                OpenAreaBlock(0, i);
                BuildAreaBlock(mapX - 1, i);
            } else {
                BuildAreaBlock(0, i);
                OpenAreaBlock(mapX - 1, i);
            }
            for (int j = 1; j < mapX-1; j++)
                BuildAreaBlock(j, i);
        }
        buildAreaModel.SetActive(false);
        //instantiate open are rows
        for (int i = 1; i < mapZ; i+=2)
            for (int j = 0; j < mapX; j++)
                OpenAreaBlock(j, i);
        openAreaModel.SetActive(false);
    }

    private void BuildAreaBlock(int x, int z)
    {
        GameObject buildArea = Instantiate(buildAreaModel, BuildPlatforms);
        buildArea.SetActive(true);
        buildArea.transform.position = new Vector3(x, -0.4f, z);
        areaBlocks[x, z] = buildArea;
    }

    private void OpenAreaBlock(int x, int z)
    {
        GameObject openArea = Instantiate(openAreaModel, OpenPaths);
        openArea.SetActive(true);
        openArea.transform.position = new Vector3(x, -0.49f, z);
        areaBlocks[x, z] = openArea;
    }
}
