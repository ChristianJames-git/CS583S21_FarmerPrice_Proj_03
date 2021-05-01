using UnityEngine;

public class Waypoints : MonoBehaviour
{
    //static so objects can access it without a direct reference
    [SerializeField] private LevelCreation lc;
    public static Transform[] points;
    public Transform Waypoint;

    private void Awake()
    {
        for (int i = 1, j = 0; i < lc.mapZ; i += 2, j++)
        {
            if (j % 2 == 0)
                Instantiate(Waypoint, gameObject.transform).position = new Vector3(0, 0, i);
            Instantiate(Waypoint, gameObject.transform).position = new Vector3(lc.mapX - 1, 0, i);
            if (j % 2 == 1)
                Instantiate(Waypoint, gameObject.transform).position = new Vector3(0, 0, i);
        }
        if (lc.mapZ % 2 == 1)
            Instantiate(Waypoint, gameObject.transform).position = new Vector3(lc.mapX - 1, 0, lc.mapZ - 1);
        points = new Transform[transform.childCount];

        //go through all the children of this object and add them to the array in order
        for (int i = 0; i < points.Length; i++)
            points[i] = transform.GetChild(i);
    }
}
