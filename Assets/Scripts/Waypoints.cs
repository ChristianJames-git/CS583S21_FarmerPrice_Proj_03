using UnityEngine;

public class Waypoints : MonoBehaviour
{
    //static so objects can access it without a direct reference
    private GameManager gm;
    public static Transform[] points;
    public Transform Waypoint;

    private void Awake()
    {
        gm = GameManager.Instance;
        for (int i = 1, j = 0; i < gm.mapZ; i += 2, j++)
        {
            if (j % 2 == 0)
                Instantiate(Waypoint, gameObject.transform).position = new Vector3(0, 0, i);
            Instantiate(Waypoint, gameObject.transform).position = new Vector3(gm.mapX - 1, 0, i);
            if (j % 2 == 1)
                Instantiate(Waypoint, gameObject.transform).position = new Vector3(0, 0, i);
        }
        if (gm.mapZ % 2 == 1)
            Instantiate(Waypoint, gameObject.transform).position = new Vector3(gm.mapX - 1, 0, gm.mapZ - 1);
        points = new Transform[transform.childCount];

        //go through all the children of this object and add them to the array in order
        for (int i = 0; i < points.Length; i++)
            points[i] = transform.GetChild(i);
    }
}
