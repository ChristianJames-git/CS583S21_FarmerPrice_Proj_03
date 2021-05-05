using UnityEngine;

[SerializeField]
public class LevelCreation : MonoBehaviour
{
    private int coreHealth = 5;

    [Header("Map References")]
    public GameObject buildAreaModel;
    public GameObject openAreaModel, WallNS, WallWE, Ground;
    public Transform BuildPlatforms, OpenPaths, Boundaries, End;
    [Header("Map Size")]
    public int mapX = 8;
    public int mapZ = 15;

    // Start is called before the first frame update
    void Start()
    {
        InstantiateBuildAreas();
        PlayerInfo.instance.SetCoreHealth(coreHealth);
    }

    private void InstantiateBuildAreas()
    {
        if (mapX < 2 || mapZ < 2)
            return; //TBI Throw error of map size not applicable here
        //Handle odd values
        float shiftX, shiftZ, endX;
        if (mapX % 2 == 0)
            shiftX = mapX / 2 - 0.5f;
        else
            shiftX = mapX / 2;
        if (mapZ % 2 == 0)
        {
            shiftZ = mapZ / 2 - 0.5f;
            endX = 0;
        }
        else
        {
            shiftZ = mapZ / 2;
            endX = mapX - 1;
        }
        //Center Ground Position
        Ground.transform.localPosition = new Vector3(shiftX, -0.5f, shiftZ);
        //Set WallNS
        WallNS.transform.localScale = new Vector3(mapX, 2, 0.2f);
        WallNS.transform.localPosition = new Vector3(shiftX, 0.3f, -0.6f);
        Instantiate(WallNS, Boundaries).transform.localPosition = new Vector3(shiftX, 0.3f, -0.4f + mapZ);
        //Set WallWE
        WallWE.transform.localScale = new Vector3(0.2f, 2, mapZ + 0.4f);
        WallWE.transform.localPosition = new Vector3(-0.6f, 0.3f, shiftZ);
        Instantiate(WallWE, Boundaries).transform.localPosition = new Vector3(-0.4f + mapX, 0.3f, shiftZ);
        //Set even rows starting on 0
        for (int i = 0; i < mapZ; i += 2)
        {
            if (i % 4 == 0)
            {
                OpenAreaBlock(0, i);
                BuildAreaBlock(mapX - 1, i);
            }
            else
            {
                BuildAreaBlock(0, i);
                OpenAreaBlock(mapX - 1, i);
            }
            for (int j = 1; j < mapX - 1; j++)
                BuildAreaBlock(j, i);
        }
        buildAreaModel.SetActive(false);
        //Set odd rows starting on 1
        for (int i = 1; i < mapZ; i += 2)
            for (int j = 0; j < mapX; j++)
                OpenAreaBlock(j, i);
        openAreaModel.SetActive(false);
        //Move end
        End.position = new Vector3(endX, 0, mapZ - 1);
    }

    private void BuildAreaBlock(int x, int z)
    {
        GameObject buildArea = Instantiate(buildAreaModel, BuildPlatforms);
        buildArea.SetActive(true);
        buildArea.transform.position = new Vector3(x, -0.4f, z);
        //buildArea.transform.scale = new Vector3(1,1,1);
    }

    private void OpenAreaBlock(int x, int z)
    {
        GameObject openArea = Instantiate(openAreaModel, OpenPaths);
        openArea.SetActive(true);
        openArea.transform.localPosition = new Vector3(x, -0.49f, z);
    }
}
